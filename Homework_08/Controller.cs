using System;
using System.Linq;

namespace Homework_08
{
    /// <summary>
    /// Класс отвечающий за вывод в консоль
    /// </summary>
    class Controller
    {
        Repository repo = new Repository();

        /// <summary>
        /// Вывод в консоль строчки с информацией о сотруднике
        /// </summary>
        /// <param name="empl">Сотрудник</param>
        private void PrintEmployee(Employee empl)
        {
            Console.WriteLine("{0,5} {1,12} {2,12} {3,3} {4,15} {5,15} {6,15}",
                    empl.EmployeeId,
                    empl.Name,
                    empl.Surname,
                    empl.Age,
                    empl.DepartmentName,
                    empl.Salary,
                    empl.NumberOfProjects
                    );
        }
        /// <summary>
        /// Вывод в консоль заголовка
        /// </summary>
        private void PrintEmployeeTitle()
        {
            Console.WriteLine($"{"№",5} {"Имя",12} {"Фамилия",12} {"Возраст",3} {"Департамент",15} {"Оплата труда",15} {"Количество проектов",15}");
        }
        /// <summary>
        /// Вывод в консоль запроса на создание нового департамента и его добавление в репозиторий
        /// </summary>
        public void AddNewDepartment()
        {
            Console.Write("Введите название департамента: ");
            var depName = Console.ReadLine();
            repo.AddNewDepartment(depName);
        }
        /// <summary>
        /// Вывод в консоль запроса на создание нового сотрудника и его добавление в репозиторий
        /// </summary>
        public void AddNewEmployee()
        {
            Console.Write("Введите фамилию нового сотрудника: ");
            var emplSurname = Console.ReadLine();

            Console.Write("Введите имя нового сотрудника: ");
            var emplName = Console.ReadLine();

            Console.Write("Введите возраст нового сотрудника: ");
            var emplAge = int.Parse(Console.ReadLine());

            Console.Write("Введите зарплату нового сотрудника: ");
            var emplSalary = int.Parse(Console.ReadLine());

            Console.Write("Введите количество проектов нового сотрудника: ");
            var emplNumberOfProjects = int.Parse(Console.ReadLine());

            Console.WriteLine("Выберете департамент для нового сотрудника из списка ниже: ");

            foreach (var department in repo.Departments)
            {
                Console.WriteLine(department.DepartmentName);
            }
            Console.Write("Введите название департамента: ");
            var departmentName = Console.ReadLine();
            var dep = repo.GetDepartment(departmentName);

            repo.AddNewEmployee(emplSurname, emplName, emplAge, dep, emplSalary, emplNumberOfProjects);
        }
        /// <summary>
        /// Вывод в консоль списка сотрудников по департаментам
        /// </summary>
        public void PrintHierarchy()
        {
            foreach (var dep in repo.Departments)
            {

                Console.WriteLine("Департамент: {0}, Дата создания: {1}, Количество сотрудников: {2}",
                    dep.DepartmentName,
                    dep.CreateDate.ToString("HH:mm:ss-dd.MM.yyyy"),
                    dep.NumberOfEmployees
                    );

                PrintEmployeeTitle();
                foreach (var empl in dep.Employees)
                {
                    PrintEmployee(empl);
                }
            }
        }
        /// <summary>
        /// Вывод в консоль информации по всем сотрудникам
        /// </summary>
        public void PrintAllUsers()
        {
            PrintEmployeeTitle();
            foreach (var empl in repo.GetAllEmployee())
            {
                PrintEmployee(empl);
            }
        }
        /// <summary>
        /// Вывод в консоль запроса на сохранение в XML
        /// </summary>
        public void ExportToXml()
        {
            Console.Write("Укажите путь для сохранения:");
            var path = Console.ReadLine();
            repo.ExportToXML(path);
        }
        /// <summary>
        /// Вывод в консоль запроса на загрузку из XML
        /// </summary>
        public void ImportFromXml()
        {
            Console.Write("Укажите путь для загрузки:");
            var path = Console.ReadLine();
            repo.ImportFromXML(path);
        }
        /// <summary>
        /// Вывод в консоль запроса на редактирования пользователя и его сохранение изменений
        /// </summary>
        public void EditEmployee()
        {
            Console.Write("Введите имя сотрудника которого нужно изменить: ");
            var name = Console.ReadLine();

            var empl = repo.GetAllEmployee().Where(x => x.Name.Equals(name)).FirstOrDefault();

            Console.Write("Введите фамилию сотрудника: ");
            var emplSurname = Console.ReadLine();

            Console.Write("Введите имя сотрудника: ");
            var emplName = Console.ReadLine();

            Console.Write("Введите возраст сотрудника: ");
            var emplAge = int.Parse(Console.ReadLine());

            Console.Write("Введите зарплату сотрудника: ");
            var emplSalary = int.Parse(Console.ReadLine());

            Console.Write("Введите количество проектов сотрудника: ");
            var emplNumberOfProjects = int.Parse(Console.ReadLine());

            empl.Surname = emplSurname;
            empl.Name = emplName;
            empl.Age = emplAge;
            empl.Salary = emplSalary;
            empl.NumberOfProjects = emplNumberOfProjects;
        }
        /// <summary>
        /// Вывод в консоль запроса на удаление соттрудика с его удалением
        /// </summary>
        public void RemoveEmployee()
        {
            Console.Write("Введите имя сотрудника которого нужно удалить: ");
            var name = Console.ReadLine();

            var empl = repo.GetAllEmployee().Where(x => x.Name.Equals(name)).FirstOrDefault();

            foreach (var dep in repo.Departments)
            {
                dep.Employees.Remove(empl);
            }
        }
        /// <summary>
        /// Вывод в консоль списка сотрудников упорядоченного по возрасту
        /// </summary>
        public void SortByAge()
        {
            Console.WriteLine("Упорядочивание по возрасту: ");
            var employees = repo.GetAllEmployee().OrderBy(x => x.Age);

            PrintEmployeeTitle();
            foreach (var empl in employees)
            {
                PrintEmployee(empl);
            }
        }
        /// <summary>
        /// Вывод в консоль списка сотрудников упорядоченного по возрасту и затем по уровню оплаты
        /// </summary>
        public void SortByAgeAndSalary()
        {
            Console.WriteLine("Упорядочивание по возрасту и оплате труда: ");
            var employees = repo.GetAllEmployee().OrderBy(x => x.Age).ThenBy(x => x.Salary);

            PrintEmployeeTitle();
            foreach (var empl in employees)
            {
                PrintEmployee(empl);
            }
        }
        /// <summary>
        /// Вывод в консоль списка сотрудников упорядоченного по возрасту и оплате труда в рамках одного департамента
        /// </summary>
        public void SortByDepartmentAndAgeAndSalary()
        {
            Console.WriteLine("Упорядочивание по возрасту и оплате труда в рамках одного департамента: ");
            var employees = repo.GetAllEmployee().OrderBy(x => x.DepartmentName).ThenBy(x => x.Age).ThenBy(x => x.Salary);

            PrintEmployeeTitle();
            foreach (var empl in employees)
            {
                PrintEmployee(empl);
            }
        }
        /// <summary>
        /// Вывод в консоль запроса на сохранение в Json
        /// </summary>
        public void ExportToJson()
        {
            Console.Write("Укажите путь для сохранения:");
            var path = Console.ReadLine();
            repo.ExportToJson(path);
        }
        /// <summary>
        /// Вывод в консоль запроса на загрузку из Json
        /// </summary>
        public void ImportFromJson()
        {
            Console.Write("Укажите путь для загрузки:");
            var path = Console.ReadLine();
            repo.ImportFromJson(path);
        }
    }
}
