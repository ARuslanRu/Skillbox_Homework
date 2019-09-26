using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework_08
{
    class Repository
    {
        public List<Department> Departments { get; set; }

        public Repository()
        {
            Departments = new List<Department>();
        }

        #region Private методы

        private int GettId()
        {
            var employees = new List<Employee>();

            foreach (var e in Departments.Select(x => x.Employees))
            {
                employees.AddRange(e);
            }

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
        /// Добавление нового сотрудника
        /// </summary>
        /// <param name="surname"></param>
        /// <param name="name"></param>
        /// <param name="age"></param>
        /// <param name="departmentId"></param>
        /// <param name="salary"></param>
        /// <param name="numberOfProjects"></param>
        public void AddNewEmployee(string surname, string name, int age, Department department, int salary, int numberOfProjects)
        {
            var employeeId = GettId();
            Employee empl = new Employee(employeeId, surname, name, age, department.DepartmentName, salary, numberOfProjects);
            department.Employees.Add(empl);
        }

        #endregion
    }
}
