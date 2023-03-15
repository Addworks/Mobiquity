using com.mobiquity.packer.services;
using com.mobiquity.packer;

namespace com.mobiquity.packer
{
    public class Packer
    {
        private const int MaxItemWeightCost = 100;

        /// <summary>
        /// Method takes filepath as input, reads file, parses to determine individual package components (max weight and possible items), determines the most optimum package based on cost and weight
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static string Pack(string filePath)
        {
            try
            {
                //check if file exists
                if(!File.Exists(filePath))
                {
                    throw new APIException("File not found");
                }

                // Read the f3ile - get all contents
                var allPackages = File.ReadAllLines(filePath);

                if(!allPackages.Any())
                {
                    throw new APIException("Empty file provided");
                }

                //todo - factory pattern to new the packager?
                var packager = new PackageService();

                var packageOutputs = new List<string>();
                foreach (var package in allPackages)
                {
                    var packageInputs = package.Split(" : ");

                    var items = packageInputs[1].Trim().Split(' ');
                    var maxWeight = Convert.ToInt32(packageInputs[0]);

                    //Validate all inputs
                    Validator.ValidPackageInputs(items, maxWeight);

                    //note - data structure used is a record data type
                    var packageElements = new List<PackageItem>();
                    foreach (var item in items)
                    {
                        packageElements.Add(item);
                    }

                    //check if cost or weight for any of the items exceeds 100
                    if (packageElements.Any(f => f.Cost > MaxItemWeightCost || f.Weight > MaxItemWeightCost))
                    {
                        throw new APIException($"The maximum weight and cost for any item cannot exceed {MaxItemWeightCost}");
                    }

                    /*
                        Determine highest cost/lowest weight
                            - Two options available GetOptimumPackages and GetOptimumPackages_Enhanced
                    */
                    packageOutputs.Add(
                            //packager.GetOptimumPackages(packageElements, maxWeight)
                            packager.GetOptimumPackages_Enhanced(packageElements, maxWeight)
                        );
                }

                return string.Join("/n", packageOutputs);
            }
            catch (Exception ex)
            {
                //todo - log the error

                throw;
            }
        }
    }
}