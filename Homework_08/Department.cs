using System;
using System.Collections.Generic;

namespace Homework_08
{
    class Department
    {
        public string DepartmentName { get; }
        public DateTime CreateDate { get; }
        public int NumberOfEmployees { get { return Employees.Count; } }
        public List<Employee> Employees { get; set; }

        public Department(string departmentName)
        {
            DepartmentName = departmentName;
            CreateDate = DateTime.Now;           
            Employees = new List<Employee>();
        }
    }
}
