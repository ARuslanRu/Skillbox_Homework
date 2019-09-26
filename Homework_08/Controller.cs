using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework_08
{
    class Controller
    {
        Repository repo = new Repository();
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

        private void PrintEmployeeTitle()
        {
            Console.WriteLine($"{"№",5} {"Имя",12} {"Фамилия",12} {"Возраст",3} {"Департамент",15} {"Оплата труда",15} {"Количество проектов",15}");
        }

        public void AddNewDepartment()
        {
            Console.Write("Введите название департамента: ");
            var depName = Console.ReadLine();
            repo.AddNewDepartment(depName);
        }

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

        public void PrintHierarchy()
        {
            foreach (var dep in repo.Departments)
            {

                Console.WriteLine("Департамент: {0}, Дата создания: {1}, Количество сотрудников: {2}",
                    dep.DepartmentName,
                    dep.CreateDate.ToString("hh:mm:ss-dd.MM.yyyy"),
                    dep.NumberOfEmployees
                    );

                PrintEmployeeTitle();
                foreach (var empl in dep.Employees)
                {
                    PrintEmployee(empl);
                }
            }
        }

        public void PrintAllUsers()
        {
            PrintEmployeeTitle();
            foreach (var empl in repo.GetAllEmployee())
            {
                PrintEmployee(empl);
            }
        }

        public void ExportToXml()
        {
            Console.Write("Укажите путь для сохранения:");
            var path = Console.ReadLine();
            repo.ExportToXML(path);
        }

        public void ImportFromXml()
        {
            Console.Write("Укажите путь для загрузки:");
            var path = Console.ReadLine();
            repo.ImportFromXML(path);
        }

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

        public void SortByDepartmentAndAgeAndSalary()
        {
            Console.WriteLine("Упорядочивание по возрасту и оплате труда в рамках одного департамента: ");
            var employees = repo.GetAllEmployee().OrderBy(x => x.DepartmentName).ThenBy(x=>x.Age).ThenBy(x => x.Salary);

            PrintEmployeeTitle();
            foreach (var empl in employees)
            {
                PrintEmployee(empl);
            }
        }
    }
}
