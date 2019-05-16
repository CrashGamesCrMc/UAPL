using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Addon
{
    public class Loader
    {
        public string[][] GetFunctions() => new string[][] { new string[] { "and", "and" } };
    }
    public class Functions
    {
        public string and(string cmd, dynamic interpreter)
        {
            string[] split = cmd.Split(' ');
            if (split[2] != "bool:true" && split[3] != "bool:true")
            {
                return "bool:true";
            }
            return "bool:false";
        }
    }
}
