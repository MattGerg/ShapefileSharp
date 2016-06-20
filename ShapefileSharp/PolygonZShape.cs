using System.Collections.Generic;

namespace ShapefileSharp
{
    internal sealed class PolygonZShape : Shape, IPolygonShape<IPointZ>
    {
        public override ShapeType ShapeType
        {
            get
            {
                return ShapeType.PolygonZ;
            }
        }

        public IBoundingBox<IPointZ> Box { get; set; }
        public IReadOnlyList<IMultiPointGeometry<IPointZ>> Rings { get; set; }
    }
}