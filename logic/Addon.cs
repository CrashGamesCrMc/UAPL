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
        public string and(string cmd)
        {
            string[] split = cmd.Split(' ');
            if (split[2] != "\"true\"" && split[3] != "\"true\"")
            {
                return "\"true\"";
            }
            return "\"false\"";
        }
    }
}
