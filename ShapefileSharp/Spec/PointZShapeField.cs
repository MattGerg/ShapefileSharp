using System.IO;

namespace ShapefileSharp.Spec
{
    class PointZShapeField : Field<IPointZShape>
    {
        public PointZShapeField(WordCount offset) : base(offset)
        {
            Point = new PointZField(offset);
        }

        private PointZField Point { get; }

        public override WordCount Length
        {
            get
            {
                return Point.Length;
            }
        }

        public override IPointZShape Read(BinaryReader reader, WordCount origin)
        {
            var point = Point.Read(reader, origin);

            return new PointZShape()
            {
                Point = point
            };
        }
    }
}
