using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Collections.ObjectModel;

using System.IO;
using Newtonsoft.Json;

namespace Homework_12.Model
{
    static class Repository
    {


        /// <summary>
        /// Загрузка из json
        /// </summary>
        public static void LoadData()
        {
            //Загрузка из json
            if (!File.Exists("data.json"))
            {
                return;
            }

            string jsonString = File.ReadAllText("data.json");

            //Десериализация 
            var restoredJson = JsonConvert.DeserializeObject<JsonData>(jsonString, new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All
            });

            Employee.LoadEmployee(restoredJson.EmployeesDb);
            Department.LoadDepartments(restoredJson.DepartmentsDb);

        }

        /// <summary>
        /// Сохраннение в json
        /// </summary>
        public static void SaveData()
        {
            List<Employee> employees = Employee.Employees as List<Employee>;
            List<Department> departments = Department.Departments as List<Department>;

            var jsonData = new JsonData(employees, departments);

            //Сериализация
            string jsonContent = JsonConvert.SerializeObject(jsonData, Formatting.Indented, new JsonSerializerSettings
            { 
                TypeNameHandling = TypeNameHandling.All
            });

            File.WriteAllText("data.json", jsonContent);
        
        }

    }
}
