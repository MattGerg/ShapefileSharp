using System;

namespace ShapefileSharp
{
    public sealed class ShapefileHeader : IShapefileHeader
    {
        public ShapefileHeader() : this(new BoundingBox<IPointZ>(new Point(), new Point()))
        {
        }

        public ShapefileHeader(IBoundingBox<IPointZ> box) : base()
        {
            BoundingBox = box;
        }

        public int FileCode { get; set; }
        public WordCount FileLength { get; set; }
        public int Version { get; set; }
        public ShapeType ShapeType { get; set; }
        public IBoundingBox<IPointZ> BoundingBox { get; set; }
    }
}
