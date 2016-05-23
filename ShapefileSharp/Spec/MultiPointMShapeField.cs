using System;
using System.Collections.Generic;
using System.IO;

namespace ShapefileSharp.Spec
{
    internal sealed class MultiPointMShapeField : Field<IMultiPointShape<IPointM>>
    {
        public MultiPointMShapeField(WordCount offset, WordCount length) : base(offset)
        {
            Length = length;

            Box = new BoundingBox2dField(offset);
            NumPoints = new IntField(offset + Box.Length, Endianness.Little);
        }

        public override WordCount Length { get; }

        private BoundingBox2dField Box { get; }
        private IntField NumPoints { get; }

        private PointMField Point(int pointIndex)
        {
            var offset = NumPoints.Offset + NumPoints.Length + (pointIndex * PointMField.FieldLength);

            return new PointMField(offset);
        }

        private DoubleField MinM(int numPoints)
        {
            var offset = NumPoints.Offset + NumPoints.Length + (numPoints * PointMField.FieldLength);

            return new DoubleField(offset);
        }

        private DoubleField MaxM(int numPoints)
        {
            var offset = NumPoints.Offset + NumPoints.Length + (numPoints * PointMField.FieldLength) + DoubleField.FieldLength;

            return new DoubleField(offset);
        }

        public override IMultiPointShape<IPointM> Read(BinaryReader reader, WordCount origin)
        {
            var box = Box.Read(reader, origin);

            var numPoints = NumPoints.Read(reader, origin);
            var points = new List<IPointM>();

            for (var i = 0; i < numPoints; i++)
            {
                var point = Point(i).Read(reader, origin);

                points.Add(point);
            }

            var minM = MinM(numPoints).Read(reader, origin);
            var maxM = MaxM(numPoints).Read(reader, origin);

            var boxM = new BoundingBox<IPointM>()
            {
                Min = new Point()
                {
                    X = box.Min.X,
                    Y = box.Min.Y,
                    M = minM
                },
                Max = new Point()
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
    }
}
