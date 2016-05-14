namespace ShapefileSharp
{
    public sealed class ShapeRecord : IShpRecord
    {
        public IShapeRecordHeader Header { get; set; }
        public IShape Shape { get; set; }
    }
}
