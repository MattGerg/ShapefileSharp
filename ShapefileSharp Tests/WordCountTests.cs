using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ShapefileSharp.Tests
{
    [TestClass]
    public class WordCountTests
    {
        WordCount wordCount1A = new WordCount()
        {
            Words = 1
        };
        WordCount wordCount1B = new WordCount()
        {
            Words = 1
        };

        WordCount wordCount2 = new WordCount()
        {
            Words = 2
        };

        [TestMethod]
        public void Equals_True()
        {
            Assert.AreEqual(wordCount1A, wordCount1B);
        }

        [TestMethod]
        public void Equals_False()
        {
            Assert.AreNotEqual(wordCount1A, wordCount2);
        }
    }
}
