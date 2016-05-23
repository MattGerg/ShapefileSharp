namespace ShapefileSharp
{
    public interface IPointMShape : IShape<IPointM>
    {
        IPointM Point { get; }
    }
}
