namespace ShapefileSharp
{
    public interface IShapefileRecord<T> where T:IShape
    {
        int RecordNumber { get; }
        T Shape { get; }
    }
}
