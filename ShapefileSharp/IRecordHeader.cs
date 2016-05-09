namespace ShapefileSharp
{
    public interface IRecordHeader
    {
        int RecordNumber { get; }
        ContentLength ContentLength { get; }
    }
}
