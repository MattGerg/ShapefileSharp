namespace ShapefileSharp
{
    public interface IShapefileHeader
    {
        ShapeType ShapeType { get; }
        IBoundingBox<IPointZ> BoundingBox { get; }
    }
}
