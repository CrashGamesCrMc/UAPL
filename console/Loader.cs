using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Addon
{
    public class Loader
    {
        public string[][] GetFunctions() => new string[][] { new string[] {"print", "print" } };
    }
    public class Functions
    {
        public string print(string cmd, dynamic interpreter)
        {
            string[] split = cmd.Split(' ');
            interpreter.ToTempVariable(split[2]);
            Console.Write(Convert.ToString(interpreter.temp_variables[0]));
            return null;
        }
    }
}
