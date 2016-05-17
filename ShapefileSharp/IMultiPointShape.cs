using System.Collections.Generic;

namespace ShapefileSharp
{
    public interface IMultiPointShape : IShape
    {
        IBoundingBox2d Box { get; }
        IReadOnlyList<IPoint> Points { get; }
    }
}
