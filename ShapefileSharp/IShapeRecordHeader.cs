namespace ShapefileSharp
{
    public interface IShapeRecordHeader
    {
        int RecordNumber { get; }
        WordCount ContentLength { get; }
    }
}
