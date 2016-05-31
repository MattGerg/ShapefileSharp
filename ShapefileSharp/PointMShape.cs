using System;

namespace ShapefileSharp
{
    public sealed class PointMShape : Shape, IPointShape<IPointM>
    {
        public override ShapeType ShapeType
        {
            get
            {
                return ShapeType.PointM;
            }
        }

        public IPointM Point { get; set; }

        public IBoundingBox<IPointM> Box
        {
            get
            {
                return new BoundingBox<IPointM>(Point, Point);
            }
        }
    }
}
