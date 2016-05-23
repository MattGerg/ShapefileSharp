using System.Collections.Generic;

namespace ShapefileSharp
{
    internal sealed class MultiPartGeometry<T> : IMultiPartGeometry<T> where T:IPoint
    {
        public IBoundingBox<T> Box { get; set; }
        public IReadOnlyList<IMultiPointGeometry<T>> Parts { get; set; }
    }
}
