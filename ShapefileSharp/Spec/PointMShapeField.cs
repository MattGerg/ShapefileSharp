using System;
using System.IO;

namespace ShapefileSharp.Spec
{
    class PointMShapeField : Field<IPointMShape>
    {
        public PointMShapeField(WordCount offset) : base(offset)
        {
            Point = new PointMField(offset);
        }

        private PointMField Point { get; }

        public override WordCount Length
        {
            get
            {
                return Point.Length;
            }
        }

        public override IPointMShape Read(BinaryReader reader, WordCount origin)
        {
            var point = Point.Read(reader, origin);

            return new PointMShape()
            {
                Point = point
            };
        }
    }
}
