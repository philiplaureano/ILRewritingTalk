using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Rule3.Interfaces;

namespace Rule3.Library
{
    public class Person : IPerson
    {
        public Person(int age)
        {
            Age = age;
        }

        public int Age
        {
            get;
            private set;
        }
    }   
}
