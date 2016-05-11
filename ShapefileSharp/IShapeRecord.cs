namespace ShapefileSharp
{
    public interface IShapeRecord
    {
        IRecordHeader Header { get; }
        IShape Shape { get; }
    }
}
