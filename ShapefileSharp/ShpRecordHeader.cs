namespace ShapefileSharp
{
    internal sealed class ShpRecordHeader : IShpRecordHeader
    {
        public int RecordNumber { get; set; }
        public WordCount ContentLength { get; set; }
    }
}