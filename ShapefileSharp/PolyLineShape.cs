using System.Collections.Generic;

namespace ShapefileSharp
{
    internal sealed class PolyLineShape : Shape, IPolyLineShape<IPoint>
    {
        public override ShapeType ShapeType
        {
            get
            {
                return ShapeType.PolyLine;
            }
        }

        public IBoundingBox<IPoint> Box { get; set; }
        public IReadOnlyList<IMultiPointGeometry<IPoint>> Lines { get; set; }
    }
}