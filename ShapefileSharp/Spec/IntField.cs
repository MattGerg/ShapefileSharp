using System;
using System.Diagnostics;
using System.IO;

namespace ShapefileSharp.Spec
{
    internal sealed class IntField : Field<int>
    {
        public IntField(WordCount offset, Endianness endianness) : base(offset)
        {
            Length = WordCount.FromBytes(sizeof(int));
            Endianness = endianness;
        }

        public override WordCount Length { get; }
        public Endianness Endianness { get; }

        public override int Read(BinaryReader reader, WordCount origin)
        {
            reader.BaseStream.Position = (origin + Offset).Bytes;

            switch (Endianness)
            {
                case Endianness.Little:
                    return reader.ReadInt32();

                case Endianness.Big:
                    return reader.ReadInt32Big();

                default:
                    Debug.Fail("Unimplemented Endianess.");
                    throw new NotImplementedException();
            }
        }
    }
}
