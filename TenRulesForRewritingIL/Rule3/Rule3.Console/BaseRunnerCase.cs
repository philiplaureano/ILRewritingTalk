using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using Mono.Cecil;
using Rule3.Interfaces;
using Rule3.Library;

namespace Rule3.Console
{
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
}
