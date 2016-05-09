namespace ShapefileSharp.Tests.Shapefiles
{
    /// <summary>
    /// A mock Point Shapefile with hardcoded data.
    /// </summary>
    internal sealed class Cities : IShapefile
    {
        public const string FilePath = "Data/ne_10m_populated_places_simple.shp";

        public ShapeType ShapeType { get; } = ShapeType.Point;
        public IBoundingBox BoundingBox { get; } = new BoundingBox()
        {
            XMax = 179.38330358817018,
            XMin = -179.58997888396897,
            YMax = 82.483323180359434,
            YMin = -89.999999814387266
        };
    }
}