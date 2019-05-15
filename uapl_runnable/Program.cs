using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace uapl_runnable
{
    class Program
    {
        static void Main(string[] args)
        {
            uapl.Interpreter interpreter = new uapl.Interpreter();
            interpreter.InterpretConsole();
        }
    }
}