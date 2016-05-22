using System.Collections.Generic;

namespace ShapefileSharp
{
    public interface IPolyLineShape<T> : IShape where T:IPoint
    {
        IBoundingBox<T> Box { get; }
        IReadOnlyList<IReadOnlyList<T>> Lines { get; }
    }
}
