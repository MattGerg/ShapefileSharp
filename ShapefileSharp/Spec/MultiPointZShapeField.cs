using System.Collections.Generic;
using System.IO;

namespace ShapefileSharp.Spec
{
    internal sealed class MultiPointZShapeField : Field<IMultiPointShape<IPointZ>>
    {
        public MultiPointZShapeField(WordCount offset) : base(offset)
        {
            Box = new BoundingBox2dField(offset);
            NumPoints = new IntField(offset + Box.Length, Endianness.Little);
        }

        private BoundingBox2dField Box { get; } //TODO: We need a Box field...
        private IntField NumPoints { get; }

        private PointZField Point(int numPoints, int pointIndex)
        {
            var pointsStart = NumPoints.Offset + NumPoints.Length;
            var xOffset = pointsStart + (pointIndex * PointField.FieldLength);
            var yOffset = xOffset + DoubleField.FieldLength;

            //Points + MinZ + MaxZ + Z values
            var zOffset = OffsetZ(numPoints, pointIndex);

            //Points + MinZ + MaxZ + Z Values + MinM + MaxM + M Values
            var mOffset = OffsetM(numPoints, pointIndex);

            return new PointZField(xOffset, yOffset, zOffset, mOffset);
        }


        private WordCount OffsetMinZ(int numPoints)
        {
            //After the final Point
            var offset = NumPoints.Offset + NumPoints.Length + (numPoints * PointField.FieldLength);
            return offset;
        }

        private WordCount OffsetMaxZ(int numPoints)
        {
            var offset = OffsetMinZ(numPoints) + DoubleField.FieldLength;
            return offset;
        }

        private WordCount OffsetZ(int numPoints, int pointIndex)
        {
            //MaxZ.Offset + MaxZ.Length + Nth Z Value
            var offset = OffsetMaxZ(numPoints) + DoubleField.FieldLength + (pointIndex * DoubleField.FieldLength);
            return offset;
        }

        private DoubleField MinZ(int numPoints)
        {
            var offset = OffsetMinZ(numPoints);
            return new DoubleField(offset);
        }

        private DoubleField MaxZ(int numPoints)
        {
            var offset = OffsetMinZ(numPoints) + DoubleField.FieldLength;
            return new DoubleField(offset);
        }


        private WordCount OffsetMinM(int numPoints)
        {
            //After the final Z value
            var offset = OffsetZ(numPoints, numPoints - 1) + DoubleField.FieldLength;
            return offset;
        }

        private WordCount OffsetMaxM(int numPoints)
        {
            //MinM.Offset + MinM.Legnth
            var offset = OffsetMinM(numPoints) + DoubleField.FieldLength;
            return offset;
        }

        private WordCount OffsetM(int numPoints, int pointIndex)
        {
            //MaxM.Offset + MaxM.Legnth + Nth M Value
            var offset = OffsetMaxM(numPoints) + DoubleField.FieldLength + (pointIndex * DoubleField.FieldLength);
            return offset;
        }

        private DoubleField MinM(int numPoints)
        {
            var offset = OffsetMinM(numPoints);
            return new DoubleField(offset);
        }

        private DoubleField MaxM(int numPoints)
        {
            var offset = OffsetMaxM(numPoints);
            return new DoubleField(offset);
        }


        public override IMultiPointShape<IPointZ> Read(BinaryReader reader, WordCount origin)
        {
            var box = Box.Read(reader, origin);

            var numPoints = NumPoints.Read(reader, origin);
            var points = new List<IPointZ>();

            for (var i = 0; i < numPoints; i++)
            {
                var point = Point(numPoints, i).Read(reader, origin);

                points.Add(point);
            }

            var minZ = MinZ(numPoints).Read(reader, origin);
            var maxZ = MaxZ(numPoints).Read(reader, origin);

            var minM = MinM(numPoints).Read(reader, origin);
            var maxM = MaxM(numPoints).Read(reader, origin);

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

            return new MultiPointZShape()
            {
                Box = boxZ,
                Points = points
            };
        }

        public override void Write(BinaryWriter writer, IMultiPointShape<IPointZ> value, WordCount origin)
        {
            Box.Write(writer, value.Box, origin);

            NumPoints.Write(writer, value.Points.Count, origin);

            for (int i = 0; i < value.Points.Count; i++)
            {
                Point(value.Points.Count, i).Write(writer, value.Points[i], origin);
            }

            MinZ(value.Points.Count).Write(writer, value.Box.Min.Z, origin);
            MaxZ(value.Points.Count).Write(writer, value.Box.Max.Z, origin);

            MinM(value.Points.Count).Write(writer, value.Box.Min.M, origin);
            MaxM(value.Points.Count).Write(writer, value.Box.Max.M, origin);
        }
    }
}
