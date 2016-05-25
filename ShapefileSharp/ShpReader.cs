using ShapefileSharp.Spec;

namespace ShapefileSharp
{
    internal sealed class ShpReader : FileReaderBase
    {
        public ShpReader(string shpFilePath) : base(shpFilePath)
        {
        }

        public IShpRecord ReadShapeRecord(IShxRecord indexRecord)
        {
            var shapeRecordField = new ShpRecordField(indexRecord.Offset);

            return shapeRecordField.Read(BinaryReader);
        }
    }
}
