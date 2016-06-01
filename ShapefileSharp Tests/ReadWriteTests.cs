using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Linq;

namespace ShapefileSharp.Tests
{
    [TestClass]
    public class ReadWriteTests
    {
        private void AssertIsContentEqual(string expectedFilePath, string actualFilePath)
        {
            var bytes1 = File.ReadAllBytes(expectedFilePath);
            var bytes2 = File.ReadAllBytes(actualFilePath);

            for (var i=0; i < bytes1.Length; i++)
            {
                Assert.AreEqual(bytes1[i], bytes2[i], string.Format("Position: {0}", i));
            }
        }

        [TestMethod]
        public void ReadWrite_Point()
        {
            var expected = Shapefiles.CitiesMainFile.FilePath;
            var actual = "written.shp";

            var reader = new Shapefile(expected);

            using (var writer = new ShapefileWriter<IPointShape<IPoint>>(actual))
            {
                foreach (var iFeature in reader.Features) {
                    writer.Write(iFeature.Shape);
                }
            }

            AssertIsContentEqual(expected, actual);
        }

        [TestMethod]
        public void ReadWrite_PolyLine()
        {
            var expected = Shapefiles.PolyLineShpFile.FilePath;
            var actual = "written.shp";

            var reader = new Shapefile(expected);

            using (var writer = new ShapefileWriter<IPolyLineShape<IPoint>>(actual))
            {
                foreach (var iFeature in reader.Features)
                {
                    writer.Write(iFeature.Shape);
                }
            }

            AssertIsContentEqual(expected, actual);
        }
    }
}
