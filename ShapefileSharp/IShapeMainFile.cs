namespace ShapefileSharp
{
    public interface IShapeMainFile
    {
        IShapefileHeader Header { get; }
        IShapeRecord GetRecord(IShapeIndexRecord indexRecord);
    }
}
