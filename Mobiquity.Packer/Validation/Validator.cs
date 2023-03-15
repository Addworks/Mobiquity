using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.mobiquity.packer
{
    public class Validator
    {
        private const int MaxNumberOfItems = 15;
        private const int MaxPackageWeight = 100;

        public Validator() 
        {
        }

        public static void ValidPackageInputs(string[] items, int maxWeight)
        { 
            /*
                1. Max weight that a package can take is ≤ 100
                2. There might be up to 15 items you need to choose from
                3. Max weight and cost of an item is ≤ 100
             
                In addition, extra check to ensure file is not empty
             */

            if(items.Length == 0)
            {
                throw new APIException($"No packages found in received file");
            }

            if (items.Length > MaxNumberOfItems)
            {
                
                throw new APIException($"Maximum number of possible packages to choose from exceeded = {MaxNumberOfItems} ");
            }

            if(maxWeight > 100)
            {
                throw new APIException($"The maximum weight any package cannot exceed {MaxPackageWeight}");
            }
        }
    }
}
