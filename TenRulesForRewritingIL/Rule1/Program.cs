using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using Mono.Cecil;
using Mono.Cecil.Cil;
using MethodAttributes = Mono.Cecil.MethodAttributes;
using MethodBody = Mono.Cecil.Cil.MethodBody;

namespace Rule1
{
    public static class MethodBodyExtensions
    {
        public static VariableDefinition AddLocal<T>(this MethodBody methodBody)
        {
            return AddLocal(methodBody, typeof(T));
        }

        public static VariableDefinition AddLocal(this MethodBody methodBody, Type variableType)
        {
            var method = methodBody.Method;
            var module = method.Module;
            var localType = module.Import(variableType);
            var local = new VariableDefinition(localType);

            methodBody.Variables.Add(local);

            return local;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            PrintOddOrEvenUsingCecil();
        }

        private static void PrintOddOrEvenUsingCecil()
        {
            var name = new AssemblyNameDefinition("PrintOddOrEven", new Version());
            var assembly = AssemblyDefinition.CreateAssembly(name, "FizzBuzzModule", ModuleKind.Dll);
            var module = assembly.MainModule;

            var fizzBuzzMethod = new MethodDefinition("PrintOddOrEven", MethodAttributes.Public | MethodAttributes.Static,
                                                      module.Import(typeof(void)));

            var moduleType = module.Types[0];
            moduleType.Methods.Add(fizzBuzzMethod);

            var body = fizzBuzzMethod.Body;
            var number = body.AddLocal(typeof(int));
            var isDivisible = body.AddLocal(typeof(bool));

            var processor = body.GetILProcessor();
            var printHelloNdc = processor.Create(OpCodes.Nop);
            
            var endLabel = processor.Create(OpCodes.Nop);
            var writeLine = module.Import(typeof(Console).GetMethod("WriteLine", new Type[] { typeof(string) }));
            
            // var number = 42;
            processor.Emit(OpCodes.Ldc_I4, 42);
            processor.Emit(OpCodes.Stloc_0);

            // if (number % 2) == 0 {
            processor.Emit(OpCodes.Ldloc_0);
            processor.Emit(OpCodes.Ldc_I4_2);
            processor.Emit(OpCodes.Rem);
            processor.Emit(OpCodes.Ldc_I4_0);
            processor.Emit(OpCodes.Ceq);
            processor.Emit(OpCodes.Ldc_I4_0);
            processor.Emit(OpCodes.Ceq);
            processor.Emit(OpCodes.Stloc_1);
            processor.Emit(OpCodes.Ldloc_1);
            processor.Emit(OpCodes.Brtrue, printHelloNdc);

            // Console.WriteLine("Hello, World!");
            processor.Emit(OpCodes.Ldstr, "Hello, World!");
            processor.Emit(OpCodes.Call, writeLine);
            processor.Emit(OpCodes.Br, endLabel);
            processor.Append(printHelloNdc);
            // }
            
            // else {
            // Console.WriteLine("Hello, NDC!");
            processor.Emit(OpCodes.Ldstr, "Hello, NDC!");
            processor.Emit(OpCodes.Call, writeLine);

            // }
            processor.Append(endLabel);
            processor.Emit(OpCodes.Ret);

            var stream = new MemoryStream();
            assembly.Write(stream);

            var bytes = stream.ToArray();
            var loadedAssembly = Assembly.Load(bytes);
            var loadedModule = loadedAssembly.GetModules()[0];
            var targetMethod = loadedModule.GetMethod("PrintOddOrEven");
            targetMethod.Invoke(null, new object[0]);
        }

        private static void PrintOddOrEven()
        {
            int number = 42;
            if (number % 2 == 0)
                Console.WriteLine("Hello, World!");
            else
                Console.WriteLine("Hello, NDC!");
        }
    }
}
