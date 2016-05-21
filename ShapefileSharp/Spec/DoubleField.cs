using System;
using System.IO;

namespace ShapefileSharp.Spec
{
    internal sealed class DoubleField : Field<double>
    {
        public DoubleField(WordCount offset) : base(offset)
        {
            Length = WordCount.FromBytes(sizeof(double));
        }

        public override WordCount Length { get; }

        public override double Read(BinaryReader reader, WordCount origin)
        {
            reader.BaseStream.Position = (origin + Offset).Bytes;
            return reader.ReadDouble();
        }
    }
}
