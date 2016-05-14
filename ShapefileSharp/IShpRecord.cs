namespace ShapefileSharp
{
    public interface IShpRecord
    {
        IShpRecordHeader Header { get; }
        IShape Shape { get; }
    }
}
