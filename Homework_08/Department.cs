using System;
using System.Collections.Generic;

namespace Homework_08
{
    public class Department
    {
        public string DepartmentName { get; set; }
        public DateTime CreateDate { get; set; }
        public int NumberOfEmployees { get { return Employees.Count; } }
        public List<Employee> Employees { get; set; }

        public Department()
        {

        }

        public Department(string departmentName)
        {
            DepartmentName = departmentName;
            CreateDate = DateTime.Now;           
            Employees = new List<Employee>();
        }
    }
}
