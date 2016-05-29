using System.Collections.Generic;

namespace ShapefileSharp
{
    public interface IShapefile
    {
        IShapefileHeader Header { get; }
        IReadOnlyList<IShapefileRecord> Features { get; }
    }
}
