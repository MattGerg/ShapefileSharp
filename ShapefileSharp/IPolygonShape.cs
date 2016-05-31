using System.Collections.Generic;

namespace ShapefileSharp
{
    public interface IPolygonShape<T> : IShape<T> where T:IPoint
    {
        IReadOnlyList<IMultiPointGeometry<T>> Rings { get; }
    }
}