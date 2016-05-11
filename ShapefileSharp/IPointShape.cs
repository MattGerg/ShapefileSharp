namespace ShapefileSharp
{
    public interface IPointShape: IShape
    {
        IPoint Point { get; }
    }
}
