using System;

namespace ShapefileSharp.Spec
{
    internal sealed class IntField : Field
    {
        public IntField(WordCount offset, Endianness endianness) : base(offset)
        {
            Length = WordCount.FromBytes(sizeof(int));
            Endianness = endianness;
        }

        public override WordCount Length { get; }
        public Endianness Endianness { get; }
    }
}
