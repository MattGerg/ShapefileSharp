namespace ShapefileSharp
{
    public sealed class ShapefileHeader : IShapefileHeader
    {
        public ShapeType ShapeType { get; set; }
        public IBoundingBox<IPointZ> BoundingBox { get; set; }
    }
}
