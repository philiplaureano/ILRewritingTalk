using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mono.Cecil;
using Mono.Cecil.Cil;

namespace Rule2.Console
{
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
}
