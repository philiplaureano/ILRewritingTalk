using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using Mono.Cecil;
using Mono.Cecil.Cil;
using Rule2.Interfaces;
using Rule2.Library;

namespace Rule2.Console
{
    public class SampleRunner
    {
        public void Run()
        {
            var greeterAssemblyLocation = typeof(ConsoleGreeter).Assembly.Location;

            // Modify the ConsoleGreeter
            var greeterAssembly = AssemblyDefinition.ReadAssembly(greeterAssemblyLocation);
            var module = greeterAssembly.MainModule;
            var greeterType = (from t in module.Types
                               where t.Name == "ConsoleGreeter"
                               select t).First();

            // Modify the greet method 
            var greeterMethod = (from m in greeterType.Methods
                                 where m.Name == "Greet"
                                 select m).First();


            ModifyGreeterMethod(greeterMethod);

            // Save the assembly for diagnostic purposes
            greeterAssembly.Write("output.dll");

            // Load the modified assembly into memory            
            ExecuteModifiedAssembly(greeterAssembly);
        }

        private void ExecuteModifiedAssembly(AssemblyDefinition greeterAssembly)
        {
            var memoryStream = new MemoryStream();
            greeterAssembly.Write(memoryStream);

            // Convert the modified assembly into
            // an assembly that will be loaded by System.Reflection
            var bytes = memoryStream.GetBuffer();
            var assembly = Assembly.Load(bytes);
            var modifiedGreeterType = assembly.GetTypes()[0];
            var greeter = (IGreeter)Activator.CreateInstance(modifiedGreeterType);
            greeter.Greet("NDC");
        }

        public virtual void ModifyGreeterMethod(MethodDefinition greeterMethod)
        {
            // Do nothing -- this is the base case that works by default
        }
    }

    public static class HelperClass
    {
        public static void WriteLine(string source, string format, object arg1)
        {
            var message = string.Format(format, arg1);
            System.Console.WriteLine("{0}: {1}", source, message);
        }

        public static void WriteLine(string format, object arg1)
        {
            System.Console.WriteLine("Greetings, {0}!", arg1);
        }

        public static void WriteLine(string target)
        {
            System.Console.WriteLine("Hello, World!");
        }
    }

    public class SampleRunnerWithBalancedStack : SampleRunner
    {
        public override void ModifyGreeterMethod(MethodDefinition greeterMethod)
        {
            var module = greeterMethod.DeclaringType.Module;
            var methodBody = greeterMethod.Body;

            // Import the HelperClass.WriteLine(string, object) reference into the modified assembly
            var replacementWriteLineMethodInfo = typeof(HelperClass).GetMethod("WriteLine", new Type[] { typeof(string), typeof(object) });
            var replacementWriteLine = module.Import(replacementWriteLineMethodInfo);

            // Replace Console.WriteLine calls with our HelperClass.WriteLine
            var callInstructions = (from instruction in methodBody.Instructions
                                    where instruction.OpCode == OpCodes.Call
                                    select instruction).ToArray();

            foreach (var callInstruction in callInstructions)
            {
                var method = callInstruction.Operand as MethodReference;
                if (method == null)
                    continue;

                if (method.Name != "WriteLine")
                    continue;

                callInstruction.Operand = replacementWriteLine;
            }
        }
    }

    public class SampleRunnerWithStackThatIsTooHigh : SampleRunner
    {
        public override void ModifyGreeterMethod(MethodDefinition greeterMethod)
        {
            var module = greeterMethod.DeclaringType.Module;
            var methodBody = greeterMethod.Body;

            // Import the HelperClass.WriteLine(string) reference into the modified assembly
            var replacementWriteLineMethodInfo = typeof(HelperClass).GetMethod("WriteLine", new Type[] { typeof(string) });
            var replacementWriteLine = module.Import(replacementWriteLineMethodInfo);

            // Replace Console.WriteLine calls with our HelperClass.WriteLine
            var callInstructions = (from instruction in methodBody.Instructions
                                    where instruction.OpCode == OpCodes.Call
                                    select instruction).ToArray();

            foreach (var callInstruction in callInstructions)
            {
                var method = callInstruction.Operand as MethodReference;
                if (method == null)
                    continue;

                if (method.Name != "WriteLine")
                    continue;

                // Notice that we're calling HelperClass.WriteLine(string), not the overload
                // with HelperClass.WriteLine(string, object)
                callInstruction.Operand = replacementWriteLine;
            }
        }
    }

    public class FixedSampleRunnerWithStackThatWasTooHigh : SampleRunner
    {
        public override void ModifyGreeterMethod(MethodDefinition greeterMethod)
        {
            var module = greeterMethod.DeclaringType.Module;
            var methodBody = greeterMethod.Body;

            // Import the HelperClass.WriteLine(string) reference into the modified assembly
            var replacementWriteLineMethodInfo = typeof(HelperClass).GetMethod("WriteLine", new Type[] { typeof(string) });
            var replacementWriteLine = module.Import(replacementWriteLineMethodInfo);

            // Replace Console.WriteLine calls with our HelperClass.WriteLine
            var callInstructions = (from instruction in methodBody.Instructions
                                    where instruction.OpCode == OpCodes.Call
                                    select instruction).ToArray();

            var processor = methodBody.GetILProcessor();
            foreach (var callInstruction in callInstructions)
            {
                var method = callInstruction.Operand as MethodReference;
                if (method == null)
                    continue;

                if (method.Name != "WriteLine")
                    continue;


                // Here's how we fix it--balance the stack by removing the extra argument
                var pop = processor.Create(OpCodes.Pop);
                processor.InsertBefore(callInstruction, pop);
                callInstruction.Operand = replacementWriteLine;
            }
        }
    }


    public class SampleRunnerWithStackUnderFlow : SampleRunner
    {
        public override void ModifyGreeterMethod(MethodDefinition greeterMethod)
        {
            var module = greeterMethod.DeclaringType.Module;
            var methodBody = greeterMethod.Body;

            // Import the HelperClass.WriteLine(string) reference into the modified assembly
            var replacementWriteLineMethodInfo = typeof(HelperClass).GetMethod("WriteLine", new Type[] { typeof(string), typeof(string), typeof(object) });
            var replacementWriteLine = module.Import(replacementWriteLineMethodInfo);

            // Replace Console.WriteLine calls with our HelperClass.WriteLine
            var callInstructions = (from instruction in methodBody.Instructions
                                    where instruction.OpCode == OpCodes.Call
                                    select instruction).ToArray();

            foreach (var callInstruction in callInstructions)
            {
                var method = callInstruction.Operand as MethodReference;
                if (method == null)
                    continue;

                if (method.Name != "WriteLine")
                    continue;

                // Notice that we're calling HelperClass.WriteLine(string, string, object) 
                callInstruction.Operand = replacementWriteLine;
            }
        }
    }

    public class FixedSampleRunnerThatHadAStackUnderFlow : SampleRunner
    {
        public override void ModifyGreeterMethod(MethodDefinition greeterMethod)
        {
            var module = greeterMethod.DeclaringType.Module;
            var methodBody = greeterMethod.Body;
            var stringType = module.Import(typeof(string));

            var formatVariable = new VariableDefinition(stringType);
            methodBody.Variables.Add(formatVariable);

            // Import the HelperClass.WriteLine(string) reference into the modified assembly
            var replacementWriteLineMethodInfo = typeof(HelperClass).GetMethod("WriteLine", new Type[] { typeof(string), typeof(string), typeof(object) });
            var replacementWriteLine = module.Import(replacementWriteLineMethodInfo);

            // Replace Console.WriteLine calls with our HelperClass.WriteLine
            var callInstructions = (from instruction in methodBody.Instructions
                                    where instruction.OpCode == OpCodes.Call
                                    select instruction).ToArray();

            var processor = methodBody.GetILProcessor();
            foreach (var callInstruction in callInstructions)
            {
                var method = callInstruction.Operand as MethodReference;
                if (method == null)
                    continue;

                if (method.Name != "WriteLine")
                    continue;

                // Notice that we're calling HelperClass.WriteLine(string, string, object), not the overload
                // with HelperClass.WriteLine(string, object)
                var saveFormat = processor.Create(OpCodes.Stloc, formatVariable);

                // No need to save the parameter from the ldarg_1 instruction since its already stored as a parameter
                var saveArgument = processor.Create(OpCodes.Pop);
                var blockStart = processor.Create(OpCodes.Nop);

                // Save the format and the arg1 arguments
                processor.InsertBefore(callInstruction, blockStart);
                processor.InsertAfter(blockStart, saveArgument);
                processor.InsertAfter(saveArgument, saveFormat);

                // Load inject the source string as "SomeOtherSource"
                var loadSourceString = processor.Create(OpCodes.Ldstr, "SomeOtherSource");
                var loadFormatString = processor.Create(OpCodes.Ldloc, formatVariable);
                var loadArg1 = processor.Create(OpCodes.Ldarg_1);

                // Reorder the parameter values to match the method signature in the HelperClass.WriteLine(string, string, object) method
                processor.InsertAfter(saveFormat, loadSourceString);
                processor.InsertAfter(loadSourceString, loadFormatString);
                processor.InsertAfter(loadFormatString, loadArg1);

                callInstruction.Operand = replacementWriteLine;
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var runner = new SampleRunnerWithStackUnderFlow();
            runner.Run();

            return;
        }
    }
}
