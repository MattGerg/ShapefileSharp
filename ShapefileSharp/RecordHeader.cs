namespace ShapefileSharp
{
    internal sealed class RecordHeader : IReadOnlyRecordHeader
    {
        public int RecordNumber { get; set; }
        public int ContentLength { get; set; }
    }
}