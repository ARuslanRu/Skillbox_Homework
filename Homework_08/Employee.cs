using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework_08
{
    struct Employee
    {
        public string Surname { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string DepartmentName { get; set; }
        public int EmployeeId { get; set; }
        public int Salary { get; set; }
        public int NumberOfProjects { get; set; }

        public Employee(string surname, string name, int age, string departmentName, int employeeId, int salary, int numberOfProjects)
        {
            Surname = surname;
            Name = name;
            Age = age;
            DepartmentName = departmentName;
            EmployeeId = employeeId;
            Salary = salary;
            NumberOfProjects = numberOfProjects;
        }

    }
}
