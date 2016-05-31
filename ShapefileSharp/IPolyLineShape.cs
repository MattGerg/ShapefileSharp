using System.Collections.Generic;

namespace ShapefileSharp
{
    public interface IPolyLineShape<T> : IShape<T> where T:IPoint
    {
        IReadOnlyList<IMultiPointGeometry<T>> Lines { get; }
    }
}
