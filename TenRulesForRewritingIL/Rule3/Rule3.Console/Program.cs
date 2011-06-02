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

    class Program
    {
        static void Main(string[] args)
        {
            var runner = new WorkingRunnerCase();
            runner.Run();

            return;
        }
    }
}
