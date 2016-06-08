using System.Collections.Generic;

namespace ShapefileSharp
{
    internal sealed class PolyLineMShape : Shape, IPolyLineShape<IPointM>
    {
        public override ShapeType ShapeType
        {
            get
            {
                return ShapeType.PolyLineM;
            }
        }

        public IBoundingBox<IPointM> Box { get; set; }
        public IReadOnlyList<IMultiPointGeometry<IPointM>> Lines { get; set; }
    }
}