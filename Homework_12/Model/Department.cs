using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using Newtonsoft.Json;

namespace Homework_12.Model
{
    public class Department
    {
        private static List<Department> departments;
        static Department()
        {
            departments = new List<Department>();
        }

        [JsonIgnore]
        public static IReadOnlyList<Department> Departments { get { return departments; } }

        //Если пометить свойство этим атрибутом [JsonProperty] то при десериализации процесс зайдет сюда и перезапишет поле,
        //которое проинициализировалось сперва в констуркторе
        [JsonProperty]
        public int Id { get; private set; }
        [JsonProperty]
        public string Name { get; set; }
        [JsonProperty]
        public int ParentId { get; set; }

        //При десериализации процесс сначала проходит в конструктор
        public Department(string name, int parentId)
        {
            Name = name;
            Id = GetDepartmentId();
            ParentId = parentId;
            departments.Add(this);
        }

        private static int GetDepartmentId()
        {
            if (departments.Count != 0)
            {
                int[] number = departments.Select(x => x.Id).ToArray();
                int[] missingNumbers = Enumerable.Range(number[0], number[number.Length - 1]).Except(number).ToArray();
                return missingNumbers.Length == 0 ? number.Max() + 1 : missingNumbers.FirstOrDefault();
            }
            else
            {
                return 1;
            }
        }

        public static void LoadDepartments(List<Department> departments)
        {
            Department.departments = departments;
        }

        public static void UpdateDepartment(int id, string name)
        {
            departments.Where(x => x.Id == id).FirstOrDefault().Name = name;
        }

        public static void DeleteDepartment(int id)
        {
            var childDepartmentsId = departments.Where(x => x.ParentId == id).Select(x => x.Id).ToList(); //Находим id дочерних департаментов
            foreach (var item in childDepartmentsId)
            {
                DeleteDepartment(item); //Удаляем дочерние департаменты
            }

            //Получаем сотрудников этого департамента
            var emplsId = Employee.Employees.Where(x => x.DepartmentId == id).ToList();


            //Сотрудников этого департамента
            foreach (var item in emplsId)
            {
                Employee.DeleteEmployee(item);
            }

            //Удаляем этот департамент
            departments.Remove(
                departments.Where(x => x.Id == id).FirstOrDefault()
                );
        }
    }
}
