using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework_11.Model
{
    class Department
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Manager Manager { get; set; }
        public List<Employee> Employees { get; set; } // Возможно стоит заменить на приватное поле и методы для доступа к нему
        public List<Department> Departments { get; set; }

        public Department(int departmentId, string departmentName, Manager manager)
        {
            Id = departmentId;
            Name = departmentName;
            Manager = manager;
            Employees = new List<Employee>();
            Departments = new List<Department>();
        }
    }
}
