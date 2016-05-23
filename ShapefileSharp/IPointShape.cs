namespace ShapefileSharp
{
    public interface IPointShape: IShape<IPoint>
    {
        IPoint Point { get; }
    }
}
