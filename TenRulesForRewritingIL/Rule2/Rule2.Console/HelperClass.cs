using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rule2.Console
{
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
}
