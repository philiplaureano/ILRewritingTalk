using System;
using System.Collections.Generic;
using System.Text;

namespace Rule1
{
    class Program
    {
        static void Main(string[] args)
        {
            int number = 42;
            if (number % 2 == 0)
                Console.WriteLine("Hello, World!");
            else
                Console.WriteLine("Hello, NDC!");
        }
    }
}
