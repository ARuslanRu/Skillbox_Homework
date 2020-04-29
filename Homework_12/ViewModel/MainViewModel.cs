using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Homework_12.Model;
using System.Windows;

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
                            departmentWindow = new AddDepartmentWindow(RootNodes, SelectedDepartment);
                        }
                        else
                        {
                            ObservableCollection<Node> ChildNodes = SelectedNode.Nodes;
                            departmentWindow = new AddDepartmentWindow(ChildNodes, SelectedDepartment);
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

        public MainViewModel()
        {
            new Department("Департмент_1", 0);
            new Department("Департмент_2", 0);
            new Department("Департмент_3", 0);
            new Department("Департмент_4", 1);
            new Department("Департмент_5", 1);
            new Department("Департмент_6", 4);

            new Manager("Имя_1", 1, "Начальник");
            new Worker("Имя_4", 1, "Рабочий", 100);
            new Intertn("Имя_5", 1, "Стажер", 100);
            new Worker("Имя_2", 2, "Рабочий", 100);
            new Intertn("Имя_3", 3, "Стажер", 100);

            Nodes = GetTreeViewNodes();
        }


        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
