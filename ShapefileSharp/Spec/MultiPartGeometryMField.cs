using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ShapefileSharp.Spec
{
    internal sealed class MultiPartGeometryMField : Field<IMultiPartGeometry<IPointM>>
    {
        public MultiPartGeometryMField(WordCount offset) : base(offset)
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
            return new IntField(Offset + WordCount.FromBytes(40) + (partIndex * IntField.FieldLength), Endianness.Little);
        }

        private PointMField Point(WordCount pointsOffset, int numPoints, int pointIndex)
        {
            var xyOffset = pointsOffset + (pointIndex * PointField.FieldLength);

            //Points + MinM + MaxM + M values
            var mOffset = pointsOffset + (numPoints * PointField.FieldLength) + (2 * DoubleField.FieldLength) + (pointIndex * DoubleField.FieldLength);

            return new PointMField(xyOffset, mOffset);
        }

        private DoubleField MinM(WordCount pointsOffset, int numPoints)
        {
            var offset = OffsetMinM(pointsOffset, numPoints);
            return new DoubleField(offset);
        }

        private WordCount OffsetMinM(WordCount pointsOffset, int numPoints)
        {
            var offset = pointsOffset + (numPoints * PointMField.FieldLength);
            return offset;
        }

        private DoubleField MaxM(WordCount pointsOffset, int numPoints)
        {
            var offset = OffsetMaxM(pointsOffset, numPoints);
            return new DoubleField(offset);
        }

        private WordCount OffsetMaxM(WordCount pointsOffset, int numPoints)
        {
            var offset = OffsetMinM(pointsOffset, numPoints) + DoubleField.FieldLength;
            return offset;
        }

        /// <summary>
        /// The offset of the first point.
        /// </summary>s>
        private WordCount OffsetPoints(int numParts)
        {
            var offset = Offset + WordCount.FromBytes(40) + (numParts * IntField.FieldLength);
            return offset;
        }

        public override IMultiPartGeometry<IPointM> Read(BinaryReader reader, WordCount origin)
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

            var parts = new List<IMultiPointGeometry<IPointM>>();
            var pointsOffset = OffsetPoints(numParts);

            for (var i = 0; i < pointStartIndices.Count; i++)
            {
                var startIndex = pointStartIndices[i];
                var endIndex = (pointStartIndices.Count > (i + 1) ? pointStartIndices[i + 1] : numPoints) - 1;
                var part = new MultiPointGeometry<IPointM>();

                for (var iPointIndex = startIndex; iPointIndex <= endIndex; iPointIndex++)
                {
                    var point = Point(pointsOffset, numPoints, iPointIndex).Read(reader, origin);
                    part.Points.Add(point);
                }

                parts.Add(part);
            }

            var minM = MinM(pointsOffset, numPoints).Read(reader, origin);
            var maxM = MaxM(pointsOffset, numPoints).Read(reader, origin);

            var boxM = new BoundingBox<IPointM>()
            {
                Min = new PointM()
                {
                    X = box.Min.X,
                    Y = box.Min.Y,
                    M = minM
                },
                Max = new PointM()
                {
                    X = box.Max.X,
                    Y = box.Max.Y,
                    M = maxM
                }
            };

            return new MultiPartGeometry<IPointM>()
            {
                Box = boxM,
                Parts = parts
            };
        }

        public override void Write(BinaryWriter writer, IMultiPartGeometry<IPointM> value, WordCount origin)
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

            MinM(pointsOffset, partCount).Write(writer, value.Box.Min.M, origin);
            MaxM(pointsOffset, partCount).Write(writer, value.Box.Max.M, origin);
        }
    }
}
