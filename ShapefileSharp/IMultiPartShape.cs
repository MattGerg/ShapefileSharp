using System.Collections.Generic;

namespace ShapefileSharp
{
    public interface IMultiPartShape<T> :IShape where T:IPoint
    {
        IBoundingBox<T> Box { get; }
        IReadOnlyList<IReadOnlyList<T>> Parts { get; }
    }
}
