namespace ShapefileSharp
{
    public interface IShapeRecord
    {
        IRecordHeader Header { get; }
        ShapeType ShapeType { get; }
        IShape Shape { get; }
    }
}
