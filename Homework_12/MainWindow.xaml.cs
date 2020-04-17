using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Homework_12.Model;

using System.Collections.ObjectModel;

namespace Homework_12
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private ObservableCollection<Employee> employees;
        private ObservableCollection<Node> treeviewNodes;
        public MainWindow()
        {
            InitializeComponent();
            Repository.LoadData();

            employees = new ObservableCollection<Employee>();
            treeviewNodes = GetTreeViewNodes();

            lvEmployees.ItemsSource = employees;
            treeViewDepartments.ItemsSource = treeviewNodes;

            btnSaveToJson.Click += BtnSaveToJson_Click;
            btnLoadFromJson.Click += BtnLoadFromJson_Click;

            btnAddEmployee.Click += BtnAddEmployee_Click;

            btnAddDepartment.Click += BtnAddDepartment_Click;
            btnUpdateDepartment.Click += BtnUpdateDepartment_Click;
            btnDeleteDepartment.Click += BtnDeleteDepartment_Click;

            treeViewDepartments.SelectedItemChanged += TreeViewDepartments_SelectedItemChanged;
        }

        private void BtnDeleteDepartment_Click(object sender, RoutedEventArgs e)
        {
            if (treeViewDepartments.SelectedItem == null)
            {
                MessageBox.Show("Необходимо выбрать департамент");
            }
            else
            {
                Node selectedNode = (treeViewDepartments.SelectedItem as Node);
                Department.DeleteDepartment(selectedNode.Id);
                //treeviewNodes.Remove(selectedNode);//так можем удалить узел из корня

                var imetvdf = GetParentNode(treeviewNodes, selectedNode);
                imetvdf.Remove(selectedNode);

                //Пооже надо как-то получить родительский узел у выбранного и удалить из него выбранный
                //Будем получать родительский узел
            }
        }


        private ObservableCollection<Node> GetParentNode(ObservableCollection<Node> nodes, Node node)
        {
            ObservableCollection<Node> resultCollection = new ObservableCollection<Node>();

            //if (nodes.Contains(node))
            //{
            //    return nodes;
            //}

            //foreach (var item in nodes)
            //{
            //    resultCollection = GetParentNode(item.Nodes, node);
            //    if (resultCollection.Count != 0)
            //    {
            //        break;
            //    }
            //}


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

        private void BtnUpdateDepartment_Click(object sender, RoutedEventArgs e)
        {
            if (treeViewDepartments.SelectedItem == null)
            {
                MessageBox.Show("Необходимо выбрать департамент");
            }
            else
            {
                //TODO: Хммм
                // Пока что идея создать новую форму, в которой будет редактироваться департамент
                // В идеале использование одной формы.
                Node selectedNode = (treeViewDepartments.SelectedItem as Node);
                UpdateDepartmentWindow newDepWindow = new UpdateDepartmentWindow(selectedNode.Id);
                newDepWindow.ShowDialog();
                selectedNode.Name = newDepWindow.DepartmentName;
            }
        }

        private void BtnAddEmployee_Click(object sender, RoutedEventArgs e)
        {
            if (treeViewDepartments.SelectedItem == null)
            {
                MessageBox.Show("Необходимо выбрать департамент");
            }
            else
            {
                int depId = (treeViewDepartments.SelectedItem as Node).Id;
                new EmployeeWindow(depId).Show();

            }
        }

        private void BtnAddDepartment_Click(object sender, RoutedEventArgs e)
        {
            if (treeViewDepartments.SelectedItem == null)
            {
                DepartmentWindow newDepWindow = new DepartmentWindow(0);
                newDepWindow.ShowDialog();
                if (newDepWindow.Department != null) //Если у окна добавления департамента не создалось департамета, тогда пропускаем.
                {
                    treeviewNodes.Add(new Node(newDepWindow.Department.Id, newDepWindow.Department.Name));
                }
            }
            else
            {
                Node selectedNode = (treeViewDepartments.SelectedItem as Node);
                DepartmentWindow newDepWindow = new DepartmentWindow(selectedNode.Id);
                newDepWindow.ShowDialog();

                if (newDepWindow.Department != null) //Если у окна добавления департамента не создалось департамета, тогда пропускаем.
                {
                    var node = new Node(newDepWindow.Department.Id, newDepWindow.Department.Name);
                    selectedNode.Nodes.Add(node);
                }
            }
        }

        private void BtnLoadFromJson_Click(object sender, RoutedEventArgs e)
        {
            Repository.LoadData();
            treeviewNodes = GetTreeViewNodes();
            treeViewDepartments.ItemsSource = treeviewNodes;
        }

        private void BtnSaveToJson_Click(object sender, RoutedEventArgs e)
        {
            Repository.SaveData();
        }

        #region TreeView

        /// <summary>
        /// Действие при изменении выбранного элемента в TreeView
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TreeViewDepartments_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            if (e.NewValue is null)
            {
                return;
            }
            var n = (e.NewValue as Node).Id;
            var empls = Employee.Employees.Where(x => x.DepartmentId == n).ToList();
            employees.Clear();
            empls.ForEach(x => employees.Add(x));
        }


        /// <summary>
        /// Формируем узлы для TreeView из департаментов
        /// </summary>
        /// <param name="dep"></param>
        /// <returns></returns>
        private ObservableCollection<Node> GetTreeViewNodes(Department dep = null)
        {
            ObservableCollection<Node> nodes = new ObservableCollection<Node>();

            if (dep == null)
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
                var subDep = Department.Departments.Where(x => x.ParentId == dep.Id).ToList();
                subDep.ForEach(e =>
                {
                    var node = new Node(e.Id, e.Name);
                    node.Nodes = GetTreeViewNodes(e);
                    nodes.Add(node);

                });
                return nodes;
            }
        }

        #endregion
    }
}
