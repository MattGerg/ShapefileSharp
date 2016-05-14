using System.Collections.Generic;

namespace ShapefileSharp
{
    public interface IShapefile: IReadOnlyList<IShpRecord>
    {
        IShapefileHeader Header { get; }
    }
}
