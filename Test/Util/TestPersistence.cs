using System.IO;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using QuickStack.Util;
using Test.Support;

namespace Test.Util
{
    /// <summary>
    ///     The test persistence.
    /// </summary>
    [TestClass]
    public class TestPersistence
    {
        /// <summary>
        ///     The test serialize to disk.
        /// </summary>
        [TestMethod]
        public void TestSerializeToDisk()
        {
            const string path = @".\data\testobj.json";

            Persistence.SerializeToJson(new {w = 0, x = 10, y = 20, z = 30}, path);

            Assert.IsTrue(File.Exists(path)); // File written! 
            Assert.IsTrue(File.ReadAllText(path).Length > 20); // Guessing the length of the JSON. 
        }

        /// <summary>
        ///     The test deserialize from disk.
        /// </summary>
        [TestMethod]
        public void TestDeserializeFromDisk()
        {
            var targetDog = new Dog("tommy") {Friends = {new Dog("kadiya"), new Dog("snowy")}};
            const string path = @".\data\testdog.json";

            Persistence.SerializeToJson(targetDog, path); // Save
            var deserializedDog = Persistence.DeserializeFromJson<Dog>(path); // Load

            Assert.AreEqual(targetDog, deserializedDog);
        }

        /// <summary>
        ///     The test deserialize anonymous from disk.
        /// </summary>
        [TestMethod]
        public void TestDeserializeAnonymousFromDisk()
        {
            const string path = @".\data\testobj.json";
            const string example = @"{""w"":0,""x"":10,""y"":20,""z"":30}";
            var sample = new {w = 0, x = 10, y = 20, z = 30};

            // ReSharper disable once AssignNullToNotNullAttribute
            Directory.CreateDirectory(Path.GetDirectoryName(path)); // Create the directory if not exists.
            File.WriteAllText(path, example); // Write the JSON data.

            Assert.AreEqual(sample, Persistence.DeserializeFromJson(path, sample)); // Guessing the length of the JSON. 
        }

        /// <summary>
        ///     The cleanup.
        /// </summary>
        [TestCleanup]
        public void Cleanup()
        {
            const string path = @".\data\";
            if (!Directory.Exists(path)) return;

            Directory.GetFiles(path).ToList().ForEach(File.Delete);
            Directory.Delete(path);
        }
    }
}