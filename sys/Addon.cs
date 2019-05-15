using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Addon
{
    public class Loader
    {
        public string[] GetFunctions() => new string[]{"test"};
    }
    public class Functions
    {
        public string test(string cmd)
        {
            Console.WriteLine("sys lib running test-function; called by line: \"" + cmd + "\"");
            return "success!";
        }
    }
}
