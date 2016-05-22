using System.IO;

namespace ShapefileSharp.Spec
{
    //TODO: Is there opportunity to inherit/abstract from PointField?
    internal sealed class PointMField : Field<IPointM>
    {
        public PointMField(WordCount offset) : base(offset)
        {
            X = new DoubleField(offset);
            Y = new DoubleField(X.Offset + X.Length);
            M = new DoubleField(Y.Offset + Y.Length);
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
            return new Point()
            {
                X = X.Read(reader, origin),
                Y = Y.Read(reader, origin),
                M = M.Read(reader, origin)
            };
        }
    }
}
