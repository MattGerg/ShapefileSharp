namespace ShapefileSharp
{
    public interface IShapefile
    {
        ShapeType ShapeType { get; }
        IReadOnlyBoundingBox BoundingBox { get; }
    }
}
