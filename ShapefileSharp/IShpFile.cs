namespace ShapefileSharp
{
    /// <summary>
    /// A Shapefile main (.shp) file.
    /// </summary>
    public interface IShpFile
    {
        IShapefileHeader Header { get; }
        IShapeRecord GetRecord(IShxRecord indexRecord);
    }
}
