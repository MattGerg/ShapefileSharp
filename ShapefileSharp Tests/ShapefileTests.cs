using Microsoft.VisualStudio.TestTools.UnitTesting;
using ShapefileSharp.Tests.Shapefiles;

namespace ShapefileSharp.Tests
{
    [TestClass]
    public class ShapefileTests
    {
        private readonly Countries Countries = new Countries();

        [TestMethod]
        public void ShapeType_Equals()
        {
            var shapefile = new Shapefile(Countries.FilePath);

            Assert.AreEqual(Countries.ShapeType, shapefile.ShapeType);
        }

        [TestMethod]
        public void BoundingBox_Equals()
        {
            var shapefile = new Shapefile(Countries.FilePath);

            Assert.AreEqual(shapefile.BoundingBox, Countries.BoundingBox);
        }
    }
}
