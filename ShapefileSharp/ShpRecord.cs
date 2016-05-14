namespace ShapefileSharp
{
    public sealed class ShpRecord : IShpRecord
    {
        public IShpRecordHeader Header { get; set; }
        public IShape Shape { get; set; }
    }
}
