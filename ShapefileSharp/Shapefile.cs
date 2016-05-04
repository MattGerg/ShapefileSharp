using System.IO;

namespace ShapefileSharp
{
    public class Shapefile
    {
        public Shapefile(string filePath)
        {
            using (var fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                using (var binaryReader = new BinaryReader(fs))
                {
                    fs.Seek(ShapefileSpec.ShapeTypePos, SeekOrigin.Begin);
                    ShapeType = (ShapeType) binaryReader.ReadInt32();

                    var boundingBox = new BoundingBox();

                    fs.Seek(ShapefileSpec.BoundingBoxXMinPos, SeekOrigin.Begin);
                    boundingBox.XMin = binaryReader.ReadDouble();

                    fs.Seek(ShapefileSpec.BoundingBoxXMaxPos, SeekOrigin.Begin);
                    boundingBox.XMax = binaryReader.ReadDouble();

                    fs.Seek(ShapefileSpec.BoundingBoxYMinPos, SeekOrigin.Begin);
                    boundingBox.YMin = binaryReader.ReadDouble();

                    fs.Seek(ShapefileSpec.BoundingBoxYMaxPos, SeekOrigin.Begin);
                    boundingBox.YMax = binaryReader.ReadDouble();

                    fs.Seek(ShapefileSpec.BoundingBoxZMinPos, SeekOrigin.Begin);
                    boundingBox.ZMin = binaryReader.ReadDouble();

                    fs.Seek(ShapefileSpec.BoundingBoxZMaxPos, SeekOrigin.Begin);
                    boundingBox.ZMax = binaryReader.ReadDouble();

                    fs.Seek(ShapefileSpec.BoundingBoxMMinPos, SeekOrigin.Begin);
                    boundingBox.MMin = binaryReader.ReadDouble();

                    fs.Seek(ShapefileSpec.BoundingBoxMMaxPos, SeekOrigin.Begin);
                    boundingBox.MMax = binaryReader.ReadDouble();

                    BoundingBox = boundingBox;
                }                    
            }
        }

        public ShapeType ShapeType { get; }
        public IReadOnlyBoundingBox BoundingBox { get; }
    }
}
