using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ShapefileSharp.Tests
{
    [TestClass]
    public class BoundingBoxTests
    {
        BoundingBox Box1 = new BoundingBox()
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

        [TestMethod]
        public void Equals_True()
        {
            BoundingBox box2 = Box1.ToMutable();

            Assert.AreNotSame(Box1, box2);
            Assert.AreEqual(Box1, box2);
        }

        [TestMethod]
        public void Equals_False()
        {
            BoundingBox box2 = Box1.ToMutable();
            box2.MMax = 0;

            Assert.AreNotEqual(Box1, box2);
        }
    }
}
