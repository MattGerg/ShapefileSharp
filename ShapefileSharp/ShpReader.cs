using ShapefileSharp.Spec;
using System.Diagnostics;
using System.IO;

namespace ShapefileSharp
{
    internal sealed class ShpReader : FileReaderBase
    {
        public ShpReader(string shpFilePath) : base(shpFilePath)
        {
        }

        private readonly ShapeSerializer ShapeSerializer = new ShapeSerializer();

        public IShpRecord ReadShapeRecord(IShxRecord indexRecord)
        {
            var shapeRecord = new ShpRecord()
            {
                Header = ReadShapeHeader(indexRecord),
            };

            BinaryReader.BaseStream.Position = (indexRecord.Offset + ShpSpec.Record.Contents.Offset).Bytes;
            var contents = BinaryReader.ReadBytes(shapeRecord.Header.ContentLength.Bytes);

            shapeRecord.Shape = ShapeSerializer.Deserialize(contents);

            return shapeRecord;
        }

        private IShpRecordHeader ReadShapeHeader(IShxRecord indexRecord)
        {
            return new ShpRecordHeader()
            {
                RecordNumber = ReadField(ShpSpec.Record.Header.RecordNumber, indexRecord.Offset),
                //TODO: Just make a WordCount field?
                ContentLength = WordCount.FromWords(ReadField(ShpSpec.Record.Header.ContentLength, indexRecord.Offset))
            };
        }
    }
}
