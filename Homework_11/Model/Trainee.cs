using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework_11.Model
{
    /// <summary>
    /// Стажер
    /// </summary>
    class Trainee : Employee
    {
        //TODO: Реализация
        public override decimal Salary { get; } 

        public Trainee(int id, string name, string dolzhnost, decimal salary)
            : base(id, name, dolzhnost)
        {
            Salary = salary;
        }

    }
}
