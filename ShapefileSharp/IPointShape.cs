namespace ShapefileSharp
{
    public interface IPointShape<T>: IShape<T> where T:IPoint
    {
        T Point { get; }
    }
}
