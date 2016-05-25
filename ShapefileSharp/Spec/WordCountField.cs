using System;
using System.IO;

namespace ShapefileSharp.Spec
{
    internal sealed class WordCountField : Field<WordCount>
    {
        public WordCountField(WordCount offset) : base(offset)
        {
            Length = WordCount.FromBytes(sizeof(int));
        }

        public override WordCount Length { get; }

        public override WordCount Read(BinaryReader reader, WordCount origin)
        {
            reader.BaseStream.Position = (origin + Offset).Bytes;
            return WordCount.FromWords(reader.ReadInt32Big()); //All "Length" fields in the spec are Big endian...
        }

        public override void Write(BinaryWriter writer, WordCount value, WordCount origin)
        {
            throw new NotImplementedException();
        }
    }
}
