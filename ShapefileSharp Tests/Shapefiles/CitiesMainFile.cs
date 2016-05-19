using System;

namespace ShapefileSharp.Tests.Shapefiles
{
    /// <summary>
    /// A mock Point Shapefile with hardcoded data.
    /// </summary>
    internal sealed class CitiesMainFile : IShpFile
    {
        public const string FilePath = "Data/ne_10m_populated_places_simple.shp";

        public IShapefileHeader Header { get; } = new ShapefileHeader()
        {
            ShapeType = ShapeType.Point,
            BoundingBox = new BoundingBox<IPointZ>()
            {
                Min = new Point()
                {
                    X = -179.58997888396897,
                    Y = -89.999999814387266
                },
                Max = new Point()
                {
                    X = 179.38330358817018,
                    Y = 82.483323180359434
                }
            }
        };

        public IShpRecord GetRecord(IShxRecord indexRecord)
        {
            switch (indexRecord.Offset.Words)
            {
                case 50:
                    return new ShpRecord()
                    {
                    
                    };

                default:
                    throw new NotImplementedException();
            }
        }
    }
}