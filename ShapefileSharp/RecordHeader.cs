namespace ShapefileSharp
{
    internal sealed class RecordHeader : IReadOnlyRecordHeader
    {
        public int RecordNumber { get; set; }
        public ContentLength ContentLength { get; set; }
    }
}