using System.IO;

namespace ShapefileSharp.Spec
{
    internal sealed class ShpRecordHeaderField : Field<IShpRecordHeader>
    {
        public ShpRecordHeaderField(WordCount offset) : base(offset)
        {
            RecordNumber = new IntField(offset, Endianness.Big);
            ContentLength = new WordCountField(offset + RecordNumber.Length);

            Length = RecordNumber.Length + ContentLength.Length;
        }

        public override WordCount Length { get; }

        private IntField RecordNumber { get; }
        private WordCountField ContentLength { get; }

        public override IShpRecordHeader Read(BinaryReader reader, WordCount origin)
        {
            reader.BaseStream.Position = origin.Bytes;

            return new ShpRecordHeader()
            {
                RecordNumber = RecordNumber.Read(reader, origin),
                ContentLength = ContentLength.Read(reader, origin)
            };
        }
    }
}
