namespace ShapefileSharp.Tests.Shapefiles
{
    /// <summary>
    /// A mock Polygon Shapefile with hardcoded data.
    /// </summary>
    internal sealed class Countries : IShapefile
    {
        public const string FilePath = "Data/ne_10m_admin_0_countries.shp";

        public ShapeType ShapeType { get; } = ShapeType.Polygon;
        public IReadOnlyBoundingBox BoundingBox { get; } = new BoundingBox()
        {
            XMax = 180.0000000000002,
            XMin = -179.99999999999989,
            YMax = 83.634100653000118,
            YMin = -90
        };
    }
}