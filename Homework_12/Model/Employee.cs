using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Homework_12.Model
{
    public abstract class Employee
    {
        private static List<Employee> employees;
        static Employee()
        {
            employees = new List<Employee>();
        }

        public static IReadOnlyList<Employee> Employees { get { return employees; } }
        public int Id { get; set; }
        public string Name { get; set; }
        public int DepartmentId { get; set; }
        public string Position { get; set; }
        public virtual decimal Salary { get; set; }

        public Employee(string name, int departmentId, string position)
        {
            Id = GetEmployeeId();
            Name = name;
            Position = position;
            DepartmentId = departmentId;

            employees.Add(this);
        }


        /// <summary>
        /// Поучение свободного идентификатора
        /// </summary>
        /// <returns></returns>
        private static int GetEmployeeId()
        {
            if (employees.Count != 0)
            {
                int[] number = employees.Select(x => x.Id).ToArray();
                int[] missingNumbers = Enumerable.Range(number[0], number[number.Length - 1]).Except(number).ToArray();
                return missingNumbers.Length == 0 ? number.Max() + 1 : missingNumbers.FirstOrDefault();
            }
            else
            {
                return 1;
            }
        }

        public static void LoadEmployee(List<Employee> employees)
        {
            Employee.employees = employees;
        }

        public static void DeleteEmployee(Employee employee)
        {
            employees.Remove(employee);
        }
    }
}
