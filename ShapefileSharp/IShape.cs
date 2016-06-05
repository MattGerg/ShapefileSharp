namespace ShapefileSharp
{
    public interface IShape
    {
        ShapeType ShapeType { get; }
    }

    public interface IShape<out T> : IShape where T:IPoint
    {
        IBoundingBox<T> Box { get; }
    }
}
