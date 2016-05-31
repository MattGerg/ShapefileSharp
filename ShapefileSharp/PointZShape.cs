using System;

namespace ShapefileSharp
{
    public sealed class PointZShape : Shape, IPointShape<IPointZ>
    {
        public override ShapeType ShapeType
        {
            get
            {
                return ShapeType.PointZ;
            }
        }

        public IPointZ Point { get; set; }

        public IBoundingBox<IPointZ> Box
        {
            get
            {
                return new BoundingBox<IPointZ>(Point, Point);
            }
        }
    }
}
