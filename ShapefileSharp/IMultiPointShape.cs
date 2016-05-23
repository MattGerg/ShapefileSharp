using System.Collections.Generic;

namespace ShapefileSharp
{
    public interface IMultiPointShape<T> : IShape<T> where T:IPoint
    {
        IBoundingBox<T> Box { get; }
        IReadOnlyList<T> Points { get; }
    }
}
