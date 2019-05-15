using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace uapl
{
    
    public class Interpeter
    {
        public List<string> variable_names = new List<string>();
        public List<dynamic> variables = new List<dynamic>();

        public List<string> function_names = new List<string>();
        public List<bool> function_types = new List<bool>(); // is it native?
        public List<dynamic> functions = new List<dynamic>(); // where is the function located in text?
        public List<int> native_instance_indexes = new List<int>();
        public List<dynamic> native_instances = new List<dynamic>();

        public bool AddFunction(string name, string[] code, bool _override)
        {
            if (function_names.IndexOf(name) == -1)
            {
                function_names.Add(name);
                function_types.Add(false);
                functions.Add(code);
                native_instance_indexes.Add(-1);
                return true;
            }
            else
            {
                if (_override)
                {
                    int index = function_names.IndexOf(name);
                    function_types[index] = false;
                    functions[index] = code;
                    native_instance_indexes.Add(-1);
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        public bool AddNativeFunction(string name, MethodInfo code, int native_instance_index, bool _override)
        {
            if (function_names.IndexOf(name) == -1)
            {
                function_names.Add(name);
                function_types.Add(true);
                functions.Add(code);
                native_instance_indexes.Add(native_instance_index);
                return true;
            }
            else
            {
                if (_override)
                {
                    int index = function_names.IndexOf(name);
                    function_types[index] = true;
                    functions[index] = code;
                    native_instance_indexes.Add(native_instance_index);
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public String RunStatement(String statement)
        {
            String[] split = statement.Split(' ');
            if (split[0] == "run")
            {
                if (split[1] == "import")
                {
                    Assembly dll = Assembly.LoadFile(split[2]);
                    Type type = dll.GetType("Addon.Loader");
                    dynamic loader_instance = Activator.CreateInstance(type);
                    MethodInfo method = type.GetMethod("GetFunctions");
                    string[] result = method.Invoke(loader_instance, new object[] { });

                    Type function_type = dll.GetType("Addon.Functions");
                    dynamic function_instance = Activator.CreateInstance(function_type);

                    int index = native_instances.Count;
                    native_instances.Add(function_instance);

                    foreach (string function in result)
                    {
                        AddNativeFunction(function, function_type.GetMethod(function), index, false);
                    }
                }
                else
                {
                    int index = function_names.IndexOf(split[1]);
                    if (index != -1)
                    {
                        if (function_types[index])
                        {
                            return functions[index].Invoke(native_instances[native_instance_indexes[index]], new object[] { statement });
                        }
                        else
                        {
                            throw new NotImplementedException("UAPL non-built-in functions not implemented yet!");
                        }
                    }
                }
            }
            return null;
        }
    }
}