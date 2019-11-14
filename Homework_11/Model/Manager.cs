using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Homework_11.Model
{
    /// <summary>
    /// Управленец
    /// </summary>
    class Manager : Employee
    {
        /// <summary>
        /// Зарплата в зависимости от количесва подчиненных
        /// </summary>
        public override decimal Salary { get => GetSalary(Department); }

        /// <summary>
        /// Департамент начальником которого является сотрудник
        /// </summary>
        public Department Department { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="dolzhnost"></param>
        /// <param name="department"></param>
        public Manager(int id, string name, string dolzhnost, Department department)
            : base(id, name, dolzhnost)
        {
            Department = department;
        }

        private decimal GetSalary(Department department)
        {
            //С данной рекурсией много повторяющихся действий получается, можно ли как-то от этого избавиться?

            Debug.WriteLine($"Департамент: {department.Name} | Имя: {Name} | Должность: {Dolzhnost}");

            decimal salary;

            //Если руководитель этого департамента не в списке сотрудников самого департамет, то считаем его начальником и считаем ЗП относительно всех подчиненых
            if (!department.Employees.Contains(this))
            {
                salary = department.Employees.Where(x => x != this).Sum(x => x.Salary) * 0.1m;
            }
            //Если руководитель внутри списка сотрудников департамента, то считаем его замом и запрлату рассчитываем не учитывая других руководителей в этом отделе
            else
            {
                salary = department.Employees.Where(x => !(x is Manager)).Sum(x => x.Salary) * 0.1m;
            }

            //Учитываем ЗП начальников дочерних подразделений
            if (department != this.Department)
            {
                salary += department.Manager.Salary / 100 * 10;
            }

            //Проверяем есть ли дочернии департаменты и учитываем ЗП сотрудников дочерних департаментов
            if (department.Departments.Count != 0)
            {
                foreach (var dep in department.Departments)
                {
                    salary += GetSalary(dep);
                }
            }

            return salary;
        }
    }
}
