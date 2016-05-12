namespace ShapefileSharp
{
    internal sealed class RecordHeader : IShapeRecordHeader
    {
        public int RecordNumber { get; set; }
        public WordCount ContentLength { get; set; }
    }
}