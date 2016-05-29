using System;
using System.Diagnostics;
using System.IO;

namespace ShapefileSharp.Spec
{
    internal sealed class IntField : FixedField<int>
    {
        public IntField(WordCount offset, Endianness endianness) : base(offset)
        {
            Endianness = endianness;
        }

        public static readonly WordCount FieldLength = WordCount.FromBytes(sizeof(int));

        public override WordCount Length {
            get
            {
                return FieldLength;
            }
        }

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

        public override void Write(BinaryWriter writer, int value, WordCount origin)
        {
            writer.BaseStream.Position = (origin + Offset).Bytes;

            switch (Endianness) {
                case Endianness.Little:
                    writer.Write(value);
                    return;

                case Endianness.Big:
                    writer.WriteInt32Big(value);
                    return;

                default:
                    Debug.Fail("Unimplemented Endianess.");
                    throw new NotImplementedException();

            }
        }
    }
}
