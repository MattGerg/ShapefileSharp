using System.Collections.Generic;

namespace ShapefileSharp
{
    internal sealed class MultiPointMShape : Shape, IMultiPointShape<IPointM>
    {
        public override ShapeType ShapeType
        {
            get
            {
                return ShapeType.MultiPointM;
            }
        }

        public IBoundingBox<IPointM> Box { get; set; }
        public IReadOnlyList<IPointM> Points { get; set; }
    }
}
