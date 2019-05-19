using System;
using System.Collections.Generic;
using System.Reflection;
using System.IO;

namespace uapl
{

    public class Interpreter
    {
        public ConsoleColor stderr_color = ConsoleColor.DarkRed;

        public List<string> variable_names = new List<string>();
        public List<dynamic> variables = new List<dynamic>();

        public List<string> function_names = new List<string>();
        public List<bool> function_types = new List<bool>(); // is it native?
        public List<dynamic> functions = new List<dynamic>(); // where is the function located in text?
        public List<int> native_instance_indexes = new List<int>();
        public List<dynamic> native_instances = new List<dynamic>();

        public string PatchTogether(string[] array, int offset)
        {
            string result = "";
            for (int i = offset; i < array.Length-1; i++)
            {
                result += array[i] + " ";
            }
            if (array.Length > 0)
            {
                result += array[array.Length - 1];
            }
            return result;
        }

        public string PatchTogether(string[] array, int offset, int length)
        {
            string result = "";
            for (int i = 0; i < length; i++)
            {
                result += array[i] + " ";
            }
            if (length > 0)
            {
                result += array[offset + length - 1];
            }
            return result;
        }

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

        public bool RemoveFunction(string name)
        {
            int index = function_names.IndexOf(name);
            if (index == -1)
            {
                return false;
            }
            else
            {
                function_names.RemoveAt(index);
                functions.RemoveAt(index);
                function_types.RemoveAt(index);
                native_instance_indexes.RemoveAt(index);
                return true;
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

        public bool AddVariable(string name, dynamic value, bool _override)
        {
            if (variable_names.IndexOf(name) == -1)
            {
                variable_names.Add(name);
                variables.Add(value);
                return true;
            }
            else if (_override)
            {
                variables[variable_names.IndexOf(name)] = value;
                return true;
            }
            return false;
        }

        public bool RemoveVariable(string name)
        {
            int index = variable_names.IndexOf(name);
            if (index == -1)
            {
                return false;
            }
            else
            {
                variable_names.RemoveAt(index);
                variables.RemoveAt(index);
                return true;
            }
        }
        
        public dynamic ParseValue(string value)
        {
            if (value.StartsWith("$"))
            {
                return variables[variable_names.IndexOf(value.Substring(1))];
            }
            else
            {
                string[] split = value.Split(':');
                if (split[0] == "bool")
                {
                    return Boolean.Parse(split[1]);
                }
                else if (split[0] == "string")
                {
                    return split[1];
                }
                else if (split[0] == "int")
                {
                    return Convert.ToInt32(split[1]);
                }
                else
                {
                    throw new Exceptions.VariableExceptions.VariableException("variable type \"" + split[0] + "\" does not exist!");
                }
            }
        }

        public object RunStatement(string statement)
        {
            String[] split = statement.Split(' ');
            if (split[0] == "run")
            {
                int index = function_names.IndexOf(split[1]);
                if (index != -1)
                {
                    if (function_types[index])
                    {
                        MethodInfo function = functions[index];
                        try
                        {
                            return Convert.ChangeType(function.Invoke(native_instances[native_instance_indexes[index]], new object[] { statement, split, this }), function.ReturnType);
                        }
                        catch (InvalidCastException)
                        {
                            return null;
                        }
                        catch (NullReferenceException)
                        {
                            throw new NotImplementedException("The function \"" + split[1] + "\" is deklared in the extensions Addon.Loader.GetFunctions()" +
                                " method, but is not implemented in Addon.Functions ");
                        }
                    }
                    else
                    {
                        throw new NotImplementedException("UAPL non-built-in functions not implemented yet!");
                    }
                }
                else
                {
                    throw new Exceptions.FunctionExcepetions.FunctionDoesNotExistException("Function \"" + split[1] + "\" does not exist!");
                }
            }
            else if (split[0] == "runs")
            {
                AddVariable(split[1], RunStatement("run " + PatchTogether(split, 2)), true);
            }
            else if (split[0] == "if")
            {
                dynamic condition = ParseValue(split[1]);
                if (condition.GetType() == true.GetType())
                {
                    if (condition)
                    {
                        RunStatements(statement.Split('/')[1].Split('|'));
                    }
                }
                else
                {
                    throw new Exceptions.VariableExceptions.VariableException("if only excepts type bool, not "+Convert.ToString(condition.GetType()));
                }
            }
            else if (split[0] == "var")
            {
                dynamic value = ParseValue(split[2]);
                AddVariable(split[1], value, true);
            }
            else if (split[0] == "import")
            {
                RunFile(split[1]);
            }
            else if (split[0] == "importn")
            {
                Assembly dll = Assembly.LoadFile(System.IO.Directory.GetCurrentDirectory() + @"\" + split[1]);
                Type type = dll.GetType("Addon.Loader");
                try
                {
                    dynamic loader_instance = Activator.CreateInstance(type);
                    try
                    {
                        MethodInfo method = type.GetMethod("GetFunctions");
                        string[][] result = method.Invoke(loader_instance, new object[] { });

                        try
                        {
                            MethodInfo init_function = type.GetMethod("Init");
                            init_function.Invoke(loader_instance, new object[] { this });
                        }
                        catch (NullReferenceException)
                        {
                        }

                        Type function_type = dll.GetType("Addon.Functions");
                        dynamic function_instance = Activator.CreateInstance(function_type);

                        int index = native_instances.Count;
                        native_instances.Add(function_instance);

                        foreach (string[] function in result)
                        {
                            AddNativeFunction(function[0], function_type.GetMethod(function[1]), index, false);
                        }
                    }
                    catch (NullReferenceException)
                    {
                        throw new Exceptions.ExtensionExceptions.ExtensionException("method Addon.Loader.GetFunctions() with return type string[][] missing! Is the DLL really an extension?");
                    }
                }
                catch (ArgumentNullException)
                {
                    throw new Exceptions.ExtensionExceptions.ExtensionException("class Addon.Loader does not exist! Is the DLL really an extension?");
                }
                
            }
            else
            {
                throw new Exceptions.SyntaxException("could not parse \"" + statement + "\"");
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
                try
                {
                    object result = RunStatement(Console.ReadLine());
                    Console.WriteLine(Convert.ToString(result));
                    try
                    {
                        Console.WriteLine("Type: " + result.GetType());
                    }
                    catch (NullReferenceException)
                    {
                        Console.WriteLine("Type: none");
                    }
                }
                catch (Exception e)
                {
                    ConsoleColor old = Console.ForegroundColor;
                    Console.ForegroundColor = stderr_color;
                    Console.WriteLine(e);
                    Console.ForegroundColor = old;
                }
            }
        }
        public void RunFile(string path)
        {
            RunStatements(File.ReadAllText(path).Split(';'));
        }
    }
}