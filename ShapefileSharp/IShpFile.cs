namespace ShapefileSharp
{
    /// <summary>
    /// A Shapefile main (.shp) file.
    /// </summary>
    public interface IShpFile
    {
        IShapefileHeader Header { get; }
        IShpRecord GetRecord(IShxRecord indexRecord);
    }
}
