using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Xml.Serialization;
using Newtonsoft.Json;

namespace Homework_08
{
    class Repository
    {
        /// <summary>
        /// Список департаментов с их сотрудниками
        /// </summary>
        public List<Department> Departments { get; set; }

        public Repository()
        {
            Departments = new List<Department>();
        }

        #region Private методы

        /// <summary>
        /// Получение свободного идентификатора для создания пользователя
        /// </summary>
        /// <returns>Возвращает идентификатор</returns>
        private int GettId()
        {
            var employees = GetAllEmployee();

            int employeeId;
            if (employees.Count != 0)
            {
                int[] number = employees.Select(x => x.EmployeeId).ToArray();
                int[] missingNumbers = Enumerable.Range(number[0], number[number.Length - 1]).Except(number).ToArray();
                employeeId = missingNumbers.Length == 0 ? number.Max() + 1 : missingNumbers.FirstOrDefault();
            }
            else
            {
                employeeId = 1;
            }
            return employeeId;
        }

        #endregion

        #region Public методы

        /// <summary>
        /// Поиск департамента по имени
        /// </summary>
        /// <param name="depName">Имя департамента</param>
        /// <returns>Возвращает департамент</returns>
        public Department GetDepartment(string depName) =>
            Departments.Where(x => x.DepartmentName.Equals(depName)).FirstOrDefault();

        /// <summary>
        /// Создание нового департамента
        /// </summary>
        /// <param name="departmentName">Название департамента</param>
        public void AddNewDepartment(string departmentName)
        {
            Department dep = new Department(departmentName);
            Departments.Add(dep);
        }

        /// <summary>
        /// Создание нового сотрудника
        /// </summary>
        /// <param name="surname">Фамилия</param>
        /// <param name="name">Имя</param>
        /// <param name="age">Возраст</param>
        /// <param name="department">Департамент</param>
        /// <param name="salary">Зарплата</param>
        /// <param name="numberOfProjects">Количество проектов</param>
        public void AddNewEmployee(string surname, string name, int age, Department department, int salary, int numberOfProjects)
        {
            var employeeId = GettId();
            Employee empl = new Employee(employeeId, surname, name, age, department.DepartmentName, salary, numberOfProjects);
            department.Employees.Add(empl);
        }

        /// <summary>
        /// Получение списка всех сотрудников
        /// </summary>
        /// <returns>Возвращает список всех сотрудников</returns>
        public List<Employee> GetAllEmployee()
        {
            var employees = new List<Employee>();

            foreach (var e in Departments.Select(x => x.Employees))
            {
                employees.AddRange(e);
            }

            return employees;
        }

        /// <summary>
        /// Экспорт в XML по указанному пути
        /// </summary>
        /// <param name="path">Путь</param>
        public void ExportToXML(string path)
        {
            // Создаем сериализатор на основе указанного типа 
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<Department>));

            // Создаем поток для сохранения данных
            Stream fStream = new FileStream(path, FileMode.Create, FileAccess.Write);

            // Запускаем процесс сериализации
            xmlSerializer.Serialize(fStream, Departments);

            // Закрываем поток
            fStream.Close();
        }

        /// <summary>
        /// Импорт из указанного XML файла
        /// </summary>
        /// <param name="path">Путь</param>
        public void ImportFromXML(string path)
        {
            // Создаем сериализатор на основе указанного типа 
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<Department>));

            // Создаем поток для чтения данных
            Stream fStream = new FileStream(path, FileMode.Open, FileAccess.Read);

            // Запускаем процесс десериализации
            Departments = xmlSerializer.Deserialize(fStream) as List<Department>;

            // Закрываем поток
            fStream.Close();
        }

        /// <summary>
        /// Экспорт в Json по указанному пути
        /// </summary>
        /// <param name="path">Путь</param>
        public void ExportToJson(string path)
        {
            string json = JsonConvert.SerializeObject(Departments);
            File.WriteAllText(path, json);
        }

        /// <summary>
        /// Импорт из указанного Json файла
        /// </summary>
        /// <param name="path">Путь</param>
        public void ImportFromJson(string path)
        {
            var json = File.ReadAllText(path);
            Departments = JsonConvert.DeserializeObject<List<Department>>(json);
        }

        #endregion
    }
}
