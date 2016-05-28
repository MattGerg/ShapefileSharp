using System;
using System.IO;

namespace ShapefileSharp.Spec
{
    internal sealed class ShxRecordField : FixedField<IShxRecord>
    {
        public ShxRecordField(WordCount offset) : base(offset)
        {
            OffsetField = new WordCountField(offset);
            ContentLengthField = new WordCountField(offset + OffsetField.Length);
        }

        public static readonly WordCount FieldLength = WordCountField.FieldLength * 2;

        public override WordCount Length
        {
            get
            {
                return FieldLength;
            }
        }

        private WordCountField OffsetField { get; }
        private WordCountField ContentLengthField { get; }

        public override IShxRecord Read(BinaryReader reader, WordCount origin)
        {
            return new ShxRecord()
            {
                ContentLength = ContentLengthField.Read(reader, origin),
                Offset = OffsetField.Read(reader, origin)
            };
        }

        public override void Write(BinaryWriter writer, IShxRecord value, WordCount origin)
        {
            ContentLengthField.Write(writer, value.ContentLength, origin);
            OffsetField.Write(writer, value.Offset, origin);
        }
    }
}
