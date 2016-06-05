using System;
using System.IO;

namespace ShapefileSharp.Spec
{
    //TODO: Is there opportunity to inherit/abstract from PointField?
    internal sealed class PointMField : FixedField<IPointM>
    {
        public PointMField(WordCount offset) : this(offset, offset + (2 * DoubleField.FieldLength))
        {
        }

        public PointMField(WordCount xOffset, WordCount mOffset) : base(xOffset)
        {
            X = new DoubleField(xOffset);
            Y = new DoubleField(X.Offset + X.Length);
            M = new DoubleField(mOffset);
        }

        public static readonly WordCount FieldLength = DoubleField.FieldLength * 3;

        public override WordCount Length
        {
            get
            {
                return FieldLength;
            }
        }

        private DoubleField X { get; }
        private DoubleField Y { get; }
        private DoubleField M { get; }

        public override IPointM Read(BinaryReader reader, WordCount origin)
        {
            return new PointM()
            {
                X = X.Read(reader, origin),
                Y = Y.Read(reader, origin),
                M = M.Read(reader, origin)
            };
        }

        public override void Write(BinaryWriter writer, IPointM value, WordCount origin)
        {
            throw new NotImplementedException();
        }
    }
}
