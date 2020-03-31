using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Collections.ObjectModel;

using System.Text.Json;
using System.Text.Json.Serialization;
using System.IO;

namespace Homework_11.Model
{
    static class Repository
    {
        public static List<Employee> EmployeesDb { get; set; }
        public static List<Department> DepartmentsDb { get; set; }

        static Repository()
        {
            EmployeesDb = new List<Employee>();
            DepartmentsDb = new List<Department>();

            InitRepo();
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

        public static void AddEmployee(Employee employee)
        {
            //Добавление сотрудника
        }

        public static void AddDepartment(Department department)
        {
            //Добавление департамента
        }

        public static void LoadData()
        {
            //Загрузка из json
        }

        public static async void SaveData()
        {
            //сохранение в json
            using (FileStream fs = File.Create("test.json"))
            {
               await JsonSerializer.SerializeAsync(fs, EmployeesDb);
            }
        }
    }
}
