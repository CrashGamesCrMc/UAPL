using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Addon
{
    public class Loader
    {
        public string[][] GetFunctions() => new string[][] { new string[] {"print", "print" }, new string[] {"println", "println"}, new string[] {"console.color.stdout.set", "color_stdout" },
        new string[]{"console.color.init", "init_color" }, new string[]{"console.color.stderr.set", "color_stderr" } };
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
        public void color_stdout(string cmd, string[] split, dynamic interpreter)
        {
            if (split[2] == "fg")
            {
                Console.ForegroundColor = interpreter.ParseValue(split[3]);
            }
            else if (split[2] == "bg")
            {
                Console.BackgroundColor = interpreter.ParseValue(split[3]);
            }
            else
            {
                throw new ArgumentException("console.color only accepts fg (foreground) and bg (background).");
            }
        }

        public void color_stderr(string cmd, string[] split, dynamic interpreter)
        {
            interpreter.stderr_color = interpreter.ParseValue(split[2]);
        }

        public void init_color(string cmd, string[] split, dynamic interpreter)
        {
            interpreter.AddVariable("console.color.black", ConsoleColor.Black, true);
            interpreter.AddVariable("console.color.blue", ConsoleColor.Blue, true);
            interpreter.AddVariable("console.color.cyan", ConsoleColor.Cyan, true);
            interpreter.AddVariable("console.color.dark_blue", ConsoleColor.DarkBlue, true);
            interpreter.AddVariable("console.color.dark_cyan", ConsoleColor.DarkCyan, true);
            interpreter.AddVariable("console.color.dark_gray", ConsoleColor.DarkGray, true);
            interpreter.AddVariable("console.color.dark_green", ConsoleColor.DarkGreen, true);
            interpreter.AddVariable("console.color.dark_magenta", ConsoleColor.DarkMagenta, true);
            interpreter.AddVariable("console.color.dark_red", ConsoleColor.DarkRed, true);
            interpreter.AddVariable("console.color.dark_yellow", ConsoleColor.DarkYellow, true);
            interpreter.AddVariable("console.color.gray", ConsoleColor.Gray, true);
            interpreter.AddVariable("console.color.green", ConsoleColor.Green, true);
            interpreter.AddVariable("console.color.magenta", ConsoleColor.Magenta, true);
            interpreter.AddVariable("console.color.red", ConsoleColor.Red, true);
            interpreter.AddVariable("console.color.white", ConsoleColor.White, true);
            interpreter.AddVariable("console.color.yellow", ConsoleColor.Yellow, true);
        }
    }
}
