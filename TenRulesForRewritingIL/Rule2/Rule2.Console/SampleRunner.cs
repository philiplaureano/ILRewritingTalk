using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using Mono.Cecil;
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
}
