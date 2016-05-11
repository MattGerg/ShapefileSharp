using System.Collections.Generic;

namespace ShapefileSharp
{
    public interface IShapefile: IReadOnlyList<IShapeRecord>
    {
        IShapefileHeader Header { get; }
    }
}
