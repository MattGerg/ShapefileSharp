namespace ShapefileSharp
{
    internal sealed class ShapeRecordHeader : IShpRecordHeader
    {
        public int RecordNumber { get; set; }
        public WordCount ContentLength { get; set; }
    }
}