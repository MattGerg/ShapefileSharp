namespace ShapefileSharp
{
    public interface IShapefileHeader
    {
        ShapeType ShapeType { get; }
        IBoundingBox BoundingBox { get; }
    }
}
