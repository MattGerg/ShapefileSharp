using System.Collections.Generic;

namespace ShapefileSharp
{
    public interface IShapefile: IReadOnlyList<IShapefileRecord>
    {
        IShapefileHeader Header { get; }
    }
}
