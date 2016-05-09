using Microsoft.VisualStudio.TestTools.UnitTesting;
using ShapefileSharp.Tests.Shapefiles;

namespace ShapefileSharp.Tests
{
    [TestClass]
    public class ShapefileTests
    {
        private readonly IShapefile CitiesExpected = new Cities();
        private readonly IShapefile CitiesActual = new Shapefile(Cities.FilePath);

        private readonly IShapefile CountriesExpected = new Countries();
        private readonly IShapefile CountriesActual = new Shapefile(Countries.FilePath);

        [TestMethod]
        public void ShapeType_Equals()
        {
            Assert.AreEqual(CitiesExpected.Header.ShapeType, CitiesActual.Header.ShapeType);
            Assert.AreEqual(CountriesExpected.Header.ShapeType, CountriesActual.Header.ShapeType);
        }

        [TestMethod]
        public void BoundingBox_Equals()
        {
            Assert.AreEqual(CitiesExpected.Header.BoundingBox, CitiesActual.Header.BoundingBox);
            Assert.AreEqual(CountriesExpected.Header.BoundingBox, CountriesActual.Header.BoundingBox);
        }
    }
}
