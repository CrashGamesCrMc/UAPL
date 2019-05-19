using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Addon
{
    public class Loader
    {
        public string[][] GetFunctions() => new string[][] { new string[] { "cleaner.whitelist.functions.add", "whitelist_functions_add" },
            new string[] { "cleaner.whitelist.variables.add", "whitelist_variables_add" }, new string[]{ "cleaner.blacklist.functions.add", "blacklist_functions_add" },
            new string[]{ "cleaner.backlist.variables.add", "blacklist_variables_add" },
            new string[]{ "cleaner.whitelist.functions.clean", "whitelist_functions_clean" }, new string[]{ "cleaner.whitelist.variables.clean", "whitelist_variables_add" },
            new string[]{ "cleaner.blacklist.functions.clean", "blacklist_functions_clean" }, new string[]{ "cleaner.blacklist.variables.clean", "blacklist_variables_clean" } };
    }
    public class Functions
    {
        public List<string> whitelist_functions = new List<string>();
        public List<string> whitelist_variables = new List<string>();
        public List<string> blacklist_functions = new List<string>();
        public List<string> blacklist_variables = new List<string>();

        public void whitelist_functions_add(string cmd, string[] split, dynamic interpreter)
        {
            whitelist_functions.Add(split[2]);
        }
        public void whitelist_variables_add(string cmd, string[] split, dynamic interpreter)
        {
            whitelist_variables.Add(split[2]);
        }
        public void blacklist_functions_add(string cmd, string[] split, dynamic interpreter)
        {
            blacklist_functions.Add(split[2]);
        }
        public void blacklist_variables_add(string cmd, string[] split, dynamic interpreter)
        {
            blacklist_variables.Add(split[2]);
        }

        public void whitelist_functions_clean(string cmd, string[] split, dynamic interpreter)
        {
            int i = 0;
            while (i < interpreter.functions.Count)
            {
                string name = interpreter.function_names[i];
                if (whitelist_functions.IndexOf(name) == -1)
                {
                    interpreter.RemoveFunction(name);
                }
                else
                {
                    i++;
                }
            }
        }

        public void whitelist_variables_clean(string cmd, string[] split, dynamic interpreter)
        {
            int i = 0;
            while (i < interpreter.variables.Count)
            {
                string name = interpreter.variable_names[i];
                if (whitelist_variables.IndexOf(name) == -1)
                {
                    interpreter.RemoveVariable(name);
                }
                else
                {
                    i++;
                }
            }
        }

        public void blacklist_functions_clean(string cmd, string[] split, dynamic interpreter)
        {
            foreach (string name in interpreter.function_names)
            {
                if (blacklist_functions.IndexOf(name) != -1)
                {
                    interpreter.RemoveFunction(name);
                }
            }
        }
        public void blacklist_variables_clean(string cmd, string[] split, dynamic interpreter)
        {
            foreach (string name in interpreter.variable_names)
            {
                if (blacklist_variables.IndexOf(name) != -1)
                {
                    interpreter.RemoveVariable(name);
                }
            }
        }
    }
}
