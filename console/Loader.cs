using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Addon
{
    public class Loader
    {
        public string[][] GetFunctions() => new string[][] { new string[] {"print", "print" }, new string[] {"println", "println"}, new string[] {"console.color", "color" },
        new string[]{"console.init.color", "init_color" } };
    }
    public class Functions
    {
        public void print(string cmd, string[] split, dynamic interpreter)
        {
            Console.Write(Convert.ToString(interpreter.ParseValue(split[2])));
        }
        public void println(string cmd, string[] split, dynamic interpreter)
        {
            Console.WriteLine(Convert.ToString(interpreter.ParseValue(split[2])));
        }
        public void color(string cmd, string[] split, dynamic interpreter)
        {
            if (split[2] == "fg")
            {
                
            }
        }
        public void init_color(string cmd, string[] split, dynamic interpreter)
        {
            interpreter.AddVariable("console.color.black", ConsoleColor.Black, true);
            interpreter.AddVariable("console.color.blue", ConsoleColor.Blue, true);
            interpreter.AddVariable("console.color.cyan", ConsoleColor.Cyan, true);
            interpreter.AddVariable("console.color.dark_blue", ConsoleColor.DarkBlue, true);
        }
    }
}
