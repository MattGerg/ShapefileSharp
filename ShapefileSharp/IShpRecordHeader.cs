namespace ShapefileSharp
{
    public interface IShpRecordHeader
    {
        int RecordNumber { get; }
        WordCount ContentLength { get; }
    }
}
