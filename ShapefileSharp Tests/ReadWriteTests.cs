using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Linq;

namespace ShapefileSharp.Tests
{
    [TestClass]
    public class ReadWriteTests
    {
        private bool IsContentEqual(string filePath1, string filePath2)
        {
            var bytes1 = File.ReadAllBytes(filePath1);
            var bytes2 = File.ReadAllBytes(filePath2);

            return bytes1.SequenceEqual(bytes2);
        }

        [TestMethod]
        public void ReadWrite_Point()
        {
            var expected = Shapefiles.CitiesMainFile.FilePath;
            var actual = "written.shp";

            var reader = new Shapefile(Shapefiles.CitiesMainFile.FilePath);

            using (var writer = new ShapefileWriter<IPointShape<IPoint>>(actual))
            {
                foreach (var iFeature in reader.Features) {
                    writer.Write(iFeature.Shape);
                }
            }

            Assert.IsTrue(IsContentEqual(expected, actual));
        }
    }
}
