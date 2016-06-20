using System.Collections.Generic;

namespace ShapefileSharp
{
    internal sealed class MultiPointGeometry<T> : IMultiPointGeometry<T> where T : IPoint
    {
        public List<T> Points { get; } = new List<T>();

        IReadOnlyList<T> IMultiPointGeometry<T>.Points
        {
            get
            {
                return Points;
            }
        }
    }
}
