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
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainViewModel();
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


    }
}
