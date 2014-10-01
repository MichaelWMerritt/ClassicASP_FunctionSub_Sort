using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubroutineFunctionSort_VB
{
    class RunSortAlgorithm
    {
        static void Main(string[] args)
        {
            if (args.Any())
            {
                SortAlgorithm sA = new SortAlgorithm();
                sA.runSort(args);

                var list = sA.getSubsAndFunctionNameList();
                foreach (var x in list)
                {
                    Console.WriteLine(x);
                }
            }
            else 
            {
                Console.WriteLine(@"VB_Sub_Function_SearchSort.exe [Fully Qualified File Path Name] [newFilePath (Optional)] ");
            }
        }
    }
}
