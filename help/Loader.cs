using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Addon
{
    public class Loader
    {
        public string[][] GetFunctions() => new string[][] { new string[] { "help", "help" } };
    }
    public class Functions
    {
        public string help(string cmd, string[] split, dynamic interpreter)
        {
            return "run <function> [<args>]; - runs a function" + Environment.NewLine +
                "if <bool> /<code>|<code>; - condition" + Environment.NewLine +
                "run importn <file>; - imports a native file (type depends on unterlying language)" + Environment.NewLine +
                "" + Environment.NewLine +
                "lib/core.dll:" + Environment.NewLine +
                "   functions:" + Environment.NewLine +
                "       and <bool> <bool> - returns true when both inputs are true" + Environment.NewLine +
                "       equals <obj> <obj> - compares two objects" + Environment.NewLine +
                "       print <obj> - prints the object to the console";
        }
    }
}
