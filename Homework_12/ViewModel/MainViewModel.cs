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
                            ObservableCollection<Node> RootNodes = Nodes;
                            //departmentWindow = new AddDepartmentWindow(RootNodes, SelectedDepartment);
                            var selectdep = Department.Departments.Where(x => x.Id == SelectedNode.Id).FirstOrDefault();
                            Debug.WriteLine($"MainViewModel selectdep = {selectdep}");
                            departmentWindow = new AddDepartmentWindow(RootNodes, selectdep);
                        }
                        else
                        {
                            ObservableCollection<Node> ChildNodes = SelectedNode.Nodes;
                            var selectdep = Department.Departments.Where(x => x.Id == SelectedNode.Id).FirstOrDefault();
                            Debug.WriteLine($"MainViewModel selectdep = {selectdep}");
                            departmentWindow = new AddDepartmentWindow(ChildNodes, selectdep);
                        }

                        //departmentWindow.WindowStartupLocation = WindowStartupLocation.CenterOwner;
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
    }
}
