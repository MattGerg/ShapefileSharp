using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ShapefileSharp.Tests
{
    [TestClass]
    public class ShapefileTests
    {
        const string CountriesShapefilePath = "Data/ne_10m_admin_0_countries.shp";

        [TestMethod]
        public void Countries_ShapeType_Equals_Polygon()
        {
            var shapefile = new Shapefile(CountriesShapefilePath);

            Assert.AreEqual(ShapeType.Polygon, shapefile.ShapeType);
        }

        [TestMethod]
        public void Countries_BoundingBox_Equals()
        {
            var shapefile = new Shapefile(CountriesShapefilePath);
            var boundingBox = shapefile.BoundingBox;

            Assert.AreEqual(boundingBox.XMax, 180.0000000000002);
            Assert.AreEqual(boundingBox.XMin, -179.99999999999989);
            Assert.AreEqual(boundingBox.YMax, 83.634100653000118);
            Assert.AreEqual(boundingBox.YMin, -90);
        }
    }
}
