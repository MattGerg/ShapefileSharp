namespace ShapefileSharp
{
    public interface IShapefileRecord
    {
        int RecordNumber { get; }
        IShape Shape { get; }
    }

    public interface IShapefileRecord<T> : IShapefileRecord where T:IShape
    {
        new T Shape { get; }
    }
}