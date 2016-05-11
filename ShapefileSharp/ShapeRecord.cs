namespace ShapefileSharp
{
    internal sealed class ShapeRecord : IShapeRecord
    {
        public IRecordHeader Header { get; set; }
        public ShapeType ShapeType { get; set; }
        public IShape Shape { get; set; }
    }
}
