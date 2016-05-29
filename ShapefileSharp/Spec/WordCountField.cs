using System;
using System.IO;

namespace ShapefileSharp.Spec
{
    internal sealed class WordCountField : FixedField<WordCount>
    {
        public WordCountField(WordCount offset) : base(offset)
        {
        }

        public static readonly WordCount FieldLength = WordCount.FromBytes(sizeof(int));

        public override WordCount Length {
            get
            {
                return FieldLength;
            }
        }

        public override WordCount Read(BinaryReader reader, WordCount origin)
        {
            reader.BaseStream.Position = (origin + Offset).Bytes;
            return WordCount.FromWords(reader.ReadInt32Big()); //All "Length" fields in the spec are Big endian...
        }

        public override void Write(BinaryWriter writer, WordCount value, WordCount origin)
        {
            writer.BaseStream.Position = (origin + Offset).Bytes;
            writer.WriteInt32Big(value.Words); //All "Length" fields in the spec are Big endian...
        }
    }
}
