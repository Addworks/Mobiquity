using com.mobiquity.packer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.mobiquity.packer.services
{
    public interface IPackageService
    {
        string GetOptimumPackages(List<PackageItem> packageElements, int maxWeight);
        string CleanOutput(string output);
    }
}
