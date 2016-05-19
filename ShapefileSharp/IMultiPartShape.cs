using System.Collections.Generic;

namespace ShapefileSharp
{
    public interface IMultiPartShape<TBox, TPoint> :IShape where TBox:IBoundingBox2d where TPoint:IPoint
    {
        TBox Box { get; }
        IReadOnlyList<IReadOnlyList<TPoint>> Parts { get; }
    }
}
