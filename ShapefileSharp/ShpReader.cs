using ShapefileSharp.Spec;
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
            var header = ShpSpec.Record.Header.Read(BinaryReader, indexRecord.Offset);

            BinaryReader.BaseStream.Position = (indexRecord.Offset + ShpSpec.Record.Contents.Offset).Bytes;
            var contents = BinaryReader.ReadBytes(header.ContentLength.Bytes);
            var shape = ShapeSerializer.Deserialize(contents);

            return new ShpRecord()
            {
                Header = header,
                Shape = shape
            };
        }
    }
}
