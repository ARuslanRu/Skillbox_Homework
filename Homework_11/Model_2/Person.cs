using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework_11.Model_2
{
    abstract class Person
    {
        private decimal salary;

        public int Id { get; set; }
        public string Name { get; set; }
        public abstract decimal Salary { get; }
    }
}
