using System.Collections.Generic;

namespace ShapefileSharp
{
    internal interface IMultiPartGeometry<T> where T:IPoint
    {
        IBoundingBox<T> Box { get; }
        IReadOnlyList<IMultiPointGeometry<T>> Parts { get; }
    }
}
