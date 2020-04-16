using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Homework_12.Model
{
    class Intertn : Employee
    {
        /// <summary>
        /// Зарплата
        /// </summary>
        [JsonProperty]
        public override decimal Salary { get; set; }

        /// <summary>
        /// Конструктор стажера
        /// </summary>
        /// <param name="id">Идентификатор</param>
        /// <param name="name">Имя</param>
        /// <param name="departmentId">Идентификатор департамента</param>
        /// <param name="position">Должность</param>
        /// <param name="salary">Зарпалата в мес</param>
        public Intertn(string name, int departmentId, string position, decimal salary) : base(name, departmentId, position)
        {
            Salary = salary;
        }
    }
}
