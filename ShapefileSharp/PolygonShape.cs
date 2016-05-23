using System.Collections.Generic;

namespace ShapefileSharp
{
    internal sealed class PolygonShape : Shape, IPolygonShape<IPoint>
    {
        public override ShapeType ShapeType
        {
            get
            {
                return ShapeType.Polygon;
            }
        }

        public IBoundingBox<IPoint> Box { get; set; }
        public IReadOnlyList<IMultiPointGeometry<IPoint>> Rings { get; set; }
    }
}
