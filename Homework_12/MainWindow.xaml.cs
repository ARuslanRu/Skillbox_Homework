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

        ObservableCollection<Employee> employees = new ObservableCollection<Employee>();
        public MainWindow()
        {
            InitializeComponent();
            Repository.LoadData();

            LoadTreeViewItems(treeViewDepartments);

            btnSaveToJson.Click += BtnSaveToJson_Click;
            btnLoadFromJson.Click += BtnLoadFromJson_Click;
            btnAddDepartment.Click += BtnAddDepartment_Click;
            btnRefresh.Click += BtnRefresh_Click;
            btnAddEmployee.Click += BtnAddEmployee_Click;

            lvEmployees.ItemsSource = employees;

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
                int depId = int.Parse((treeViewDepartments.SelectedItem as TreeViewItem).Tag.ToString());
                new EmployeeWindow(depId).Show();

            }
        }

        private void BtnRefresh_Click(object sender, RoutedEventArgs e)
        {
            LoadTreeViewItems(treeViewDepartments);
        }

        private void BtnAddDepartment_Click(object sender, RoutedEventArgs e)
        {
            if (treeViewDepartments.SelectedItem == null)
            {
                new DepartmentWindow(0).Show();
            }
            else
            {
                var parentDep = treeViewDepartments.SelectedItem as TreeViewItem;
                int parentId = int.Parse(parentDep.Tag.ToString());
                new DepartmentWindow(parentId).Show();
            }
        }

        private void BtnLoadFromJson_Click(object sender, RoutedEventArgs e)
        {
            Repository.LoadData();
            LoadTreeViewItems(treeViewDepartments);
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
            var n = int.Parse((e.NewValue as TreeViewItem).Tag.ToString(), null);
            var empls = Repository.EmployeesDb.Where(x => x.DepartmentId == n).ToList();
            employees.Clear();
            empls.ForEach(x => employees.Add(x));
        }

        /// <summary>
        /// Загрузка элементов для TreeView
        /// </summary>
        /// <param name="treeView"></param>
        private void LoadTreeViewItems(TreeView treeView)
        {
            treeView.Items.Clear();
            var rootDep = Repository.DepartmentsDb.Where(x => x.ParentId == 0).ToList();

            rootDep.ForEach(dep =>
            {
                var item = new TreeViewItem()
                {
                    Header = dep.Name,
                    Tag = dep.Id
                };

                item.Items.Add(null);

                item.Expanded += Item_Expanded;

                treeView.Items.Add(item);
            });
        }

        /// <summary>
        /// Действие при раскрытии элемента в TreeView
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Item_Expanded(object sender, RoutedEventArgs e)
        {
            var item = (TreeViewItem)sender;

            if (item.Items.Count != 1 || item.Items[0] != null)
            {
                return;
            }

            item.Items.Clear();

            var depId = int.Parse(item.Tag.ToString(), null);

            #region Получение департаментов

            var depsIn = Repository.DepartmentsDb.Where(x => x.ParentId == depId).ToList();

            depsIn.ForEach(dep =>
            {
                var subItem = new TreeViewItem()
                {
                    Header = dep.Name,
                    Tag = dep.Id
                };


                subItem.Items.Add(null);

                subItem.Expanded += Item_Expanded;

                item.Items.Add(subItem);

            });

            #endregion

        }

        #endregion
    }
}
