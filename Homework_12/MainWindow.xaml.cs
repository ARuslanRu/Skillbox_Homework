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
            treeviewNodes = new ObservableCollection<Node>();


            //LoadTreeViewItems(treeViewDepartments);

            lvEmployees.ItemsSource = employees;

            treeViewDepartments.ItemsSource = GetTreeViewNodes();

            btnSaveToJson.Click += BtnSaveToJson_Click;
            btnLoadFromJson.Click += BtnLoadFromJson_Click;
            btnAddDepartment.Click += BtnAddDepartment_Click;
            btnAddEmployee.Click += BtnAddEmployee_Click;

            treeViewDepartments.SelectedItemChanged += TreeViewDepartments_SelectedItemChanged;
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
                DepartmentWindow newDep = new DepartmentWindow(0);
                newDep.ShowDialog();
                var root = treeViewDepartments.ItemsSource as ObservableCollection<Node>;
                root.Add(new Node(newDep.Department.Id, newDep.Department.Name));
            }
            else
            {
                int parentId = (treeViewDepartments.SelectedItem as Node).Id;
                DepartmentWindow newDepWindow = new DepartmentWindow(parentId);
                newDepWindow.ShowDialog();
                var node = new Node(newDepWindow.Department.Id, newDepWindow.Department.Name);
                (treeViewDepartments.SelectedItem as Node).Nodes.Add(node);
            }
        }

        private void BtnLoadFromJson_Click(object sender, RoutedEventArgs e)
        {
            Repository.LoadData();
            treeViewDepartments.ItemsSource = GetTreeViewNodes();
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
