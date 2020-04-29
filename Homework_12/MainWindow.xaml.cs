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
using Homework_12.ViewModel;

using System.Collections.ObjectModel;

namespace Homework_12
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        //private ObservableCollection<Employee> employees;
        //rivate ObservableCollection<Node> treeviewNodes;
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainViewModel();
            //Repository.LoadData();

            //employees = new ObservableCollection<Employee>();
            //treeviewNodes = GetTreeViewNodes();

            //lvEmployees.ItemsSource = employees;
            
            
            //treeViewDepartments.ItemsSource = treeviewNodes;

            //btnSaveToJson.Click += BtnSaveToJson_Click;
            //btnLoadFromJson.Click += BtnLoadFromJson_Click;

            //btnAddEmployee.Click += BtnAddEmployee_Click;
            //btnUpdateEmployee.Click += BtnUpdateEmployee_Click;
            //btnDeleteEmployee.Click += BtnDeleteEmployee_Click;

            //btnAddDepartment.Click += BtnAddDepartment_Click;
            //btnUpdateDepartment.Click += BtnUpdateDepartment_Click;
            //btnDeleteDepartment.Click += BtnDeleteDepartment_Click;

            //btnRefresh.Click += BtnRefresh_Click;


            //treeViewDepartments.SelectedItemChanged += TreeViewDepartments_SelectedItemChanged;
        }


        #region Sort Methods

        //public void Clik_Position_Sort(object sender, RoutedEventArgs e)
        //{
        //    var empls = employees.OrderBy(x => x.Position).ToList();
        //    employees.Clear();
        //    empls.ForEach(x => employees.Add(x));
        //}

        //public void Clik_Name_Sort(object sender, RoutedEventArgs e)
        //{
        //    var empls = employees.OrderBy(x => x.Name).ToList();
        //    employees.Clear();
        //    empls.ForEach(x => employees.Add(x));
        //}

        //public void Clik_Salary_Sort(object sender, RoutedEventArgs e)
        //{
        //    var empls = employees.OrderBy(x => x.Salary).ToList();
        //    employees.Clear();
        //    empls.ForEach(x => employees.Add(x));
        //}

        #endregion


        #region Employee Methods

        //private void BtnDeleteEmployee_Click(object sender, RoutedEventArgs e)
        //{

        //    if (lvEmployees.SelectedItem == null)
        //    {
        //        MessageBox.Show("Необходимо выбрать сотрудника");
        //    }
        //    else
        //    {
        //        if (MessageBox.Show("Удаление сотрудника.\nПодтвердите удаление.", "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
        //        {
        //            var empl = lvEmployees.SelectedItem as Employee;
        //            employees.Remove(empl); //Удаляет из отображения
        //            Employee.DeleteEmployee(empl); //Удаляет из хранилища
        //        }
        //    }
        //}

        //private void BtnUpdateEmployee_Click(object sender, RoutedEventArgs e)
        //{
        //    var empl = lvEmployees.SelectedItem as Employee;
        //    if (lvEmployees.SelectedItem == null)
        //    {
        //        MessageBox.Show("Необходимо выбрать сотрудника");
        //    }
        //    else
        //    {
        //        EmployeeWindow wnd = new EmployeeWindow(empl);
        //        wnd.Owner = this;
        //        wnd.WindowStartupLocation = WindowStartupLocation.CenterOwner;
        //        wnd.ShowDialog();
        //    }
        //}

        //private void BtnAddEmployee_Click(object sender, RoutedEventArgs e)
        //{
        //    if (treeViewDepartments.SelectedItem == null)
        //    {
        //        MessageBox.Show("Необходимо выбрать департамент");
        //    }
        //    else
        //    {
        //        int depId = (treeViewDepartments.SelectedItem as Node).Id;
        //        EmployeeWindow wnd = new EmployeeWindow(depId);
        //        wnd.Owner = this;
        //        wnd.WindowStartupLocation = WindowStartupLocation.CenterOwner;
        //        wnd.ShowDialog();
        //    }
        //}

        //private void BtnRefresh_Click(object sender, RoutedEventArgs e)
        //{
        //    if (treeViewDepartments.SelectedItem == null)
        //    {
        //        MessageBox.Show("Необходимо выбрать департамент");
        //    }
        //    else
        //    {
        //        int depId = (treeViewDepartments.SelectedItem as Node).Id;
        //        var empls = Employee.Employees.Where(x => x.DepartmentId == depId).ToList();
        //        employees.Clear();
        //        empls.ForEach(x => employees.Add(x));
        //    }
        //}

        #endregion

        //private void BtnLoadFromJson_Click(object sender, RoutedEventArgs e)
        //{
        //    Repository.LoadData();
        //    treeviewNodes = GetTreeViewNodes();
        //    treeViewDepartments.ItemsSource = treeviewNodes;
        //}

        //private void BtnSaveToJson_Click(object sender, RoutedEventArgs e)
        //{
        //    Repository.SaveData();
        //}

        #region Department Methods

        //private void BtnAddDepartment_Click(object sender, RoutedEventArgs e)
        //{
        //    if (treeViewDepartments.SelectedItem == null)
        //    {
        //        DepartmentWindow newDepWindow = new DepartmentWindow(0);
        //        newDepWindow.Owner = this;
        //        newDepWindow.WindowStartupLocation = WindowStartupLocation.CenterOwner;

        //        if (newDepWindow.ShowDialog() == true)
        //        {
        //            treeviewNodes.Add(new Node(newDepWindow.Department.Id, newDepWindow.Department.Name));
        //        }
        //    }
        //    else
        //    {
        //        Node selectedNode = (treeViewDepartments.SelectedItem as Node);

        //        DepartmentWindow newDepWindow = new DepartmentWindow(selectedNode.Id);
        //        newDepWindow.Owner = this;
        //        newDepWindow.WindowStartupLocation = WindowStartupLocation.CenterOwner;

        //        if (newDepWindow.ShowDialog() == true)
        //        {
        //            var node = new Node(newDepWindow.Department.Id, newDepWindow.Department.Name);
        //            selectedNode.Nodes.Add(node);
        //        }
        //    }
        //}

        //private void BtnUpdateDepartment_Click(object sender, RoutedEventArgs e)
        //{
        //    if (treeViewDepartments.SelectedItem == null)
        //    {
        //        MessageBox.Show("Необходимо выбрать департамент");
        //    }
        //    else
        //    {
        //        Node selectedNode = (treeViewDepartments.SelectedItem as Node);

        //        UpdateDepartmentWindow newDepWindow = new UpdateDepartmentWindow(selectedNode.Id);
        //        newDepWindow.Owner = this;
        //        newDepWindow.WindowStartupLocation = WindowStartupLocation.CenterOwner;

        //        if (newDepWindow.ShowDialog() == true)
        //        {
        //            selectedNode.Name = newDepWindow.DepartmentName;
        //        }
        //    }
        //}

        //private void BtnDeleteDepartment_Click(object sender, RoutedEventArgs e)
        //{
        //    if (treeViewDepartments.SelectedItem == null)
        //    {
        //        MessageBox.Show("Необходимо выбрать департамент");
        //    }
        //    else
        //    {

        //        if (MessageBox.Show("Удаление департамента приведет к удалению всех дочерних департаментов\n" +
        //            "и сотрудников в этих департаментах.\n" +
        //            "Подтвердите удаление.", "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
        //        {
        //            Node selectedNode = (treeViewDepartments.SelectedItem as Node);
        //            Department.DeleteDepartment(selectedNode.Id);

        //            var imetvdf = GetParentNode(treeviewNodes, selectedNode);
        //            imetvdf.Remove(selectedNode);
        //        }


        //    }
        //}

        #endregion


        #region TreeView

        ///// <summary>
        ///// Действие при изменении выбранного элемента в TreeView
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //private void TreeViewDepartments_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        //{
        //    if (e.NewValue is null)
        //    {
        //        return;
        //    }
        //    var n = (e.NewValue as Node).Id;
        //    var empls = Employee.Employees.Where(x => x.DepartmentId == n).ToList();
        //    employees.Clear();
        //    empls.ForEach(x => employees.Add(x));
        //}

        ///// <summary>
        ///// Формируем узлы для TreeView из департаментов
        ///// </summary>
        ///// <param name="dep"></param>
        ///// <returns></returns>
        //private ObservableCollection<Node> GetTreeViewNodes(Department dep = null)
        //{
        //    ObservableCollection<Node> nodes = new ObservableCollection<Node>();

        //    if (dep == null)
        //    {
        //        var rootDep = Department.Departments.Where(x => x.ParentId == 0).ToList();
        //        rootDep.ForEach(e =>
        //        {
        //            var node = new Node(e.Id, e.Name);
        //            node.Nodes = GetTreeViewNodes(e);
        //            nodes.Add(node);
        //        });
        //        return nodes;
        //    }
        //    else
        //    {
        //        var subDep = Department.Departments.Where(x => x.ParentId == dep.Id).ToList();
        //        subDep.ForEach(e =>
        //        {
        //            var node = new Node(e.Id, e.Name);
        //            node.Nodes = GetTreeViewNodes(e);
        //            nodes.Add(node);
        //        });
        //        return nodes;
        //    }
        //}

        ///// <summary>
        ///// Получает коллекцию содержащую узел который необходимо удалить
        ///// </summary>
        ///// <param name="nodes"></param>
        ///// <param name="node"></param>
        ///// <returns></returns>
        //private ObservableCollection<Node> GetParentNode(ObservableCollection<Node> nodes, Node node)
        //{
        //    ObservableCollection<Node> resultCollection = new ObservableCollection<Node>();

        //    if (nodes.Contains(node))
        //    {
        //        resultCollection = nodes;
        //    }
        //    else
        //    {
        //        foreach (var item in nodes)
        //        {
        //            resultCollection = GetParentNode(item.Nodes, node);
        //            if (resultCollection.Count != 0)
        //            {
        //                break;
        //            }
        //        }
        //    }
        //    return resultCollection;
        //}

        #endregion

    }
}
