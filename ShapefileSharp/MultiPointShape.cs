using System.Collections.Generic;

namespace ShapefileSharp
{
    public sealed class MultiPointShape : Shape, IMultiPointShape
    {
        public override ShapeType ShapeType
        {
            get
            {
                return ShapeType.MultiPoint;
            }
        }

        public IBoundingBox2d Box { get; set; }
        public IReadOnlyList<IPoint> Points { get; set; }
    }
}