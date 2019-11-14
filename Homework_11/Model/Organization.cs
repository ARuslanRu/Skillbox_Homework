using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Homework_11.Model;

namespace Homework_11.Model
{
    class Organization
    {
        public List<Department> Departments { get; set; }

        public Organization()
        {
            Departments = new List<Department>();

            //Тестовые данные
            //Создаем департаменты без начальников
            Department dep1 = new Department(1, "Первый департамент", null);
            Department dep2 = new Department(2, "Второй департамент", null);
            Department dep3 = new Department(3, "Третий департамент", null);
            dep1.Departments.Add(dep2);
            dep2.Departments.Add(dep3);
            Departments.Add(dep1);

            //Добавляем начальников
            dep1.Manager = new Manager(1, "Имя_1", "Начальник_1", dep1);
            dep2.Manager = new Manager(1, "Имя_2", "Начальник_11", dep2);
            dep3.Manager = new Manager(1, "Имя_3", "Начальник_111", dep3);

            //Добавляем сотрудников
            dep1.Employees.Add(new Worker(2, "Имя_4", "Рабочий_1", 100));
            dep1.Employees.Add(new Worker(3, "Имя_5", "Рабочий_3", 100));

            dep2.Employees.Add(new Manager(1, "Имя_6", "Зам_Начальник_12", dep2));
            dep2.Employees.Add(new Worker(2, "Имя_7", "Рабочий_11", 100));
            dep2.Employees.Add(new Worker(3, "Имя_8", "Рабочий_12", 100));

            dep3.Employees.Add(new Manager(1, "Имя_9", "Зам_Начальник_112", dep3));
            dep3.Employees.Add(new Manager(1, "Имя_12", "Зам_Начальник_113", dep3));
            dep3.Employees.Add(new Worker(2, "Имя_10", "Рабочий_11", 100));
            dep3.Employees.Add(new Worker(3, "Имя_11", "Рабочий_12", 100));

        }

    }
}
