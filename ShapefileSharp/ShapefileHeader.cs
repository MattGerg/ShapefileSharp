using System;

namespace ShapefileSharp
{
    public sealed class ShapefileHeader : IShapefileHeader
    {
        public int FileCode { get; set; }
        public WordCount FileLength { get; set; }
        public int Version { get; set; }
        public ShapeType ShapeType { get; set; }
        public IBoundingBox<IPointZ> BoundingBox { get; set; }
    }
}
