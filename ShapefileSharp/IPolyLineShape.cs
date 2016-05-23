﻿using System.Collections.Generic;

namespace ShapefileSharp
{
    public interface IPolyLineShape<T> : IShape<T> where T:IPoint
    {
        IBoundingBox<T> Box { get; }
        IReadOnlyList<IMultiPointGeometry<T>> Lines { get; }
    }
}
