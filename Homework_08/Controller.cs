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

            Console.WriteLine($"Выбран департамен: {dep.DepartmentName} дата создания: {dep.CreateDate}");

            repo.AddNewEmployee(emplSurname, emplName, emplAge, dep, emplSalary, emplNumberOfProjects);
        }

        public void PrintAll()
        {
            foreach (var dep in repo.Departments)
            {
                Console.WriteLine($"\t{dep.DepartmentName}");

                foreach (var empl in dep.Employees)
                {
                    Console.WriteLine($"\t\t{empl.EmployeeId} {empl.Surname} {empl.Name} {empl.Age}");
                }
            }

        }

        public void Test()
        {
            //repo.Departments.
        }
    }
}
