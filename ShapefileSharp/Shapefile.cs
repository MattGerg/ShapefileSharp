using System.IO;

namespace ShapefileSharp
{
    public class Shapefile : IShapefile
    {
        public Shapefile(string filePath)
        {
            using (var fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                using (var shapefileReader = new ShapefileReader(fs))
                {
                    Header = shapefileReader.ReadHeader();
                }                    
            }
        }

        public IShapefileHeader Header { get; }
    }
}
