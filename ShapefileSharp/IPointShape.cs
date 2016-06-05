namespace ShapefileSharp
{
    public interface IPointShape<out T>: IShape<T> where T:IPoint
    {
        T Point { get; }
    }
}
