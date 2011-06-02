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
    class Program
    {
        static void Main(string[] args)
        {
            var runner = new SampleRunnerWithBalancedStack();
            runner.Run();

            return;
        }
    }
}
