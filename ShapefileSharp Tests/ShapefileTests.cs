using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ShapefileSharp.Tests
{
    [TestClass]
    public class ShapefileTests
    {
        [TestMethod]
        public void ShapeType_Equals_Polygon()
        {
            var shapefile = new Shapefile("Data/ne_10m_admin_0_countries.shp");

            Assert.AreEqual(ShapeType.Polygon, shapefile.ShapeType);
        }
    }
}
