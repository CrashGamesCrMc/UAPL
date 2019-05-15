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
        public List<String> variable_names = new List<string>();
        public List<dynamic> variables = new List<dynamic>();

        public List<String> function_names = new List<string>();
        public List<bool> function_types = new List<bool>(); // is it native?
        public List<dynamic> function = new List<dynamic>(); // where is the function located in text?

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

                    foreach ()

                }
                else
                {

                }
            }
            return null;
        }
    }
}