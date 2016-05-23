using System.Collections.Generic;

namespace ShapefileSharp
{
    public interface IMultiPointGeometry<T> where T:IPoint
    {
        IReadOnlyList<T> Points { get; }
    }
}
