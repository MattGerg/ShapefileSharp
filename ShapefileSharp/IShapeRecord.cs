namespace ShapefileSharp
{
    public interface IShapeRecord
    {
        IReadOnlyRecordHeader Header { get; }
        IShape Shape { get; }
    }
}
