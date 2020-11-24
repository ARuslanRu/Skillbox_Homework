using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Homework_12.Model
{
    class JsonData
    {
        [JsonProperty]
        public List<Employee> EmployeesDb { get; set; }

        [JsonProperty]
        public List<Department> DepartmentsDb { get; set; }

        public JsonData(List<Employee> employees, List<Department> departments)
        {
            EmployeesDb = employees;
            DepartmentsDb = departments;
        }
    }
}
