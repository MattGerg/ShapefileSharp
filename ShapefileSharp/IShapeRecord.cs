namespace ShapefileSharp
{
    public interface IShapeRecord
    {
        IShapeRecordHeader Header { get; }
        IShape Shape { get; }
    }
}
