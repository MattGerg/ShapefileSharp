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

            //TODO: Override BoundingBox.Equals.  Use here.  Create a Unit Test for it.
            Assert.AreEqual(shapefile.BoundingBox.XMax, Countries.BoundingBox.XMax);
            Assert.AreEqual(shapefile.BoundingBox.XMin, Countries.BoundingBox.XMin);
            Assert.AreEqual(shapefile.BoundingBox.YMax, Countries.BoundingBox.YMax);
            Assert.AreEqual(shapefile.BoundingBox.YMin, Countries.BoundingBox.YMin);
        }
    }
}
