namespace ShapefileSharp
{
    public interface IPointZShape : IShape<IPointZ>
    {
        IPointZ Point { get; }
    }
}
