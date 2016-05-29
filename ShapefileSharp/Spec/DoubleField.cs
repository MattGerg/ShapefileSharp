using System;
using System.IO;

namespace ShapefileSharp.Spec
{
    internal sealed class DoubleField : FixedField<double>
    {
        public DoubleField(WordCount offset) : base(offset)
        {
        }

        public static readonly WordCount FieldLength = WordCount.FromBytes(sizeof(double));

        public override WordCount Length
        {
            get
            {
                return FieldLength;
            }
        }

        public override double Read(BinaryReader reader, WordCount origin)
        {
            reader.BaseStream.Position = (origin + Offset).Bytes;
            return reader.ReadDouble();
        }

        public override void Write(BinaryWriter writer, double value, WordCount origin)
        {
            writer.BaseStream.Position = (origin + Offset).Bytes;
            writer.Write(value);
        }
    }
}
