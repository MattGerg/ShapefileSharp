using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ShapefileSharp.Spec
{
    internal sealed class MultiPartGeometryZField : Field<IMultiPartGeometry<IPointZ>>
    {
        public MultiPartGeometryZField(WordCount offset) : base(offset)
        {
            Box = new BoundingBox2dField(offset);
            NumParts = new IntField(offset + WordCount.FromBytes(32), Endianness.Little);
            NumPoints = new IntField(offset + WordCount.FromBytes(36), Endianness.Little);
        }

        private BoundingBox2dField Box { get; }
        private IntField NumParts { get; }
        private IntField NumPoints { get; }

        /// <summary>
        /// The index of the first point in the part.
        /// </summary>
        private IntField Part(int partIndex)
        {
            //TODO: Do we have to do all of this math again and again for every part?
            var offset = Offset + WordCount.FromBytes(40) + (partIndex * IntField.FieldLength);

            return new IntField(offset, Endianness.Little);
        }

        private PointZField Point(WordCount pointsOffset, int numPoints, int pointIndex)
        {
            var xOffset = OffsetX(pointsOffset, pointIndex);
            var yOffset = xOffset + DoubleField.FieldLength;
            var zOffset = OffsetZ(pointsOffset, numPoints, pointIndex);
            var mOffset = OffsetM(pointsOffset, numPoints, pointIndex);

            return new PointZField(xOffset, yOffset, zOffset, mOffset);
        }

        private WordCount OffsetX(WordCount pointsOffset, int pointIndex)
        {
            return pointsOffset + (pointIndex * PointField.FieldLength);
        }

        private DoubleField MinZ(WordCount pointsOffset, int numPoints)
        {
            var offset = OffsetMinZ(pointsOffset, numPoints);

            return new DoubleField(offset);
        }

        private WordCount OffsetMinZ(WordCount pointsOffset, int numPoints)
        {
            return pointsOffset + (numPoints * PointField.FieldLength);
        }

        private DoubleField MaxZ(WordCount pointsOffset, int numPoints)
        {
            var offset = OffsetMaxZ(pointsOffset, numPoints);

            return new DoubleField(offset);
        }

        private WordCount OffsetMaxZ(WordCount pointsOffset, int numPoints)
        {
            return OffsetMinZ(pointsOffset, numPoints) + DoubleField.FieldLength;
        }

        private WordCount OffsetZ(WordCount pointsOffset, int numPoints, int pointIndex)
        {
            //MaxZ.Offset + MaxZ.Length + Nth Z value
            return OffsetMaxZ(pointsOffset, numPoints) + DoubleField.FieldLength + (pointIndex * DoubleField.FieldLength);
        }


        private DoubleField MinM(WordCount pointsOffset, int numPoints)
        {
            var offset = OffsetMinM(pointsOffset, numPoints);

            return new DoubleField(offset);
        }

        private WordCount OffsetMinM(WordCount pointsOffset, int numPoints)
        {
            //After the last Z value
            return OffsetZ(pointsOffset, numPoints, numPoints - 1) + DoubleField.FieldLength;
        }

        private DoubleField MaxM(WordCount pointsOffset, int numPoints)
        {
            var offset = OffsetMaxM(pointsOffset, numPoints);

            return new DoubleField(offset);
        }

        private WordCount OffsetMaxM(WordCount pointsOffset, int numPoints)
        {
            return OffsetMinM(pointsOffset, numPoints) + DoubleField.FieldLength;
        }

        private WordCount OffsetM(WordCount pointsOffset, int numPoints, int pointIndex)
        {
            //MaxM.Offset + MaxM.Length + Nth M value
            return OffsetMaxM(pointsOffset, numPoints) + DoubleField.FieldLength + (pointIndex * DoubleField.FieldLength);
        }

        
        /// <summary>
        /// The offset of the first point.
        /// </summary>s>
        private WordCount OffsetPoints(int numParts)
        {
            return Offset + WordCount.FromBytes(40) + (numParts * IntField.FieldLength);
        }

        public override IMultiPartGeometry<IPointZ> Read(BinaryReader reader, WordCount origin)
        {
            var box = Box.Read(reader, origin);
            var numParts = NumParts.Read(reader, origin);
            var numPoints = NumPoints.Read(reader, origin);

            var pointStartIndices = new List<int>();

            for (var i = 0; i < numParts; i++)
            {
                var pointStartIndex = Part(i).Read(reader, origin);

                pointStartIndices.Add(pointStartIndex);
            }

            var parts = new List<IMultiPointGeometry<IPointZ>>();
            var pointsOffset = OffsetPoints(numParts);

            for (var i = 0; i < pointStartIndices.Count; i++)
            {
                var startIndex = pointStartIndices[i];
                var endIndex = (pointStartIndices.Count > (i + 1) ? pointStartIndices[i + 1] : numPoints) - 1;
                var part = new MultiPointGeometry<IPointZ>();

                for (var iPointIndex = startIndex; iPointIndex <= endIndex; iPointIndex++)
                {
                    var point = Point(pointsOffset, numPoints, iPointIndex).Read(reader, origin);
                    part.Points.Add(point);
                }

                parts.Add(part);
            }

            var minZ = MinZ(pointsOffset, numPoints).Read(reader, origin);
            var maxZ = MaxZ(pointsOffset, numPoints).Read(reader, origin);

            var minM = MinM(pointsOffset, numPoints).Read(reader, origin);
            var maxM = MaxM(pointsOffset, numPoints).Read(reader, origin);

            var boxZ = new BoundingBox<IPointZ>()
            {
                Min = new PointZ()
                {
                    X = box.Min.X,
                    Y = box.Min.Y,
                    Z = minZ,
                    M = minM
                },
                Max = new PointZ()
                {
                    X = box.Max.X,
                    Y = box.Max.Y,
                    Z = maxZ,
                    M = maxM
                }
            };

            return new MultiPartGeometry<IPointZ>()
            {
                Box = boxZ,
                Parts = parts
            };
        }

        public override void Write(BinaryWriter writer, IMultiPartGeometry<IPointZ> value, WordCount origin)
        {
            Box.Write(writer, value.Box, origin);

            var partCount = value.Parts.Count;
            NumParts.Write(writer, partCount, origin);

            var points = value.Parts.SelectMany(part => part.Points).ToArray();

            NumPoints.Write(writer, points.Length, origin);

            int pointCount = 0;

            for (int i = 0; i < partCount; i++)
            {
                Part(i).Write(writer, pointCount, origin);

                pointCount += value.Parts[i].Points.Count;
            }

            var pointsOffset = OffsetPoints(partCount);

            for (int i = 0; i < points.Length; i++)
            {
                Point(pointsOffset, pointCount, i).Write(writer, points[i], origin);
            }

            MinZ(pointsOffset, pointCount).Write(writer, value.Box.Min.Z, origin);
            MaxZ(pointsOffset, pointCount).Write(writer, value.Box.Max.Z, origin);

            MinM(pointsOffset, pointCount).Write(writer, value.Box.Min.M, origin);
            MaxM(pointsOffset, pointCount).Write(writer, value.Box.Max.M, origin);
        }
    }
}
