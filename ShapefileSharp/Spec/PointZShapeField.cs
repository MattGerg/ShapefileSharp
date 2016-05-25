using System;
using System.IO;

namespace ShapefileSharp.Spec
{
    internal sealed class PointZShapeField : Field<IPointShape<IPointZ>>
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

        public override IPointShape<IPointZ> Read(BinaryReader reader, WordCount origin)
        {
            var point = Point.Read(reader, origin);

            return new PointZShape()
            {
                Point = point
            };
        }

        public override void Write(BinaryWriter writer, IPointShape<IPointZ> value, WordCount origin)
        {
            throw new NotImplementedException();
        }
    }
}
