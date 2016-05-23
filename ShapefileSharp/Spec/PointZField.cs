using System.IO;

namespace ShapefileSharp.Spec
{
    //TODO: Is there opportunity to inherit/abstract from PointField/PointMField?
    internal sealed class PointZField : Field<IPointZ>
    {
        public PointZField(WordCount offset) : base(offset)
        {
            X = new DoubleField(offset);
            Y = new DoubleField(X.Offset + X.Length);
            Z = new DoubleField(Y.Offset + Y.Length);
            M = new DoubleField(Z.Offset + Z.Length);
        }

        public static readonly WordCount FieldLength = DoubleField.FieldLength * 4;

        public override WordCount Length
        {
            get
            {
                return FieldLength;
            }
        }

        private DoubleField X { get; }
        private DoubleField Y { get; }
        private DoubleField Z { get; }
        private DoubleField M { get; }

        public override IPointZ Read(BinaryReader reader, WordCount origin)
        {
            return new Point()
            {
                X = X.Read(reader, origin),
                Y = Y.Read(reader, origin),
                Z = Z.Read(reader, origin),
                M = M.Read(reader, origin)
            };
        }
    }
}
