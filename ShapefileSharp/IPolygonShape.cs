using System.Collections.Generic;

namespace ShapefileSharp
{
    public interface IPolygonShape<T> : IShape<T> where T:IPoint
    {
        IBoundingBox<T> Box { get; }
        IReadOnlyList<IMultiPointGeometry<T>> Rings { get; }
    }
}