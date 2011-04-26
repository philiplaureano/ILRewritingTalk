using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using Mono.Cecil;
using Mono.Cecil.Cil;
using Rule3.Interfaces;
using Rule3.Library;

namespace Rule3.Console
{
    public static class PropertyHelper
    {
        public static object GetValue(object target)
        {
            return 42;
        }
    }

    public class BaseRunnerCase
    {
        public void Run()
        {
            var location = typeof(AgePrinter).Assembly.Location;
            var assemblyDefinition = AssemblyDefinition.ReadAssembly(location);
            var module = assemblyDefinition.MainModule;

            var agePrinterType = (from t in module.Types
                                  where t.Name == "AgePrinter"
                                  select t).First();

            var printAgeMethod = (from m in agePrinterType.Methods
                                  where m.Name == "PrintAge"
                                  select m).First();

            RewriteMethod(printAgeMethod);
            assemblyDefinition.Write("output.dll");

            var memoryStream = new MemoryStream();
            assemblyDefinition.Write(memoryStream);

            // Convert the modified assembly into
            // an assembly that will be loaded by System.Reflection
            var bytes = memoryStream.GetBuffer();
            var assembly = Assembly.Load(bytes);

            var modifiedType = (from t in assembly.GetTypes()
                                where t.Name == "AgePrinter"
                                select t).First();

            var printer = (IAgePrinter)Activator.CreateInstance(modifiedType);
            printer.PrintAge(new Person(18));
        }

        protected virtual void RewriteMethod(MethodDefinition targetMethod)
        {
        }
    }

    public class BrokenRunnerCase : BaseRunnerCase
    {
        protected override void RewriteMethod(MethodDefinition targetMethod)
        {
            var declaringType = targetMethod.DeclaringType;
            var module = declaringType.Module;

            // Use the PropertyHelper.GetValue method
            var getValueMethod = module.Import(typeof(PropertyHelper).GetMethod("GetValue"));

            // Save the old instructions
            var body = targetMethod.Body;
            var originalInstructions = new List<Instruction>(body.Instructions.ToArray());

            // Use a local variable to keep track of the target
            body.Instructions.Clear();
            var processor = body.GetILProcessor();
            foreach (var instruction in originalInstructions)
            {
                // Ignore everything but the callvirt and call instructions
                if (instruction.OpCode != OpCodes.Callvirt && instruction.OpCode != OpCodes.Call)
                {
                    // Leave the old instruction alone
                    processor.Append(instruction);
                    continue;
                }                    

                // Replace all calls to the get_Age() method with the PropertyHelper.GetValue() method
                var currentMethod = instruction.Operand as MethodReference;
                if (currentMethod == null || currentMethod.Name != "get_Age")
                {
                    processor.Append(instruction);
                    continue;
                }

                // Cause an error by pushing the person instance onto the stack
                processor.Emit(OpCodes.Ldarg_1);
                processor.Emit(OpCodes.Call, getValueMethod);
                processor.Emit(OpCodes.Unbox_Any, module.Import(typeof(int)));
            }

            return;
        }
    }

    public class WorkingRunnerCase : BaseRunnerCase
    {
        protected override void RewriteMethod(MethodDefinition targetMethod)
        {
            var declaringType = targetMethod.DeclaringType;
            var module = declaringType.Module;

            // Use the PropertyHelper.GetValue method
            var getValueMethod = module.Import(typeof(PropertyHelper).GetMethod("GetValue"));

            // Save the old instructions
            var body = targetMethod.Body;
            var originalInstructions = new List<Instruction>(body.Instructions.ToArray());

            // Use a local variable to keep track of the target
            body.Instructions.Clear();
            var processor = body.GetILProcessor();
            foreach (var instruction in originalInstructions)
            {
                // Ignore everything but the callvirt and call instructions
                if (instruction.OpCode != OpCodes.Callvirt && instruction.OpCode != OpCodes.Call)
                {
                    // Leave the old instruction alone
                    processor.Append(instruction);
                    continue;
                }

                // Replace all calls to the get_Age() method with the PropertyHelper.GetValue() method
                var currentMethod = instruction.Operand as MethodReference;
                if (currentMethod == null || currentMethod.Name != "get_Age")
                {
                    processor.Append(instruction);
                    continue;
                }
            
                processor.Emit(OpCodes.Call, getValueMethod);
                processor.Emit(OpCodes.Unbox_Any, module.Import(typeof(int)));
            }

            return;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var runner = new BrokenRunnerCase();
            runner.Run();

            return;
        }
    }
}
