using System.Collections.Generic;

namespace ShapefileSharp
{
    public sealed class MultiPointShape : Shape, IMultiPointShape<IPoint>
    {
        public override ShapeType ShapeType
        {
            get
            {
                return ShapeType.MultiPoint;
            }
        }

        public IBoundingBox<IPoint> Box { get; set; }
        public IReadOnlyList<IPoint> Points { get; set; }
    }
}