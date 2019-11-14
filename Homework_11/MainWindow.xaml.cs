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
using Homework_11.Model;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace Homework_11
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Organization organization;
        public MainWindow()
        {
            InitializeComponent();
            organization = new Organization();

            treeViewDepartments.ItemsSource = organization.Departments;

            treeViewDepartments.SelectedItemChanged += Departments_SelectedItemChanged;
        }

        private void Departments_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            Department dep = treeViewDepartments.SelectedItem as Department;
            employeeList.ItemsSource = dep.Employees;

            Manager manager = dep.Manager;
            managerName.Text = manager.Name;
            managerSalary.Text = $" Зарплата: {manager.Salary.ToString()}";
            Debug.WriteLine("Рассчитали и показали ЗП для начальника");
        }
    }
}
