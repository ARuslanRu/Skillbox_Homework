﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Homework_11.Model
{
    class Manager : Employee
    {
        /// <summary>
        /// Конструктор начальника
        /// </summary>
        /// <param name="id">Идентификатор</param>
        /// <param name="name">Имя</param>
        /// <param name="departmentId">Идентификатор департамента</param>
        /// <param name="position">Должность</param>
        public Manager(int id, string name, int departmentId, string position)
            : base(id, name, departmentId, position)
        {

        }

        /// <summary>
        /// Зарплата
        /// </summary>
        [JsonIgnore]
        public override decimal Salary 
        {
            get { return GetSalary(this.DepartmentId) * 0.15m < 1300 ? 1300 : GetSalary(this.DepartmentId) * 0.15m; }
        } 


        /// <summary>
        /// Рассчет суммарной зарплаты всех подчиненных
        /// </summary>
        /// <param name="departmentId">идентификатор департамента</param>
        /// <returns>Сумма зарпалат всех подчиненных</returns>
        private decimal GetSalary(int departmentId)
        {
            decimal sum;

            if (departmentId == this.DepartmentId)
            {
                //Суммируем зарплату всех в текущем подразделении кроме начальника этого подразделения
                sum = Repository.EmployeesDb.Where(x => x.DepartmentId == departmentId && !(x is Manager)).Select(x => x.Salary).Sum();
            }
            else
            {
                //Суммируем зарплату всех сотрудников если начальник не этого подразделения
                sum = Repository.EmployeesDb.Where(x => x.DepartmentId == departmentId).Select(x => x.Salary).Sum();
            }

            var depsId = Repository.DepartmentsDb.Where(x => x.ParentId == departmentId).Select(x => x.Id);

            foreach (var id in depsId)
            {
                //Суммируем зарплату всех сотрудников всех дочерних департаментов
                sum += GetSalary(id);
            }

            return sum;
        }
    }
}
