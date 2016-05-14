namespace ShapefileSharp
{
    public sealed class ShpRecord : IShpRecord
    {
        public IShapeRecordHeader Header { get; set; }
        public IShape Shape { get; set; }
    }
}
