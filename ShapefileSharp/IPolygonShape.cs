using System.Collections.Generic;

namespace ShapefileSharp
{
    public interface IPolygonShape<T> : IShape where T:IPoint
    {
        IBoundingBox<T> Box { get; }
        IReadOnlyList<IReadOnlyList<T>> Rings { get; }
    }
}