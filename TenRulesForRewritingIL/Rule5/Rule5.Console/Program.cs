using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using FizzBuzz;
using FizzBuzz.Interfaces;
using Mono.Cecil;
using Mono.Cecil.Cil;

namespace Rule5.Console
{
    public static class ILProcessorExtensions
    {
        public static void EmitWriteLine(this ILProcessor processor, string text)
        {
            var body = processor.Body;
            var method = body.Method;
            var module = method.Module;
            var writeLine = module.Import(typeof(System.Console).GetMethod("WriteLine", new Type[] { typeof(string) }));

            processor.Emit(OpCodes.Ldstr, text);
            processor.Emit(OpCodes.Call, writeLine);
        }
    }

    public class Program
    {
        public static void Main(string[] args)
        {
            RunFizzBuzz(ErrantAddFizzBuzzBehavior);
            return;
        }

        public static void ErrantAddFizzBuzzBehavior(TypeDefinition printerType)
        {
            // This method emits a valid CIL method but
            // it contains a logical error that cannot be directly debugged

            var printMethod = (from m in printerType.Methods
                               where m.Name == "Print"
                               select m).First();

            var module = printerType.Module;
            var boolType = module.Import(typeof(bool));

            var body = printMethod.Body;
            body.InitLocals = true;

            var isFizz = new VariableDefinition(boolType);
            var isBuzz = new VariableDefinition(boolType);

            body.Variables.Add(isFizz);
            body.Variables.Add(isBuzz);

            // Remove the original instructions altogether            
            body.Instructions.Clear();
            var processor = body.GetILProcessor();

            // var isFizz = num % 3 == 0;
            EmitIsDivisibleBy(processor, 3, isFizz);

            // var isBuzz = num % 5 == 0;
            EmitIsDivisibleBy(processor, 5, isBuzz);

            processor.EmitWriteLine("--Begin--");

            var endLabel = processor.Create(OpCodes.Nop);
            var printFizzOrBuzz = processor.Create(OpCodes.Nop);
            // if (isFizz && isBuzz) {
            processor.Emit(OpCodes.Ldloc, isFizz);
            processor.Emit(OpCodes.Ldloc, isBuzz);
            processor.Emit(OpCodes.And);

            processor.EmitWriteLine("--Marker--");

            processor.Emit(OpCodes.Brfalse, printFizzOrBuzz);

            // Console.WriteLine("FizzBuzz");
            processor.EmitWriteLine("FizzBuzz");

            // return;
            processor.Emit(OpCodes.Br, endLabel);
            // }
            
            processor.Append(printFizzOrBuzz);

            var skipFizz = processor.Create(OpCodes.Nop);
            processor.Emit(OpCodes.Ldloc, isFizz);
            processor.Emit(OpCodes.Brfalse, skipFizz);
            
            processor.EmitWriteLine("Fizz");
            processor.Append(skipFizz);

            var skipBuzz = processor.Create(OpCodes.Nop);
            processor.Emit(OpCodes.Ldloc, isBuzz);
            processor.Emit(OpCodes.Brfalse, skipFizz);

            processor.EmitWriteLine("Buzz");
            processor.Append(skipBuzz);

            processor.Append(endLabel);

            processor.EmitWriteLine("--End--");
            processor.Emit(OpCodes.Ret);
        }

        public static void AddFizzBuzzBehavior(TypeDefinition printerType)
        {
            var printMethod = (from m in printerType.Methods
                               where m.Name == "Print"
                               select m).First();

            var module = printerType.Module;
            var boolType = module.Import(typeof(bool));

            var body = printMethod.Body;
            body.InitLocals = true;

            var isFizz = new VariableDefinition(boolType);
            var isBuzz = new VariableDefinition(boolType);

            body.Variables.Add(isFizz);
            body.Variables.Add(isBuzz);

            // Remove the original instructions altogether            
            body.Instructions.Clear();
            var processor = body.GetILProcessor();

            
            // var isFizz = num % 3 == 0;
            EmitIsDivisibleBy(processor, 3, isFizz);

            // var isBuzz = num % 5 == 0;
            EmitIsDivisibleBy(processor, 5, isBuzz);
            processor.EmitWriteLine("--Begin--");

            
            var endLabel = processor.Create(OpCodes.Nop);
            var printFizzOrBuzz = processor.Create(OpCodes.Nop);
            // if (isFizz && isBuzz) {
            processor.Emit(OpCodes.Ldloc, isFizz);
            processor.Emit(OpCodes.Ldloc, isBuzz);
            processor.Emit(OpCodes.And);

            processor.EmitWriteLine("--Marker--");

            processor.Emit(OpCodes.Brtrue, printFizzOrBuzz);

            // Console.WriteLine("FizzBuzz");
            processor.EmitWriteLine("FizzBuzz");

            // return;
            
            processor.Emit(OpCodes.Br, endLabel);
            // }

            processor.Append(printFizzOrBuzz);

            var skipFizz = processor.Create(OpCodes.Nop);
            processor.Emit(OpCodes.Ldloc, isFizz);
            processor.Emit(OpCodes.Brfalse, skipFizz);

            processor.EmitWriteLine("Fizz");
            processor.Append(skipFizz);

            var skipBuzz = processor.Create(OpCodes.Nop);
            processor.Emit(OpCodes.Ldloc, isBuzz);
            processor.Emit(OpCodes.Brfalse, skipFizz);

            processor.EmitWriteLine("Buzz");
            processor.Append(skipBuzz);

            processor.Append(endLabel);

            processor.EmitWriteLine("--End--");
            processor.Emit(OpCodes.Ret);
        }

        private static void EmitIsDivisibleBy(ILProcessor processor, int divisor, VariableDefinition storageVariable)
        {
            var skipSetFlag = processor.Create(OpCodes.Nop);
            processor.Emit(OpCodes.Ldarg_1);
            processor.Emit(OpCodes.Ldc_I4, divisor);
            processor.Emit(OpCodes.Rem);

            // var isDivisible = remainder == 0;
            processor.Emit(OpCodes.Brfalse, skipSetFlag);
            processor.Emit(OpCodes.Ldc_I4_1);

            processor.Emit(OpCodes.Stloc, storageVariable);
            processor.Append(skipSetFlag);
        }

        public static void RunFizzBuzz(Action<TypeDefinition> modifyPrinterType)
        {
            var assemblyLocation = typeof(DoNothingNumberPrinter).Assembly.Location;
            var assemblyDefinition = AssemblyDefinition.ReadAssembly(assemblyLocation);
            var module = assemblyDefinition.MainModule;

            var printerType = (from t in module.Types
                               where t.Name == "DoNothingNumberPrinter"
                               select t).First();

            modifyPrinterType(printerType);

            // Save the modified assembly and load it
            var stream = new MemoryStream();
            assemblyDefinition.Write(stream);

            // Keep a copy of the assembly on disk 
            // in case we need to use PEVerify to examine it
            assemblyDefinition.Write("output.dll");

            var bytes = stream.ToArray();
            var loadedAssembly = Assembly.Load(bytes);

            var modifiedType = (from t in loadedAssembly.GetTypes()
                                where t.Name == "DoNothingNumberPrinter"
                                select t).First();

            var printer = (INumberPrinter)Activator.CreateInstance(modifiedType);

            printer.Print(15);
        }
    }
}
