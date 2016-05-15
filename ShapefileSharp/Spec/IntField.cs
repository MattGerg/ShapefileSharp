using System;

namespace ShapefileSharp.Spec
{
    internal sealed class IntField : Field
    {
        public IntField(WordCount offset, Endianness endianness) : base(offset, WordCount.FromBytes(sizeof(int)))
        {
            Endianness = endianness;
        }

        public Endianness Endianness { get; }
    }
}
