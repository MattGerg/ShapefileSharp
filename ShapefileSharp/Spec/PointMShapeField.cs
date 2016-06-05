using System;
using System.IO;

namespace ShapefileSharp.Spec
{
    internal sealed class PointMShapeField : Field<IPointShape<IPointM>>
    {
        public PointMShapeField(WordCount offset) : base(offset)
        {
            Point = new PointMField(offset);
        }

        private PointMField Point { get; }

        public override IPointShape<IPointM> Read(BinaryReader reader, WordCount origin)
        {
            var point = Point.Read(reader, origin);

            return new PointMShape()
            {
                Point = point
            };
        }

        public override void Write(BinaryWriter writer, IPointShape<IPointM> value, WordCount origin)
        {
            Point.Write(writer, value.Point, origin);
        }
    }
}
