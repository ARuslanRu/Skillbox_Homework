using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace Homework_11.Model
{
    /// <summary>
    /// Сотрудник
    /// </summary>
    abstract class Employee
    {
        static ObservableCollection<Employee> employees;
        public static ObservableCollection<Employee> Employees { get { return employees; } }

        static Employee()
        {
            employees = new ObservableCollection<Employee>();
        }


        public int EmployeeID { get; }
        public string Name { get; set; }

        public Employee(string name)
        {
            Name = name;
            EmployeeID = GetId();
            Employees.Add(this);
        }

        private static int GetId()
        {
            if (employees.Count != 0)
            {
                int[] number = employees.Select(x => x.EmployeeID).ToArray();
                int[] missingNumbers = Enumerable.Range(number[0], number[number.Length - 1]).Except(number).ToArray();
                return missingNumbers.Length == 0 ? number.Max() + 1 : missingNumbers.FirstOrDefault();
            }
            else
            {
                return 1;
            }
        }
    }
}
