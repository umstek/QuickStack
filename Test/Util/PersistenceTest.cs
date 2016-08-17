using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Test.Util
{
    [TestClass]
    public class PersistenceTest
    {
        [TestMethod]
        public void TestSerializeToDisk()
        {
            const string path = @".\data\testobj.json";

            QuickStack.Util.Persistence.SerializeToJson(new { w = 0, x = 10, y = 20, z = 30 }, path);
            Assert.IsTrue(File.Exists(path)); // File written! 
            Assert.IsTrue(File.ReadAllText(path).Length > 20); // Guessing the lenght of the JSON. 
        }
    }
}
