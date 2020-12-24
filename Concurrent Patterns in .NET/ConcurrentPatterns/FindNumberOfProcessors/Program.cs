using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Management;

namespace FindNumberOfProcessors
{
    /// <summary>
    /// this is not such a good idea after all
    /// </summary>
    public class Program
    {

        static void Main(string[] args)
        {

            foreach (var item in new ManagementObjectSearcher("Select * from Win32_ComputerSystem").Get())
            {
                Console.WriteLine("Number Of Physical Processors: {0} ", item["NumberOfProcessors"]);
            }

            int coreCount = 0;
            foreach (var item in new System.Management.ManagementObjectSearcher("Select * from Win32_Processor").Get())
            {
                coreCount += int.Parse(item["NumberOfCores"].ToString());
            }
            Console.WriteLine("Number Of Cores: {0}", coreCount);

            Console.WriteLine("Number Of Logical Processors: {0}", Environment.ProcessorCount);

            Console.Read();
        }
    }
}
