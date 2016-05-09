namespace ShapefileSharp
{
    internal sealed class RecordHeader : IRecordHeader
    {
        public int RecordNumber { get; set; }
        public ContentLength ContentLength { get; set; }
    }
}