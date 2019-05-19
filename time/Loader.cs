using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Addon
{
    public class Loader
    {
        public string[][] GetFunctions() => new string[][] { new string[] { "time.time","time"}, new string[] { "time.getSeconds", "getSeconds"}, new string[] { "time.getMinutes", "getMinutes"},
            new string[]{ "time.getHours", "getHours" } };
    }
    public class Functions
    {
        public DateTime time(string cmd, string[] split, dynamic interpreter)
        {
            return DateTime.Now;
        }
        public int getSeconds(string cmd, string[] split, dynamic interpreter)
        {
            return interpreter.ParseValue(split[2]).Second;
        }
        public int getMinutes(string cmd, string[] split, dynamic interpreter)
        {
            return interpreter.ParseValue(split[2]).Minute;
        }
        public int getHours(string cmd, string[] split, dynamic interpreter)
        {
            return interpreter.ParseValue(split[2]).Hour;
        }
    }
}
