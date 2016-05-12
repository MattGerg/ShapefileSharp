namespace ShapefileSharp
{
    public sealed class ShapeRecord : IShapeRecord
    {
        public IShapeRecordHeader Header { get; set; }
        public IShape Shape { get; set; }
    }
}
