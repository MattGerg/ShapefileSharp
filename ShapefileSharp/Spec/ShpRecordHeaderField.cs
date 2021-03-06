﻿using System;
using System.IO;

namespace ShapefileSharp.Spec
{
    internal sealed class ShpRecordHeaderField : FixedField<IShpRecordHeader>
    {
        public ShpRecordHeaderField(WordCount offset) : base(offset)
        {
            RecordNumber = new IntField(offset, Endianness.Big);
            ContentLength = new WordCountField(offset + RecordNumber.Length);
        }

        public static readonly WordCount FieldLength = IntField.FieldLength + WordCountField.FieldLength;

        public override WordCount Length
        {
            get
            {
                return FieldLength;
            }
        }

        private IntField RecordNumber { get; }
        private WordCountField ContentLength { get; }

        public override IShpRecordHeader Read(BinaryReader reader, WordCount origin)
        {
            var recordNumber = RecordNumber.Read(reader, origin);
            var contentLength = ContentLength.Read(reader, origin);

            return new ShpRecordHeader()
            {
                RecordNumber = recordNumber,
                ContentLength = contentLength
            };
        }

        public override void Write(BinaryWriter writer, IShpRecordHeader value, WordCount origin)
        {
            RecordNumber.Write(writer, value.RecordNumber, origin);
            ContentLength.Write(writer, value.ContentLength, origin);
        }
    }
}
