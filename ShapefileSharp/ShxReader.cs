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
            return Convert.ToInt32((BinaryReader.BaseStream.Length - ShxSpec.Header.Length.Bytes) / ShxSpec.Record.Length.Bytes);
        }

        public IShxRecord ReadRecord(int recordIndex)
        {
            var indexRecord = new ShxRecord();

            var recordOffset = ShxSpec.Record.GetPos(recordIndex);

            var shxRecordField = new ShxRecordField(recordOffset);

            return shxRecordField.Read(BinaryReader);
        }
    }
}
