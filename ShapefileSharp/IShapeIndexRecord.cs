namespace ShapefileSharp
{
    public interface IShapeIndexRecord
    {
        WordCount Offset { get; }
        WordCount ContentLength { get; }
    }
}
