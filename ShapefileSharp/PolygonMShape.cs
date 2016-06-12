using System.Collections.Generic;

namespace ShapefileSharp
{
    internal sealed class PolygonMShape : Shape, IPolygonShape<IPointM>
    {
        public override ShapeType ShapeType
        {
            get
            {
                return ShapeType.PolygonM;
            }
        }

        public IBoundingBox<IPointM> Box { get; set; }
        public IReadOnlyList<IMultiPointGeometry<IPointM>> Rings { get; set; }
    }
}