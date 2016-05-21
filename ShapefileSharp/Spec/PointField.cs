using System;
using System.IO;

namespace ShapefileSharp.Spec
{
    internal sealed class PointField : Field<IPoint>
    {
        public PointField(WordCount offset) : base(offset)
        {
            X = new DoubleField(offset);
            Y = new DoubleField(offset + X.Length);

            Length = X.Length + Y.Length;
        }

        public override WordCount Length { get; }

        public DoubleField X { get; }
        public DoubleField Y { get; }

        public override IPoint Read(BinaryReader reader, WordCount origin)
        {
            return new Point()
            {
                X = X.Read(reader, origin),
                Y = Y.Read(reader, origin)
            };
        }
    }
}
