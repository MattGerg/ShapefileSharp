namespace ShapefileSharp
{
    public interface IShapefileHeader
    {
        WordCount FileLength { get; }
        ShapeType ShapeType { get; }
        IBoundingBox<IPointZ> BoundingBox { get; }
    }
}
