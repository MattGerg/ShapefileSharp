namespace ShapefileSharp
{
    public interface IShapefile
    {
        ShapeType ShapeType { get; }
        IBoundingBox BoundingBox { get; }
    }
}
