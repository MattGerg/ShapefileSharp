using System;

namespace ShapefileSharp.Tests.Shapefiles
{
    /// <summary>
    /// A mock Polygon Shapefile with hardcoded data.
    /// </summary>
    internal sealed class CountriesMainFile : IShpFile
    {
        public const string FilePath = "Data/ne_10m_admin_0_countries.shp";

        public IShapefileHeader Header { get; } = new ShapefileHeader()
        {
            ShapeType = ShapeType.Polygon,
            BoundingBox = new BoundingBox()
            {
                XMax = 180.0000000000002,
                XMin = -179.99999999999989,
                YMax = 83.634100653000118,
                YMin = -90
            }
        };

        public IShpRecord GetRecord(IShxRecord indexRecord)
        {
            throw new NotImplementedException();
        }
    }
}