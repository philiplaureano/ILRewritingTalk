using System;
using System.Collections.Generic;
using System.Text;
using Rule2.Interfaces;

namespace Rule2.Library
{
    public class ConsoleGreeter : IGreeter
    {
        public void Greet(string target)
        {
            Console.WriteLine("Hello, {0}!", target);
        }
    }
}
