using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework_11.Model
{
    class Intertn : Employee
    {
        /// <summary>
        /// Зарплата
        /// </summary>
        private decimal salary;

        /// <summary>
        /// Зарплата
        /// </summary>
        public override decimal Salary { get { return salary; } }

        /// <summary>
        /// Конструктор стажера
        /// </summary>
        /// <param name="id">Идентификатор</param>
        /// <param name="name">Имя</param>
        /// <param name="departmentId">Идентификатор департамента</param>
        /// <param name="position">Должность</param>
        /// <param name="salary">Зарпалата в мес</param>
        public Intertn(int id, string name, int departmentId, string position, decimal salary) : base(id, name, departmentId, position)
        {
            this.salary = salary;
        }
    }
}
