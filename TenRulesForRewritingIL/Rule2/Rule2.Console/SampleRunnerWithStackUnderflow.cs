using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mono.Cecil;
using Mono.Cecil.Cil;

namespace Rule2.Console
{
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
}
