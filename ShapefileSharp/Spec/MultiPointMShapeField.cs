using System.Collections.Generic;
using System.IO;

namespace ShapefileSharp.Spec
{
    internal sealed class MultiPointMShapeField : Field<IMultiPointShape<IPointM>>
    {
        public MultiPointMShapeField(WordCount offset) : base(offset)
        {
            Box = new BoundingBox2dField(offset);
            NumPoints = new IntField(offset + Box.Length, Endianness.Little);
        }

        private BoundingBox2dField Box { get; } //TODO: We need a BoxM field...
        private IntField NumPoints { get; }

        //TODO: Combine the Box/MinM/MaxM fields like this...?
        private PointMField Point(int numPoints, int pointIndex)
        {
            var pointsStart = NumPoints.Offset + NumPoints.Length;
            var xOffset = pointsStart + (pointIndex * PointField.FieldLength);

            //Points + MinM + MaxM + M values
            var mOffset = pointsStart + (numPoints * PointField.FieldLength) + (2 * DoubleField.FieldLength) + (pointIndex * DoubleField.FieldLength);

            return new PointMField(xOffset, mOffset);
        }

        private DoubleField MinM(int numPoints)
        {
            var offset = OffsetMinM(numPoints);
            return new DoubleField(offset);
        }

        private WordCount OffsetMinM(int numPoints)
        {
            var offset = NumPoints.Offset + NumPoints.Length + (numPoints * PointField.FieldLength);
            return offset;
        }

        private DoubleField MaxM(int numPoints)
        {
            var offset = OffsetMaxM(numPoints);
            return new DoubleField(offset);
        }

        private WordCount OffsetMaxM(int numPoints)
        {
            var offset = OffsetMinM(numPoints) + DoubleField.FieldLength;
            return offset;
        }

        public override IMultiPointShape<IPointM> Read(BinaryReader reader, WordCount origin)
        {
            var box = Box.Read(reader, origin);

            var numPoints = NumPoints.Read(reader, origin);
            var points = new List<IPointM>();

            for (var i = 0; i < numPoints; i++)
            {
                var point = Point(numPoints, i).Read(reader, origin);

                points.Add(point);
            }

            var minM = MinM(numPoints).Read(reader, origin);
            var maxM = MaxM(numPoints).Read(reader, origin);

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

            return new MultiPointMShape()
            {
                Box = boxM,
                Points = points
            };
        }

        public override void Write(BinaryWriter writer, IMultiPointShape<IPointM> value, WordCount origin)
        {
            Box.Write(writer, value.Box, origin);

            NumPoints.Write(writer, value.Points.Count, origin);

            for (int i = 0; i < value.Points.Count; i++)
            {
                Point(value.Points.Count, i).Write(writer, value.Points[i], origin);
            }

            MinM(value.Points.Count).Write(writer, value.Box.Min.M, origin);
            MaxM(value.Points.Count).Write(writer, value.Box.Max.M, origin);
        }
    }
}
