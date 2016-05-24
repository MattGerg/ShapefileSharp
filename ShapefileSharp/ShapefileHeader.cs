namespace ShapefileSharp
{
    public sealed class ShapefileHeader : IShapefileHeader
    {
        public WordCount FileLength { get; set; }
        public ShapeType ShapeType { get; set; }
        public IBoundingBox<IPointZ> BoundingBox { get; set; }
    }
}
