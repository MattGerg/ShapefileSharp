namespace ShapefileSharp
{
    public interface IRecordHeader
    {
        int RecordNumber { get; }
        WordCount ContentLength { get; }
    }
}
