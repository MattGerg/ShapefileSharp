namespace ShapefileSharp
{
    internal sealed class ShapeRecordHeader : IShapeRecordHeader
    {
        public int RecordNumber { get; set; }
        public WordCount ContentLength { get; set; }
    }
}