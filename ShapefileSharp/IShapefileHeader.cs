namespace ShapefileSharp
{
    public interface IShapefileHeader
    {
        int FileCode { get; }
        WordCount FileLength { get; }
        int Version { get; }
        ShapeType ShapeType { get; }
        IBoundingBox<IPointZ> BoundingBox { get; }
    }
}
