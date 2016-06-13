using System.Collections.Generic;

namespace ShapefileSharp
{
    internal sealed class PolyLineZShape : Shape, IPolyLineShape<IPointZ>
    {
        public override ShapeType ShapeType
        {
            get
            {
                return ShapeType.PolyLineZ;
            }
        }

        public IBoundingBox<IPointZ> Box { get; set; }
        public IReadOnlyList<IMultiPointGeometry<IPointZ>> Lines { get; set; }
    }
}