namespace ShapefileSharp
{
    class RecordHeader : IReadOnlyRecordHeader
    {
        public int RecordNumber { get; }
        public int ContentLength { get; }
    }
}
