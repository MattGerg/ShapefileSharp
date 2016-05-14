using ShapefileSharp.Spec;

namespace ShapefileSharp
{
    internal sealed class ShxReader : FileReaderBase
    {
        public ShxReader(string shxFilePath) : base(shxFilePath)
        {
        }

        public IShxRecord ReadRecord(int recordIndex)
        {
            var indexRecord = new ShapeIndexRecord();

            var recordPos = ShxSpec.Record.GetPos(recordIndex);

            BinaryReader.BaseStream.Position = recordPos.Bytes;
            indexRecord.Offset = new WordCount(BinaryReader.ReadInt32Big());
            indexRecord.ContentLength = new WordCount(BinaryReader.ReadInt32Big());

            return indexRecord;
        }
    }
}
