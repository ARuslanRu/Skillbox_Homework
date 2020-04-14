using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Collections.ObjectModel;

using System.Text.Json;
using System.Text.Json.Serialization;
using System.IO;
using Newtonsoft.Json;

namespace Homework_11.Model
{
    static class Repository
    {
        public static List<Employee> EmployeesDb { get; set; }

        public static List<Department> DepartmentsDb { get; set; }

        //TODO: доделать добавление сотрудников

        static Repository()
        {
            EmployeesDb = new List<Employee>();
            DepartmentsDb = new List<Department>();

            //InitRepo();
        }

        private static void InitRepo()
        {
            DepartmentsDb.Add(new Department("Департамент закупок", 1, 0));
            DepartmentsDb.Add(new Department("Отдел закупок гречки", 2, 1));
            DepartmentsDb.Add(new Department("Отдел закупок туалетной бумаги", 3, 1));

            DepartmentsDb.Add(new Department("Департамент Продаж", 4, 0));
            DepartmentsDb.Add(new Department("Подразделение оптовых продаж", 5, 4));
            DepartmentsDb.Add(new Department("Отдел продаж гречки", 6, 5));

            EmployeesDb.Add(new Manager(1, "Oleg", 1, "Директор"));
            EmployeesDb.Add(new Worker(2, "Иван", 1, "Работник", 5000));

            EmployeesDb.Add(new Manager(4, "Василий", 3, "Начальник"));
            EmployeesDb.Add(new Worker(5, "Закупщик_1", 3, "Работник", 100));
            EmployeesDb.Add(new Worker(6, "Закупщик_2", 3, "Работник", 100));
            EmployeesDb.Add(new Worker(7, "Закупщик_3", 3, "Работник", 100));
            EmployeesDb.Add(new Worker(8, "Закупщик_4", 3, "Работник", 100));
            EmployeesDb.Add(new Intertn(9, "Стажер_1", 3, "Стажер", 2000));
            EmployeesDb.Add(new Intertn(10, "Стажер_2", 3, "Стажер", 2000));
        }

        public static void AddEmployee(string name, int departmentId, string position, decimal salary)
        {
            int id = GetEmployeeId();
            //Добавление сотрудника
            switch (position)
            {
                case "Начальник":
                    EmployeesDb.Add(new Manager(id, name, departmentId, position));               
                    break;
                case "Рабочий":
                    EmployeesDb.Add(new Worker(id, name, departmentId, position, salary));
                    break;
                case "Стажер":
                    EmployeesDb.Add(new Intertn(id, name, departmentId, position, salary));
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// Добавление департамента
        /// </summary>
        /// <param name="departmentName"></param>
        /// <param name="parentId"></param>
        public static void AddDepartment(string departmentName, int parentId)
        {
            int id = GetDepartmentId();
            DepartmentsDb.Add(new Department(departmentName, id, parentId));
        }

        /// <summary>
        /// Загрузка из json
        /// </summary>
        public static void LoadData()
        {
            //Загрузка из json
            string jsonString = File.ReadAllText("data.json");

            //Десериализация 
            var restoredJson = JsonConvert.DeserializeObject<JsonData>(jsonString, new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All
            });

            EmployeesDb = restoredJson.EmployeesDb;
            DepartmentsDb = restoredJson.DepartmentsDb;

        }

        /// <summary>
        /// Сохраннение в json
        /// </summary>
        public static void SaveData()
        {
            var jsonData = new JsonData(EmployeesDb, DepartmentsDb);

            //Сериализация
            string jsonContent = JsonConvert.SerializeObject(jsonData, Formatting.Indented, new JsonSerializerSettings
            { 
                TypeNameHandling = TypeNameHandling.All
            });

            File.WriteAllText("data.json", jsonContent);
        
        }

        /// <summary>
        /// Поучение свободного идентификатора для сотрудников
        /// </summary>
        /// <returns></returns>
        private static int GetEmployeeId()
        {
            if (EmployeesDb.Count != 0)
            {
                int[] number = EmployeesDb.Select(x => x.Id).ToArray();
                int[] missingNumbers = Enumerable.Range(number[0], number[number.Length - 1]).Except(number).ToArray();
                return missingNumbers.Length == 0 ? number.Max() + 1 : missingNumbers.FirstOrDefault();
            }
            else
            {
                return 1;
            }
        }

        /// <summary>
        /// Поучение свободного идентификатора для сотрудников
        /// </summary>
        /// <returns></returns>
        private static int GetDepartmentId()
        {
            if (DepartmentsDb.Count != 0)
            {
                int[] number = DepartmentsDb.Select(x => x.Id).ToArray();
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
