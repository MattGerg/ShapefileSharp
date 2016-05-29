using System.Collections.Generic;

namespace ShapefileSharp
{
    /// <summary>
    /// A Shapefile Index (.shx) file.
    /// </summary>
    public interface IShxFile
    {
        IShapefileHeader Header { get; }
        IReadOnlyList<IShxRecord> Records { get; }
    }
}
