using System;
using System.IO;

namespace ShapefileSharp.Spec
{
    internal sealed class PointField : FixedField<IPoint>
    {
        public PointField(WordCount offset) : base(offset)
        {
            X = new DoubleField(offset);
            Y = new DoubleField(offset + X.Length);
        }

        public static readonly WordCount FieldLength = DoubleField.FieldLength * 2;

        public override WordCount Length
        {
            get
            {
                return FieldLength;
            }
        }

        private DoubleField X { get; }
        private DoubleField Y { get; }

        public override IPoint Read(BinaryReader reader, WordCount origin)
        {
            return new Point()
            {
                X = X.Read(reader, origin),
                Y = Y.Read(reader, origin)
            };
        }

        public override void Write(BinaryWriter writer, IPoint value, WordCount origin)
        {
            X.Write(writer, value.X, origin);
            Y.Write(writer, value.Y, origin);
        }
    }
}
