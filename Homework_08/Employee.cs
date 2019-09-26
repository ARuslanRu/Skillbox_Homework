namespace Homework_08
{
    public class Employee
    {
        public int EmployeeId { get; set; }
        public string Surname { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string DepartmentName { get; set; }
        public int Salary { get; set; }
        public int NumberOfProjects { get; set; }

        public Employee()
        {

        }

        public Employee(int employeeId, string surname, string name, int age, string departmentName, int salary, int numberOfProjects)
        {
            EmployeeId = employeeId;
            Surname = surname;
            Name = name;
            Age = age;
            DepartmentName = departmentName;
            Salary = salary;
            NumberOfProjects = numberOfProjects;
        }
    }
}
