using com.mobiquity.packer;
using System.Text;

namespace com.mobiquity.packer.services
{
    public class PackageService: IPackageService
    {
        /// <summary>
        /// Gets the most optimum package based on cost and weight of individual possible items and max weight possible for each package
        /// </summary>
        /// <param name="packageElements"></param>
        /// <param name="maxWeight"></param>
        /// <returns></returns>
        public string GetOptimumPackages(List<PackageItem> packageElements, int maxWeight)
        {
            //initialise local variables
            var output = new StringBuilder();
            var outputWeights = 0.00;

            /*
             * Notes:
             * ----------
             *  -   This particular implementation employs a greedy approach to solve this Knapsack Problem
             *  -   loop through all elements - order by cost (descending) and then by weight (ascending) to ensure a more efficient iteration is conducted
             *  -   does a single iteration through all the items and adds until the max weight is reached
             *  -   Limitations
             *  ------------------
             *      -   Because a greedy, single iteration approach is used, the solution may not be the most optimal
             *      -   We can enhance the solution to determine all possible combinations and then choose the most optimal from there
             */

            foreach (var element in packageElements
                                        .OrderByDescending(f => f.Cost)
                                        .ThenBy(f => f.Weight)
                                        .Where(f => f.Weight <= maxWeight))
            {
                if (outputWeights + element.Weight <= maxWeight)
                {
                    outputWeights += element.Weight;
                    output.Append($"{element.ElementIndex},");
                }
            }

            return CleanOutput(output.ToString());
        }

        /// <summary>
        /// An enhancement of the above solution - gets more possible combinations and the evaluates all
        /// </summary>
        /// <param name="packageElements"></param>
        /// <param name="maxWeight"></param>
        /// <returns></returns>
        public string GetOptimumPackages_Enhanced(List<PackageItem> packageElements, int maxWeight)
        {
            /*
           * Notes:
           * ----------
           *  -   This particular implementation employs a more thorough but less optimum solution to solve this Knapsack Problem
           *  -   gets possible combination by removing the highest cost item at each outer iteration.
           *  -   then gets all items that make up the package until the max weight is reached
           *  -   at the end - compares all possible combinations to choose the best option
           *  -   Limitations
           *  ------------------
           *      -   A lot less perfomant than the alternative approach
           */

            Dictionary<string, Tuple<double, double>> possibleCombinations = new Dictionary<string, Tuple<double, double>>();

            var i = 0;

            while (i < packageElements.Count())
            {
                var output = new StringBuilder();
                var outputWeights = 0.00;
                var outputCosts = 0.00;

                foreach (var element in packageElements
                                            .OrderByDescending(f => f.Cost)
                                            .ThenBy(f => f.Weight)
                                            .Where(f => f.Weight <= maxWeight)
                                            .Skip(i)
                )
                {
                    if (outputWeights + element.Weight <= maxWeight)
                    {
                        outputWeights += element.Weight;
                        outputCosts += element.Cost;
                        output.Append($"{element.ElementIndex},");
                    }
                }
                var key = CleanOutput(output.ToString());
                if (!possibleCombinations.ContainsKey(key))
                {
                    possibleCombinations.Add(key, new Tuple<double, double>(outputWeights, outputCosts));
                }

                i++;
            }

            return possibleCombinations
                        .OrderByDescending(f => f.Value.Item2)
                        .ThenBy(f => f.Value.Item1)
                        .FirstOrDefault().Key.ToString();
        }

        /// <summary>
        /// Formats the returned packages if results are empty. Also removes trailing commas
        /// </summary>
        /// <param name="output"></param>
        /// <returns></returns>
        public string CleanOutput(string output)
        {
            if (string.IsNullOrEmpty(output))
            {
                output = "-";
            }
            else if(output.EndsWith(","))
            {
                output = output[..^1];
            }

            return output;
        }
    }
}