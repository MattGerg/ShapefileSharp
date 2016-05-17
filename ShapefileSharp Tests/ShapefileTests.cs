using Microsoft.VisualStudio.TestTools.UnitTesting;
using ShapefileSharp.Tests.Shapefiles;
using System.Linq;

namespace ShapefileSharp.Tests
{
    [TestClass]
    public class ShapefileTests
    {
        Shapefile CitiesActual = new Shapefile(CitiesMainFile.FilePath);
        Shapefile MultiPointActual = new Shapefile(MultiPointShpFile.FilePath);

        [TestMethod]
        public void Cities_FirstRecord_Equals()
        {
            var actual = CitiesActual.First();

            //TODO: There must be a better way to store these expected values...
            Assert.AreEqual(1, actual.Header.RecordNumber);
            Assert.AreEqual(ShapeType.Point, actual.Shape.ShapeType);
            Assert.IsInstanceOfType(actual.Shape, typeof(IPointShape));

            var pointShape = actual.Shape as IPointShape;
            Assert.AreEqual(-57.840002473401341, pointShape.Point.X);
            Assert.AreEqual(-34.47999900541754, pointShape.Point.Y);
        }

        [TestMethod]
        public void MultiPoint_FirstRecord_Equals()
        {
            var actual = MultiPointActual.First();

            //TODO: There must be a better way to store these expected values...
            Assert.AreEqual(1, actual.Header.RecordNumber);
            Assert.AreEqual(ShapeType.MultiPoint, actual.Shape.ShapeType);
            Assert.IsInstanceOfType(actual.Shape, typeof(IMultiPointShape));

            //TODO: Check the box...

            var multiPointShape = actual.Shape as IMultiPointShape;
            Assert.AreEqual(458860, multiPointShape.Points.First().X);
            Assert.AreEqual(132410, multiPointShape.Points.First().Y);
        }
    }
}
