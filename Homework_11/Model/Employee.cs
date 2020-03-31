using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework_11.Model
{
    abstract class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int DepartmentId { get; set; }
        public string Position { get; set; }
        public abstract decimal Salary { get; }
        public Employee(int id, string name, int departmentId, string position)
        {
            Id = id;
            Name = name;
            Position = position;
            DepartmentId = departmentId;
        }
    }
}
