using System.Collections.Generic;

namespace ShapefileSharp
{
    internal abstract class MultiPartShape<T> : Shape, IMultiPartShape<T> where T:IPoint
    {
        public IBoundingBox<T> Box { get; set; }
        public IReadOnlyList<IReadOnlyList<T>> Parts { get; set; }
    }
}
