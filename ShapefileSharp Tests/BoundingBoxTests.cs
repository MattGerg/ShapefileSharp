using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ShapefileSharp.Tests
{
    [TestClass]
    public class BoundingBoxTests
    {
        [TestMethod]
        public void Equals_True()
        {
            BoundingBox box1 = new BoundingBox()
            {
                XMin = -10,
                XMax = 10,
                YMin = -20,
                YMax = 20,
                ZMin = -30,
                ZMax = 30,
                MMin = -40,
                MMax = 40
            };

            BoundingBox box2 = box1.ToMutable();

            Assert.AreNotSame(box1, box2);
            Assert.AreEqual(box1, box2);
        }
    }
}
