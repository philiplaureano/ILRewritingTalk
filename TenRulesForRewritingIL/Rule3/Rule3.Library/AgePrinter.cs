using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Rule3.Interfaces;

namespace Rule3.Library
{
    public class AgePrinter : IAgePrinter
    {
        public void PrintAge(IPerson person)
        {
            System.Console.WriteLine("The person's age is {0}", person.Age);
        }
    }
}
