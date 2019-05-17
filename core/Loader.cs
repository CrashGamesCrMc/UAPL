using System;

namespace Addon
{
    public class Loader
    {
        public string[][] GetFunctions() => new string[][] { new string[] { "==", "equals" }, new string[] { "and", "and" }, new string[] { "set", "set" },new string[] { "del", "del"},
            new string[]{ "+=", "add"}, new string[]{ "+", "addn" }, new string[]{ "-=", "substract" }, new string[]{ "-", "substractn" },
            new string[]{ "*=", "multiply" }, new string[]{"*","multiplyn" }, new string[]{ "/=","divide" }, new string[]{ "/", "dividen" } };
    }
    public class Functions
    {
        public bool equals(string statement, string[] split, dynamic interpreter)
        {
            return interpreter.ParseValue(split[2]).Equals(interpreter.ParseValue(split[3]));
        }

        public bool and(string cmd, string[] split, dynamic interpreter)
        {
            return interpreter.ParseValue(split[2]) && interpreter.ParseValue(split[3]);
        }

        public void set(string cmd, string[] split, dynamic interpreter)
        {
            interpreter.variables[interpreter.variable_names.IndexOf(split[2])] = interpreter.ParseValue(split[3]);
        }

        public void del(string cmd, string[] split, dynamic interpreter)
        {
            interpreter.variables.Remove(split[2]);
        }

        public void add(string cmd, string[] split, dynamic interpreter)
        {
            interpreter.variables[interpreter.variable_names.IndexOf(split[2])] += interpreter.ParseValue(split[3]);
        }
        
        public dynamic addn(string cmd, string[] split, dynamic interpreter)
        {
            return interpreter.ParseValue(split[2]) + interpreter.ParseValue(split[3]);
        }

        public void subtract(string cmd, string[] split, dynamic interpreter)
        {
            interpreter.variables[interpreter.variable_names.IndexOf(split[2])] -= interpreter.ParseValue(split[3]);
        }

        public dynamic substractn(string cmd, string[] split, dynamic interpreter)
        {
            return interpreter.ParseValue(split[2]) + interpreter.ParseValue(split[3]);
        }

        public void multiply(string cmd, string[] split, dynamic interpreter)
        {
            interpreter.variables[interpreter.variable_names.IndexOf(split[2])] *= interpreter.ParseValue(split[3]);
        }

        public dynamic multiplyn(string cmd, string[] split, dynamic interpreter)
        {
            return interpreter.variables[interpreter.variable_names.IndexOf(split[2])] * interpreter.ParseValue(split[3]);
        }

        public void divide(string cmd, string[] split, dynamic interpreter)
        {
            interpreter.variables[interpreter.variable_names.IndexOf(split[2])] /= interpreter.ParseValue(split[3]);
        }

        public dynamic dividen(string cmd, string[] split, dynamic interpreter)
        {
            return interpreter.variables[interpreter.variable_names.IndexOf(split[2])] / interpreter.ParseValue(split[3]);
        }
    }
}
