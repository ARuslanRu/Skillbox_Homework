using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework_11.Model
{
    /// <summary>
    /// Рабочий
    /// </summary>
    class Worker : Employee
    {
        public override decimal Salary { get; }

        public Worker(int id, string name, string dolzhnost, decimal salary)
            : base(id, name, dolzhnost)
        {
            Salary = salary;
        }
    }
}
