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

            var recordPos = ShxSpec.Record.GetPos(recordIndex);

            BinaryReader.BaseStream.Position = recordPos.Bytes;
            indexRecord.Offset = new WordCount(BinaryReader.ReadInt32Big());
            indexRecord.ContentLength = new WordCount(BinaryReader.ReadInt32Big());

            return indexRecord;
        }
    }
}
