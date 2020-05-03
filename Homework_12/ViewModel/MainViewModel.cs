using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Homework_12.Model;
using System.Windows;
using System.Diagnostics;
using System.IO;
using Newtonsoft.Json;

namespace Homework_12.ViewModel
{
    class MainViewModel : INotifyPropertyChanged
    {
        private Node selectedNode;
        public Node SelectedNode
        {
            get { return selectedNode; }
            set
            {
                selectedNode = value;
                SelectedDepartment = Department.Departments.Where(x => x.Id == SelectedNode.Id).FirstOrDefault();
                EmployeesInDepartment = Employee.Employees.Where(x => x.DepartmentId == SelectedNode.Id).ToList();
                OnPropertyChanged("SelectedNode");
            }
        }

        private Department selectedDepartment;
        public Department SelectedDepartment
        {
            get { return selectedDepartment; }
            set
            {
                selectedDepartment = value;
                OnPropertyChanged("SelectedDepartment");
            }
        }

        private Employee selectedEmployee;
        public Employee SelectedEmployee
        {
            get { return selectedEmployee; }
            set
            {
                selectedEmployee = value;
                OnPropertyChanged("SelectedEmployee");
            }
        }

        private ObservableCollection<Node> nodes;
        public ObservableCollection<Node> Nodes
        {
            get { return nodes; }
            set
            {
                nodes = value;
                OnPropertyChanged("Nodes");
            }
        }

        private IEnumerable<Employee> employeesInDepartment;
        public IEnumerable<Employee> EmployeesInDepartment
        {
            get { return employeesInDepartment; }
            set
            {
                employeesInDepartment = value;
                OnPropertyChanged("EmployeesInDepartment");
            }
        }


        #region Команды по департаментам

        private RelayCommand addDepartment;
        public RelayCommand AddDepartment
        {
            get
            {
                return addDepartment ??
                    (addDepartment = new RelayCommand(obj =>
                    {
                        AddDepartmentWindow departmentWindow;
                        if (SelectedNode == null)
                        {
                            if (Nodes == null)
                            {
                                Nodes = new ObservableCollection<Node>();
                            }

                            ObservableCollection<Node> RootNodes = Nodes;
                            departmentWindow = new AddDepartmentWindow(RootNodes, SelectedDepartment);
                        }
                        else
                        {
                            ObservableCollection<Node> ChildNodes = SelectedNode.Nodes;
                            var selectdep = Department.Departments.Where(x => x.Id == SelectedNode.Id).FirstOrDefault();
                            departmentWindow = new AddDepartmentWindow(ChildNodes, selectdep);
                        }

                        departmentWindow.Owner = obj as Window;
                        departmentWindow.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                        departmentWindow.ShowDialog();
                    }));

            }
        }

        private RelayCommand updateDepartment;
        public RelayCommand UpdateDepartment
        {
            get
            {
                return updateDepartment ??
                    (updateDepartment = new RelayCommand(obj =>
                    {
                        UpdateDepartmentWindow departmentWindow;
                        departmentWindow = new UpdateDepartmentWindow(SelectedNode, SelectedDepartment);

                        departmentWindow.Owner = obj as Window;
                        departmentWindow.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                        departmentWindow.ShowDialog();
                    }, obj => SelectedNode != null));

            }
        }

        private RelayCommand deleteDepartment;
        public RelayCommand DeleteDepartment
        {
            get
            {
                return deleteDepartment ??
                    (deleteDepartment = new RelayCommand(obj =>
                    {
                        if (MessageBox.Show("Удаление департамента приведет к удалению всех дочерних департаментов и сотрудников.\nПодтвердите удаление.",
                            "Подтверждение",
                            MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                        {
                            ObservableCollection<Node> parentNode = GetParentNode(Nodes, SelectedNode);
                            Department.DeleteDepartment(SelectedNode.Id);
                            parentNode.Remove(SelectedNode);
                        }
                    }, obj => SelectedNode != null));
            }
        }

        #endregion

        #region Команды по сотрудникам

        private RelayCommand addEmployee;
        public RelayCommand AddEmployee
        {
            get
            {
                return addEmployee ??
                    (addEmployee = new RelayCommand(obj =>
                    {
                        EmployeeWindow employeeWindow = new EmployeeWindow(SelectedDepartment);

                        employeeWindow.Owner = obj as Window;
                        employeeWindow.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                        employeeWindow.ShowDialog();

                        EmployeesInDepartment = Employee.Employees.Where(x => x.DepartmentId == SelectedNode.Id)
                        .OrderBy(x => x.Id).ToList();
                    }, obj => SelectedDepartment != null));
            }
        }


        private RelayCommand updateEmployee;
        public RelayCommand UpdateEmployee
        {
            get
            {
                return updateEmployee ??
                    (updateEmployee = new RelayCommand(obj =>
                    {
                        EmployeeWindow employeeWindow = new EmployeeWindow(SelectedDepartment, SelectedEmployee);

                        employeeWindow.Owner = obj as Window;
                        employeeWindow.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                        employeeWindow.ShowDialog();

                        EmployeesInDepartment = Employee.Employees.Where(x => x.DepartmentId == SelectedNode.Id)
                        .OrderBy(x => x.Id).ToList();
                    }, obj => SelectedEmployee != null));
            }
        }

        private RelayCommand deleteEmployee;
        public RelayCommand DeleteEmployee
        {
            get
            {
                return deleteEmployee ??
                    (deleteEmployee = new RelayCommand(obj =>
                    {

                        if (MessageBox.Show("Удаление сотрудника.\nПодтвердите удаление.", "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                        {
                            Employee.DeleteEmployee(SelectedEmployee); //Удаляет из хранилища
                            EmployeesInDepartment = Employee.Employees.Where(x => x.DepartmentId == SelectedNode.Id).ToList();
                        }
                    }, obj => SelectedEmployee != null)); ;
            }
        }

        #endregion

        #region Команды сортировки

        private RelayCommand sortById;
        public RelayCommand SortById
        {
            get
            {
                return sortById ??
                    (sortById = new RelayCommand(obj =>
                    {
                        EmployeesInDepartment = EmployeesInDepartment.OrderBy(x => x.Id);
                    }));
            }
        }

        private RelayCommand sortByName;
        public RelayCommand SortByName
        {
            get
            {
                return sortByName ??
                    (sortByName = new RelayCommand(obj =>
                    {
                        EmployeesInDepartment = EmployeesInDepartment.OrderBy(x => x.Name);
                    }));
            }
        }

        private RelayCommand sortByPosition;
        public RelayCommand SortByPosition
        {
            get
            {
                return sortByPosition ??
                    (sortByPosition = new RelayCommand(obj =>
                    {
                        EmployeesInDepartment = EmployeesInDepartment.OrderBy(x => x.Position);
                    }));
            }
        }

        private RelayCommand sortBySalary;
        public RelayCommand SortBySalary
        {
            get
            {
                return sortBySalary ??
                    (sortBySalary = new RelayCommand(obj =>
                    {
                        EmployeesInDepartment = EmployeesInDepartment.OrderBy(x => x.Salary);
                    }));
            }
        }



        #endregion

        public MainViewModel()
        {
            //new Department("Департмент_1", 0);
            //new Department("Департмент_2", 0);
            //new Department("Департмент_3", 0);
            //new Department("Департмент_4", 1);
            //new Department("Департмент_5", 1);
            //new Department("Департмент_6", 4);

            //new Manager("Имя_1", 1, "Начальник");
            //new Worker("Имя_4", 1, "Рабочий", 100);
            //new Intertn("Имя_5", 1, "Стажер", 100);
            //new Worker("Имя_2", 2, "Рабочий", 100);
            //new Intertn("Имя_3", 3, "Стажер", 100);

            //Nodes = GetTreeViewNodes();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

        #region Команды сохранения в Json и загрузки из Json

        private RelayCommand loadFromJson;
        public RelayCommand LoadFromJson
        {
            get
            {
                return loadFromJson ??
                    (loadFromJson = new RelayCommand(obj =>
                    {
                        //Загрузка из json
                        if (!File.Exists("data.json"))
                        {
                            return;
                        }

                        string jsonString = File.ReadAllText("data.json");

                        //Десериализация 
                        var restoredJson = JsonConvert.DeserializeObject<JsonData>(jsonString, new JsonSerializerSettings
                        {
                            TypeNameHandling = TypeNameHandling.All
                        });

                        Employee.LoadEmployee(restoredJson.EmployeesDb);
                        Department.LoadDepartments(restoredJson.DepartmentsDb);

                        Nodes = GetTreeViewNodes();
                    }));
            }
        }

        private RelayCommand saveToJson;
        public RelayCommand SaveToJson
        {
            get
            {
                return saveToJson ??
                    (saveToJson = new RelayCommand(obj =>
                    {
                        List<Employee> employees = Employee.Employees as List<Employee>;
                        List<Department> departments = Department.Departments as List<Department>;

                        var jsonData = new JsonData(employees, departments);

                        //Сериализация
                        string jsonContent = JsonConvert.SerializeObject(jsonData, Formatting.Indented, new JsonSerializerSettings
                        {
                            TypeNameHandling = TypeNameHandling.All
                        });

                        File.WriteAllText("data.json", jsonContent);
                    }));
            }
        }

        #endregion

        #region Приватные методы

        /// <summary>
        /// Формируем узлы для TreeView из департаментов
        /// </summary>
        /// <param name="dep"></param>
        /// <returns></returns>
        private ObservableCollection<Node> GetTreeViewNodes(Department department = null)
        {
            ObservableCollection<Node> nodes = new ObservableCollection<Node>();

            if (department == null)
            {
                var rootDep = Department.Departments.Where(x => x.ParentId == 0).ToList();
                rootDep.ForEach(e =>
                {
                    var node = new Node(e.Id, e.Name);
                    node.Nodes = GetTreeViewNodes(e);
                    nodes.Add(node);
                });
                return nodes;
            }
            else
            {
                var subDep = Department.Departments.Where(x => x.ParentId == department.Id).ToList();
                subDep.ForEach(e =>
                {
                    var node = new Node(e.Id, e.Name);
                    node.Nodes = GetTreeViewNodes(e);
                    nodes.Add(node);
                });
                return nodes;
            }
        }

        /// <summary>
        /// Получает коллекцию содержащую узел который необходимо удалить
        /// </summary>
        /// <param name="nodes"></param>
        /// <param name="node"></param>
        /// <returns></returns>
        private ObservableCollection<Node> GetParentNode(ObservableCollection<Node> nodes, Node node)
        {
            ObservableCollection<Node> resultCollection = new ObservableCollection<Node>();

            if (nodes.Contains(node))
            {
                resultCollection = nodes;
            }
            else
            {
                foreach (var item in nodes)
                {
                    resultCollection = GetParentNode(item.Nodes, node);
                    if (resultCollection.Count != 0)
                    {
                        break;
                    }
                }
            }
            return resultCollection;
        }

        #endregion
    }
}
