using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace uapl
{
    public class Exceptions
    {
        public class SyntaxException : Exception
        {
            public SyntaxException(string message) : base(message)
            {
            }
        }
        public class VariableExceptions
        {
            public class VariableException : Exception
            {
                public VariableException(string message) : base(message)
                {
                }
            }
            public class VariableDoesNotExistException : VariableException
            {
                public VariableDoesNotExistException(string message) : base(message)
                {
                }
            }
        }
        public class FunctionExcepetions
        {
            public class FunctionException : Exception
            {
                public FunctionException(string message) : base(message)
                {
                }
            }
            public class FunctionDoesNotExistException : FunctionException
            {
                public FunctionDoesNotExistException(string message) : base(message)
                {
                }
            }
        }
    }
}
