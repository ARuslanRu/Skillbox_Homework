using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Homework_12.Model
{
    class Worker : Employee
    {
        /// <summary>
        /// Зарплата
        /// </summary>
        [JsonProperty]
        private decimal salary;

        /// <summary>
        /// Зарплата
        /// </summary>
        [JsonIgnore]
        public override decimal Salary
        {
            get { return salary * 8 * 20; }
            set { salary = value; }
        }

        /// <summary>
        /// Конструктор рабочего
        /// </summary>
        /// <param name="id">идентификатор</param>
        /// <param name="name">Имя</param>
        /// <param name="departmentId">Идентификатор департамента</param>
        /// <param name="position">Должность</param>
        /// <param name="salary">Зарплата в час</param>
        public Worker(string name, int departmentId, string position, decimal salary)
            : base(name, departmentId, position)
        {
            this.salary = salary;
        }
    }
}
