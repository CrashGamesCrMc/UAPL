using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace uapl
{
    
    public class Interpreter
    {
        public List<string> variable_names = new List<string>();
        public List<dynamic> variables = new List<dynamic>();

        public List<string> function_names = new List<string>();
        public List<bool> function_types = new List<bool>(); // is it native?
        public List<dynamic> functions = new List<dynamic>(); // where is the function located in text?
        public List<int> native_instance_indexes = new List<int>();
        public List<dynamic> native_instances = new List<dynamic>();

        public List<dynamic> temp_variables = new List<dynamic>();

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

        public void ToTempVariable(string value)
        {
            if (value.StartsWith("$"))
            {
                temp_variables.Add(variables[variable_names.IndexOf(value.Substring(1))]);
            }
            else
            {
                string[] split = value.Split(':');
                if (split[0] == "bool")
                {
                    temp_variables.Add(Boolean.Parse(split[1]));
                }
                else if (split[0] == "string")
                {
                    temp_variables.Add(split[1]);
                }
                else if (split[0] == "int")
                {
                    temp_variables.Add(Convert.ToInt32(split[1]));
                }
                else
                {
                    throw new Exceptions.VariableExceptions.VariableException("variable type \"" + split[0] + "\" does not exist!");
                }
            }
        }

        public string RunStatement(string statement)
        {
            temp_variables.Clear();

            String[] split = statement.Split(' ');
            if (split[0] == "run")
            {
                if (split[1] == "importn")
                {
                    Assembly dll = Assembly.LoadFile(System.IO.Directory.GetCurrentDirectory() + @"\" + split[2]);
                    Type type = dll.GetType("Addon.Loader");
                    dynamic loader_instance = Activator.CreateInstance(type);
                    MethodInfo method = type.GetMethod("GetFunctions");
                    string[][] result = method.Invoke(loader_instance, new object[] { });

                    Type function_type = dll.GetType("Addon.Functions");
                    dynamic function_instance = Activator.CreateInstance(function_type);

                    int index = native_instances.Count;
                    native_instances.Add(function_instance);

                    foreach (string[] function in result)
                    {
                        AddNativeFunction(function[0], function_type.GetMethod(function[1]), index, false);
                    }
                }
                else
                {
                    int index = function_names.IndexOf(split[1]);
                    if (index != -1)
                    {
                        if (function_types[index])
                        {
                            return functions[index].Invoke(native_instances[native_instance_indexes[index]], new object[] { statement, this });
                        }
                        else
                        {
                            throw new NotImplementedException("UAPL non-built-in functions not implemented yet!");
                        }
                    }
                }
            }
            else if (split[0] == "if")
            {
                ToTempVariable(split[1]);
                if (temp_variables[0].GetType() == true.GetType())
                {
                    if (temp_variables[0])
                    {
                        RunStatements(statement.Split('/')[1].Split('|'));
                    }
                }
                else
                {
                    throw new Exceptions.VariableExceptions.VariableException("if only excepts type bool, not "+Convert.ToString(temp_variables[0].GetType()));
                }
            }
            else
            {
                throw new Exceptions.SyntaxException("could not parse \""+statement+"\"");
            }
            return null;
        }
        
        public void RunStatements(string[] statements)
        {
            foreach (string item in statements)
            {
                RunStatement(item);
            }
        }

        public void InterpretConsole()
        {
            while (true)
            {
                Console.WriteLine(Convert.ToString(RunStatement(Console.ReadLine())));
            }
        }
    }
}