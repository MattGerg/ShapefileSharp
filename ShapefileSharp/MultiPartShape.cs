using System.Collections.Generic;

namespace ShapefileSharp
{
    internal abstract class MultiPartShape<TBox, TPoint> : Shape, IMultiPartShape<TBox, TPoint> where TBox:IBoundingBox2d where TPoint:IPoint
    {
        public TBox Box { get; set; }
        public IReadOnlyList<IReadOnlyList<TPoint>> Parts { get; set; }
    }
}
