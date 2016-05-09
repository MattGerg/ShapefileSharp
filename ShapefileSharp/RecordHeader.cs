namespace ShapefileSharp
{
    internal sealed class RecordHeader : IRecordHeader
    {
        public int RecordNumber { get; set; }
        public WordCount ContentLength { get; set; }
    }
}