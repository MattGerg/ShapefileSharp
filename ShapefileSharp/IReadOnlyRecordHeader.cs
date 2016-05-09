namespace ShapefileSharp
{
    public interface IReadOnlyRecordHeader
    {
        int RecordNumber { get; }
        int ContentLength { get; }
    }
}
