using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;

namespace ShapefileSharp.Tests
{
    //TODO: Test that the .shx files are correct as well...
    [TestClass]
    public class ReadWriteTests
    {
        private void AssertIsContentEqual(string expectedFilePath, string actualFilePath)
        {
            var bytes1 = File.ReadAllBytes(expectedFilePath);
            var bytes2 = File.ReadAllBytes(actualFilePath);

            //Check shape content first...
            for (var i=100; i < bytes1.Length; i++)
            {
                Assert.AreEqual(bytes1[i], bytes2[i], string.Format("Position: {0}", i));
            }

            //Check header content last...
            for (var i=0; i < 100; i++)
            {
                Assert.AreEqual(bytes1[i], bytes2[i], string.Format("Position: {0}", i));
            }
        }

        [TestMethod]
        public void ReadWrite_Point()
        {
            var expected = Shapefiles.PointShpFile.FilePath;
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

        [TestMethod]
        public void ReadWrite_PolyLineM()
        {
            var expected = Shapefiles.PolyLineMShpFile.FilePath;
            var actual = "written.shp";

            var reader = new Shapefile(expected);

            using (var writer = new ShapefileWriter<IPolyLineShape<IPointM>>(actual))
            {
                foreach (var iFeature in reader.Features)
                {
                    writer.Write(iFeature.Shape);
                }
            }

            AssertIsContentEqual(expected, actual);
        }

        [TestMethod]
        public void ReadWrite_PolyLineZ()
        {
            var expected = Shapefiles.PolyLineZShpFile.FilePath;
            var actual = "written.shp";

            var reader = new Shapefile(expected);

            using (var writer = new ShapefileWriter<IPolyLineShape<IPointZ>>(actual))
            {
                foreach (var iFeature in reader.Features)
                {
                    writer.Write(iFeature.Shape);
                }
            }

            AssertIsContentEqual(expected, actual);
        }

        [TestMethod]
        public void ReadWrite_Polygon()
        {
            var expected = Shapefiles.PolygonShpFile.FilePath;
            var actual = "written.shp";

            var reader = new Shapefile(expected);

            using (var writer = new ShapefileWriter<IPolygonShape<IPoint>>(actual))
            {
                foreach (var iFeature in reader.Features)
                {
                    writer.Write(iFeature.Shape);
                }
            }

            AssertIsContentEqual(expected, actual);
        }

        [TestMethod]
        public void ReadWrite_PolygonM()
        {
            var expected = Shapefiles.PolygonMShpFile.FilePath;
            var actual = "written.shp";

            var reader = new Shapefile(expected);

            using (var writer = new ShapefileWriter<IPolygonShape<IPointM>>(actual))
            {
                foreach (var iFeature in reader.Features)
                {
                    writer.Write(iFeature.Shape);
                }
            }

            AssertIsContentEqual(expected, actual);
        }

        [TestMethod]
        public void ReadWrite_MultiPoint()
        {
            var expected = Shapefiles.MultiPointShpFile.FilePath;
            var actual = "written.shp";

            var reader = new Shapefile(expected);

            using (var writer = new ShapefileWriter<IMultiPointShape<IPoint>>(actual))
            {
                foreach (var iFeature in reader.Features)
                {
                    writer.Write(iFeature.Shape);
                }
            }

            AssertIsContentEqual(expected, actual);
        }

        [TestMethod]
        public void ReadWrite_MultiPointM()
        {
            var expected = Shapefiles.MultiPointMShpFile.FilePath;
            var actual = "written.shp";

            var reader = new Shapefile(expected);

            using (var writer = new ShapefileWriter<IMultiPointShape<IPointM>>(actual))
            {
                foreach (var iFeature in reader.Features)
                {
                    writer.Write(iFeature.Shape);
                }
            }

            AssertIsContentEqual(expected, actual);
        }

        [TestMethod]
        public void ReadWrite_MultiPointZ()
        {
            var expected = Shapefiles.MultiPointZShpFile.FilePath;
            var actual = "written.shp";

            var reader = new Shapefile(expected);

            using (var writer = new ShapefileWriter<IMultiPointShape<IPointZ>>(actual))
            {
                foreach (var iFeature in reader.Features)
                {
                    writer.Write(iFeature.Shape);
                }
            }

            AssertIsContentEqual(expected, actual);
        }

        [TestMethod]
        public void ReadWrite_PointM()
        {
            var expected = Shapefiles.PointMShpFile.FilePath;
            var actual = "written.shp";

            var reader = new Shapefile(expected);

            using (var writer = new ShapefileWriter<IPointShape<IPointM>>(actual))
            {
                foreach (var iFeature in reader.Features)
                {
                    writer.Write(iFeature.Shape);
                }
            }

            AssertIsContentEqual(expected, actual);
        }

        [TestMethod]
        public void ReadWrite_PointZ()
        {
            var expected = Shapefiles.PointZShpFile.FilePath;
            var actual = "written.shp";

            var reader = new Shapefile(expected);

            using (var writer = new ShapefileWriter<IPointShape<IPointZ>>(actual))
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
