class SyntaxException(Exception):
    pass
class VariableExceptions:
    class VariableException(Exception):
        pass
    class VariableDoesNotExistException(VariableException):
        pass
class FunctionExceptions:
    class FunctionException(Exception):
        pass
    class FunctionDoesNotExistException(Exception):
        pass
class ExtensionExceptions:
    class ExtensionException(Exception):
        pass
    pass

class Interpreter:
    stderr_color = 4
    stdout_bg = 0
    stdout_fg = 15

    variable_names = []
    variables = []

    function_names = []
    functions_types = []
    functions = []

    native_instance_index = []
    native_instances = []

    def PatchTogether(elements, offset):
        result = ""
        for i in range(offset, len(elements)-1):
            result += elements[i] + " "
            if len(elements) > 0:
                result += elements[len(elements) - 1]
        return result
    def PatchTogether(elements, offset, length):
        result = ""
        for i in range(0, length - 1):
            result += elements[i] + " "
        if len(length) > 0:
            result += elements[offset + length - 1]
        return result
    def AddFunction(name, code, _override):
        try:
            function_names.index(name)

            if _override:
                index = function_names.index(name)
                function_types[index] = False
                functions[index] = code
                native_instance_indexes.append(-1)
                return True
            else:
                return False
        except:
            function_names.append(name)
            function_types.append(false)
            functions.append(code)
            native_instance_indexes.Add(-1)
            return True

    def RemoveFunction(name):
        try:
            index = function_names.index(name)

            del function_names[index]
            del functions[index]
            del function_types[index]
            del native_instance_indexes[index]
            return True
        except:
            return False

    def AddNativeFunction(name, code, native_instance_index, _override):
        try:
            function_names.index(name)

            if _override:
                index = function_names.index(name)
                function_types[index] = True
                functions[index] = code
                native_instance_indexes.Add(native_instance_index)
                return True
            else:
               return False
        except:
            function_names.append(name)
            function_types.append(true)
            functions.append(code)
            native_instance_indexes.append(native_instance_index)
            return True
