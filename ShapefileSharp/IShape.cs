namespace ShapefileSharp
{
    public interface IShape
    {
        ShapeType ShapeType { get; }
    }

    public interface IShape<T> : IShape where T:IPoint
    {
    }
}
