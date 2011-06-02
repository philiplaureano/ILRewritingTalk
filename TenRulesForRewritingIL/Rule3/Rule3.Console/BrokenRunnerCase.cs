using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mono.Cecil;
using Mono.Cecil.Cil;

namespace Rule3.Console
{
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
}
