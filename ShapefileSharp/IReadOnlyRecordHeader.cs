namespace ShapefileSharp
{
    public interface IReadOnlyRecordHeader
    {
        int RecordNumber { get; }
        ContentLength ContentLength { get; }
    }
}
