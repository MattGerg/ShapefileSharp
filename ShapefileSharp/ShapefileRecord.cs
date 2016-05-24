namespace ShapefileSharp
{
    internal sealed class ShapefileRecord<T> : IShapefileRecord<T> where T : IShape
    {
        public int RecordNumber { get; set; }
        public T Shape { get; set; }
    }
}
