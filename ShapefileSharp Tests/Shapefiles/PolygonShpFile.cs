using System;

namespace ShapefileSharp.Tests.Shapefiles
{
    /// <summary>
    /// A mock Polygon Shapefile with hardcoded data.
    /// </summary>
    internal sealed class PolygonShpFile : IShpFile
    {
        public const string FilePath = "Data/ne_10m_admin_0_countries.shp";

        public IShapefileHeader Header { get; } = new ShapefileHeader()
        {
            ShapeType = ShapeType.Polygon,
            BoundingBox = new BoundingBox<IPointZ>()
            {
                Min = new Point()
                {
                    X = -179.99999999999989,
                    Y = -90
                },
                Max = new Point()
                {
                    X = 180.0000000000002,
                    Y = 83.634100653000118
                }
            }
        };

        public IShpRecord GetRecord(IShxRecord indexRecord)
        {
            throw new NotImplementedException();
        }
    }
}