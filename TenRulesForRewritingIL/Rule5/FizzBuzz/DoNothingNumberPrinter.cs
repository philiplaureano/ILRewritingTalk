using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FizzBuzz.Interfaces;

namespace FizzBuzz
{
    public class DoNothingNumberPrinter : INumberPrinter 
    {
        public void Print(int number)
        {
            throw new NotImplementedException();
        }
    }
}
