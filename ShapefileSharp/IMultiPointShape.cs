using System.Collections.Generic;

namespace ShapefileSharp
{
    public interface IMultiPointShape<T> : IShape<T> where T:IPoint
    {
        IReadOnlyList<T> Points { get; }
    }
}
