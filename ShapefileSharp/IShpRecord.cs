namespace ShapefileSharp
{
    public interface IShpRecord
    {
        IShapeRecordHeader Header { get; }
        IShape Shape { get; }
    }
}
