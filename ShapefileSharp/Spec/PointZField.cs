using System;
using System.IO;

namespace ShapefileSharp.Spec
{
    //TODO: Is there opportunity to inherit/abstract from PointField/PointMField?
    internal sealed class PointZField : FixedField<IPointZ>
    {
        /// <summary>
        /// The offset of the X value, when the XYZM values are contiguous.
        /// </summary>
        /// <param name="offset"></param>
        public PointZField(WordCount offset) : this(offset, offset + DoubleField.FieldLength, offset + (2 * DoubleField.FieldLength), offset + (3 * DoubleField.FieldLength))
        {
        }

        public PointZField(WordCount xOffset, WordCount yOffset, WordCount zOffset, WordCount mOffset) : base(xOffset)
        {
            X = new DoubleField(xOffset);
            Y = new DoubleField(yOffset);
            Z = new DoubleField(zOffset);
            M = new DoubleField(mOffset);
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

        public override void Write(BinaryWriter writer, IPointZ value, WordCount origin)
        {
            throw new NotImplementedException();
        }
    }
}
