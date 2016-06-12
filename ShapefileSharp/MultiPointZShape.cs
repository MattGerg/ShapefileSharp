using System.Collections.Generic;

namespace ShapefileSharp
{
    internal sealed class MultiPointZShape : Shape, IMultiPointShape<IPointZ>
    {
        public override ShapeType ShapeType
        {
            get
            {
                return ShapeType.MultiPointZ;
            }
        }

        public IBoundingBox<IPointZ> Box { get; set; }
        public IReadOnlyList<IPointZ> Points { get; set; }
    }
}
