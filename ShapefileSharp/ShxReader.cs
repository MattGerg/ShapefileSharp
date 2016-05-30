using ShapefileSharp.Spec;
using System;

namespace ShapefileSharp
{
    internal sealed class ShxReader : FileReaderBase
    {
        public ShxReader(string shxFilePath) : base(shxFilePath)
        {
        }

        public int GetRecordCount()
        {
            return Convert.ToInt32((BinaryReader.BaseStream.Length - ShapefileHeaderField.FieldLength.Bytes) / ShxRecordField.FieldLength.Bytes);
        }

        private WordCount GetRecordOffset(int recordIndex)
        {
            return ShapefileHeaderField.FieldLength + (recordIndex * ShxRecordField.FieldLength);
        }

        public IShxRecord ReadRecord(int recordIndex)
        {
            var recordOffset = GetRecordOffset(recordIndex);

            var shxRecordField = new ShxRecordField(recordOffset);

            return shxRecordField.Read(BinaryReader);
        }
    }
}
