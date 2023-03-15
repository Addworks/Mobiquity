using com.mobiquity.packer;
using com.mobiquity.packer.services;

namespace Mobiquity.Test
{
    [TestClass]
    public class PackageServiceTests
    {
        IPackageService packageService; 

        public PackageServiceTests()
        {
        }

        private string dir;

        [TestInitialize()]
        public void Startup()
        {
            packageService = new PackageService();
        }

        [TestCleanup()]
        public void Cleanup()
        {        
        }


        [TestMethod]
        public void CleanOutput_TrailingCommas()
        {
            var withTrailingComma = "4,5,";
            var output = packageService.CleanOutput(withTrailingComma);

            Assert.AreEqual(output, "4,5");
        }


        [TestMethod]
        public void CleanOutput_NoPackagesMatched()
        {
            var withTrailingComma = "";
            var output = packageService.CleanOutput(withTrailingComma);

            Assert.AreEqual(output, "-");
        }


        [TestMethod]
        public void GetOptimumPackages_NoPackagesMatched()
        {
            var packageElements = new List<PackageItem> { new PackageItem { ElementIndex = 1, Weight = 95, Cost = 99 } };
            var maxWeight = 90;

            var output = packageService.GetOptimumPackages(packageElements, maxWeight);

            Assert.AreEqual(output, "-");
        }


        #region Provided Scenarios

        [TestMethod]
        public void GetOptimumPackages_SinglePackageMatched()
        {
            var packageElements = new List<PackageItem> 
            { 
                new PackageItem { ElementIndex = 1, Weight = 53.38, Cost = 45 },
                new PackageItem { ElementIndex = 2, Weight = 88.62, Cost = 98 },
                new PackageItem { ElementIndex = 3, Weight = 78.48, Cost = 3 },
                new PackageItem { ElementIndex = 4, Weight = 72.30, Cost = 76 },
                new PackageItem { ElementIndex = 5, Weight = 30.18, Cost = 9 }
            };
            var maxWeight = 81;

            var output = packageService.GetOptimumPackages(packageElements, maxWeight);

            Assert.AreEqual(output, "4");
        }


        [TestMethod]
        public void GetOptimumPackages_MultiplePackagesMatched_1()
        {
            var packageElements = new List<PackageItem>
            {
                new PackageItem { ElementIndex = 1, Weight = 85.31, Cost = 29 },
                new PackageItem { ElementIndex = 2, Weight = 14.55, Cost = 74 },
                new PackageItem { ElementIndex = 3, Weight = 3.98, Cost = 16 },
                new PackageItem { ElementIndex = 4, Weight = 26.24, Cost = 55 },
                new PackageItem { ElementIndex = 5, Weight = 63.69, Cost = 52 },
                new PackageItem { ElementIndex = 6, Weight = 76.25, Cost = 75 },
                new PackageItem { ElementIndex = 7, Weight = 60.02, Cost = 74 },
                new PackageItem { ElementIndex = 8, Weight =93.18, Cost =  35},
                new PackageItem { ElementIndex = 9, Weight = 89.95, Cost = 78 }
            };
            var maxWeight = 75;

            var output = packageService.GetOptimumPackages(packageElements, maxWeight);

            Assert.AreEqual(output, "2,7");
        }


        [TestMethod]
        public void GetOptimumPackages_MultiplePackagesMatched_2()
        {
            var packageElements = new List<PackageItem>
            {
                new PackageItem { ElementIndex = 1, Weight = 90.72, Cost = 13 },
                new PackageItem { ElementIndex = 2, Weight = 33.80, Cost = 40 },
                new PackageItem { ElementIndex = 3, Weight = 43.15, Cost = 10 },
                new PackageItem { ElementIndex = 4, Weight = 37.97, Cost = 16 },
                new PackageItem { ElementIndex = 5, Weight = 46.81, Cost = 36 },
                new PackageItem { ElementIndex = 6, Weight = 44.77, Cost = 79 },
                new PackageItem { ElementIndex = 7, Weight = 81.80, Cost = 45 },
                new PackageItem { ElementIndex = 8, Weight = 19.36, Cost = 79 },
                new PackageItem { ElementIndex = 9, Weight = 6.76, Cost = 64 }
            };
            var maxWeight = 56;

            var output = packageService.GetOptimumPackages(packageElements, maxWeight);

            Assert.AreEqual(output, "8,9");
        }

        #endregion

        #region Other Scenarios

        [TestMethod]
        public void GetOptimumPackages_OtherScenarios_SinglePackageMatched()
        {
            var packageElements = new List<PackageItem>
            {
                new PackageItem { ElementIndex = 1, Weight = 53.38, Cost = 45 },
                new PackageItem { ElementIndex = 2, Weight = 88.62, Cost = 98 },
                new PackageItem { ElementIndex = 3, Weight = 78.48, Cost = 3 },
                new PackageItem { ElementIndex = 4, Weight = 72.30, Cost = 76 },
                new PackageItem { ElementIndex = 5, Weight = 30.18, Cost = 9 }
            };
            var maxWeight = 81;

            var output = packageService.GetOptimumPackages(packageElements, maxWeight);

            Assert.AreEqual(output, "4");
        }


        [TestMethod]
        public void GetOptimumPackages_OtherScenarios_MultiplePackagesMatched_1()
        {
            var packageElements = new List<PackageItem>
            {
                new PackageItem { ElementIndex = 1, Weight = 85.31, Cost = 29 },
                new PackageItem { ElementIndex = 2, Weight = 14.55, Cost = 74 },
                new PackageItem { ElementIndex = 3, Weight = 3.98, Cost = 16 },
                new PackageItem { ElementIndex = 4, Weight = 26.24, Cost = 55 },
                new PackageItem { ElementIndex = 5, Weight = 63.69, Cost = 52 },
                new PackageItem { ElementIndex = 6, Weight = 76.25, Cost = 75 },
                new PackageItem { ElementIndex = 7, Weight = 60.02, Cost = 74 },
                new PackageItem { ElementIndex = 8, Weight =93.18, Cost =  35},
                new PackageItem { ElementIndex = 9, Weight = 89.95, Cost = 78 }
            };
            var maxWeight = 75;

            var output = packageService.GetOptimumPackages(packageElements, maxWeight);

            Assert.AreEqual(output, "2,7");
        }


        [TestMethod]
        public void GetOptimumPackages_OtherScenarios_MultiplePackagesMatched_2()
        {
            var packageElements = new List<PackageItem>
            {
                new PackageItem { ElementIndex = 1, Weight = 90.72, Cost = 13 },
                new PackageItem { ElementIndex = 2, Weight = 33.80, Cost = 40 },
                new PackageItem { ElementIndex = 3, Weight = 43.15, Cost = 10 },
                new PackageItem { ElementIndex = 4, Weight = 37.97, Cost = 16 },
                new PackageItem { ElementIndex = 5, Weight = 46.81, Cost = 36 },
                new PackageItem { ElementIndex = 6, Weight = 44.77, Cost = 79 },
                new PackageItem { ElementIndex = 7, Weight = 81.80, Cost = 45 },
                new PackageItem { ElementIndex = 8, Weight = 19.36, Cost = 79 },
                new PackageItem { ElementIndex = 9, Weight = 6.76, Cost = 64 }
            };
            var maxWeight = 56;

            var output = packageService.GetOptimumPackages(packageElements, maxWeight);

            Assert.AreEqual(output, "8,9");
        }

        #endregion
    }
}