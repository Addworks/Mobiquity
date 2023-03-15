using com.mobiquity.packer;
using com.mobiquity.packer.services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mobiquity.Test
{
    [TestClass]
    public class PackerTests
    {
        /* 
         *   ETE tests with a couple of files
         */


        public PackerTests()
        {
        }

        private string dir;

        [TestInitialize()]
        public void Startup()
        {
        }

        [TestCleanup()]
        public void Cleanup()
        {
        }


        [TestMethod]
        public void FileNotFound()
        {
            var filePath = Directory.GetCurrentDirectory() + @"\Files\NoFileProvided.txt";
            var ex = Assert.ThrowsException<APIException>(() => Packer.Pack(filePath));
            Assert.AreEqual(ex.Message, "Packer Exception Encountered: File not found");
        }

        [TestMethod]
        public void EmptyFileProvided()
        {
            var filePath = Directory.GetCurrentDirectory() + @"\Files\EmptyFile.txt";
            var ex = Assert.ThrowsException<APIException>(() => Packer.Pack(filePath));
            Assert.AreEqual(ex.Message, "Packer Exception Encountered: Empty file provided");
        }

        [TestMethod]
        public void MoreThan15Items()
        {
            var filePath = Directory.GetCurrentDirectory() + @"\Files\MoreThan15Items.txt";
            var ex = Assert.ThrowsException<APIException>(() => Packer.Pack(filePath));
            Assert.AreEqual(ex.Message, "Packer Exception Encountered: Maximum number of possible packages to choose from exceeded = 15 ");
        }

        [TestMethod]
        public void WeightOrCostGreaterThan100()
        {
            var filePath = Directory.GetCurrentDirectory() + @"\Files\WeightOrCostGreaterThan100.txt";
            var ex = Assert.ThrowsException<APIException>(() => Packer.Pack(filePath));
            Assert.AreEqual(ex.Message, "Packer Exception Encountered: The maximum weight and cost for any item cannot exceed 100");
        }

        [TestMethod]
        public void MaxWeightMoreThan100()
        {
            var filePath = Directory.GetCurrentDirectory() + @"\Files\MaxWeightMoreThan100.txt";
            var ex = Assert.ThrowsException<APIException>(() => Packer.Pack(filePath));
            Assert.AreEqual(ex.Message, "Packer Exception Encountered: The maximum weight any package cannot exceed 100");
        }

        [TestMethod]
        public void EndToEnd_File1()
        {
            var filePath = Directory.GetCurrentDirectory() + @"\Files\example_input";
            var output = Packer.Pack(filePath);
            Assert.AreEqual(output, "4/n-/n2,7/n8,9");
        }

        [TestMethod]
        public void EndToEnd_File2()
        {
            var filePath = Directory.GetCurrentDirectory() + @"\Files\ete_input1";
            var output = Packer.Pack(filePath);
            Assert.AreEqual(output, "4/n-/n2,7,3/n8,9,2");
        }
    }
}
