using Homework_12.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Homework_12.ViewModel;

namespace Homework_12
{
    /// <summary>
    /// Interaction logic for EmployeeWindow.xaml
    /// </summary>
    public partial class EmployeeWindow : Window
    {
        //private int departmentId;
        //private bool isUpdate;
        //private Employee _employee;
        public EmployeeWindow(Department department, Employee employee = null)
        {
            //this.departmentId = departmentId;
            //this.isUpdate = false;
            InitializeComponent();
            DataContext = new EmployeeViewModel(department, employee);
            //cbEmployeePosition.SelectionChanged += CbEmployeePosition_SelectionChanged;

            //btnSave.Click += BtnSave_Click;
            //btnCancel.Click += BtnCancel_Click;

            //cbEmployeePosition.ItemsSource = new List<String> { "Начальник", "Рабочий", "Стажер" };

        }

        //public EmployeeWindow(Employee employee)
        //{

        //    this.isUpdate = true;
        //    this._employee = employee;
        //    this.departmentId = employee.DepartmentId;

        //    InitializeComponent();

        //    tbName.Text = employee.Name;
        //    cbEmployeePosition.SelectedItem = employee.Position;
        //    cbEmployeePosition.ItemsSource = new List<String> { "Начальник", "Рабочий", "Стажер" };
        //    tbSalary.Text = employee.Salary.ToString();

        //    cbEmployeePosition.SelectionChanged += CbEmployeePosition_SelectionChanged;

        //    btnSave.Click += BtnSave_Click;
        //    btnCancel.Click += BtnCancel_Click;


        //    if (employee.Position.Equals("Начальник"))
        //    {
        //        tblSalary.Visibility = Visibility.Hidden;
        //        tbSalary.Visibility = Visibility.Hidden;
        //    }
        //}


        //private void BtnCancel_Click(object sender, RoutedEventArgs e)
        //{
        //    this.Close();
        //}

        //private void CbEmployeePosition_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    ComboBox comboBox = (ComboBox)sender;
        //    var selectedItem = comboBox.SelectedItem.ToString();

        //    if (selectedItem.Equals("Начальник"))
        //    {
        //        tblSalary.Visibility = Visibility.Hidden;
        //        tbSalary.Visibility = Visibility.Hidden;
        //    }
        //    else
        //    {
        //        tblSalary.Visibility = Visibility.Visible;
        //        tbSalary.Visibility = Visibility.Visible;
        //    }
        //}

        //private void BtnSave_Click(object sender, RoutedEventArgs e)
        //{

        //    if(isUpdate)
        //    {
        //        Employee.DeleteEmployee(_employee);


        //        string name = tbName.Text;

        //        var selectedItem = cbEmployeePosition.SelectedItem.ToString();

        //        decimal salary = 0;
        //        if (!selectedItem.Equals("Начальник"))
        //        {
        //            salary = decimal.Parse(tbSalary.Text);
        //        }

        //        switch (selectedItem)
        //        {
        //            case "Начальник":
        //                Manager manager = new Manager(name, departmentId, selectedItem);
        //                manager.Id = _employee.Id;
        //                break;
        //            case "Рабочий":
        //                Worker worker = new Worker(name, departmentId, selectedItem, salary);
        //                worker.Id = _employee.Id;
        //                break;
        //            case "Стажер":
        //                Intertn intertn = new Intertn(name, departmentId, selectedItem, salary);
        //                intertn.Id = _employee.Id;
        //                break;
        //            default:
        //                break;
        //        }


        //    }
        //    else
        //    {
        //        string name = tbName.Text;

        //        var selectedItem = cbEmployeePosition.SelectedItem.ToString();

        //        decimal salary = 0;
        //        if (!selectedItem.Equals("Начальник"))
        //        {
        //            salary = decimal.Parse(tbSalary.Text);
        //        }

        //        switch (selectedItem)
        //        {
        //            case "Начальник":
        //                new Manager(name, departmentId, selectedItem);
        //                break;
        //            case "Рабочий":
        //                new Worker(name, departmentId, selectedItem, salary);
        //                break;
        //            case "Стажер":
        //                new Intertn(name, departmentId, selectedItem, salary);
        //                break;
        //            default:
        //                break;
        //        }
        //    }


        //    this.Close();
        //}
    }
}
