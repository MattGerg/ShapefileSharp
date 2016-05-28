using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Linq;

namespace ShapefileSharp.Tests
{
    [TestClass]
    public class ReadWriteTests
    {
        private bool IsContentEqual(string filePath1, string filePath2)
        {
            var bytes1 = File.ReadAllBytes(filePath1);
            var bytes2 = File.ReadAllBytes(filePath2);

            return bytes1.SequenceEqual(bytes2);
        }

        [TestMethod]
        public void ReadWrite_Point()
        {

        }
    }
}
