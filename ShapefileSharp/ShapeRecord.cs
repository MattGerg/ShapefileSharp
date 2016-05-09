namespace ShapefileSharp
{
    internal sealed class ShapeRecord : IShapeRecord
    {
        public IReadOnlyRecordHeader Header { get; set; }
        public IShape Shape { get; set; }
    }
}
