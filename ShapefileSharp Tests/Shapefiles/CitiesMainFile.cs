using System;

namespace ShapefileSharp.Tests.Shapefiles
{
    /// <summary>
    /// A mock Point Shapefile with hardcoded data.
    /// </summary>
    internal sealed class CitiesMainFile : IShapeMainFile
    {
        public const string FilePath = "Data/ne_10m_populated_places_simple.shp";

        public IShapefileHeader Header { get; } = new ShapefileHeader()
        {
            ShapeType = ShapeType.Point,
            BoundingBox = new BoundingBox()
            {
                XMax = 179.38330358817018,
                XMin = -179.58997888396897,
                YMax = 82.483323180359434,
                YMin = -89.999999814387266
            }
        };

        public IShapeRecord GetRecord(IShapeIndexRecord indexRecord)
        {
            switch (indexRecord.Offset.Words)
            {
                case 50:
                    return new ShapeRecord()
                    {
                    
                    };

                default:
                    throw new NotImplementedException();
            }
        }
    }
}