namespace ShapefileSharp
{
    interface IReadOnlyRecordHeader
    {
        int RecordNumber { get; }
        int ContentLength { get; }
    }
}
