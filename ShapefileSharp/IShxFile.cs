using System.Collections.Generic;

namespace ShapefileSharp
{
    /// <summary>
    /// A Shapefile Index (.shx) file.
    /// </summary>
    public interface IShxFile : IReadOnlyList<IShapeIndexRecord>
    {
        //TODO: Add accessors for header information...
    }
}
