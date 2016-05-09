namespace ShapefileSharp
{
    internal sealed class ShapeIndexRecord : IShapeIndexRecord
    {
        public WordCount Offset { get; set; }
        public WordCount ContentLength { get; set; }
    }
}
