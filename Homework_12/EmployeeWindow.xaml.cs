using Homework_12.Model;
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
using System.Windows.Shapes;

namespace Homework_12
{
    /// <summary>
    /// Interaction logic for EmployeeWindow.xaml
    /// </summary>
    public partial class EmployeeWindow : Window
    {
        private int departmentId;
        public EmployeeWindow(int departmentId)
        {
            this.departmentId = departmentId;
            InitializeComponent();
            cbEmployeePosition.SelectionChanged += CbEmployeePosition_SelectionChanged;

            btnSave.Click += BtnSave_Click;
            btnCancel.Click += BtnCancel_Click;
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void CbEmployeePosition_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            TextBlock selectedItem = (TextBlock)comboBox.SelectedItem;

            if (selectedItem.Text.Equals("Начальник"))
            {
                tblSalary.Visibility = Visibility.Hidden;
                tbSalary.Visibility = Visibility.Hidden;
            }
            else
            {
                tblSalary.Visibility = Visibility.Visible;
                tbSalary.Visibility = Visibility.Visible;
            }
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            string name = tbName.Text;
            TextBlock selectedItem = (TextBlock)cbEmployeePosition.SelectedItem;

            decimal salary = 0;
            if (!selectedItem.Text.Equals("Начальник"))
            {
                salary = decimal.Parse(tbSalary.Text);
            }

            Repository.AddEmployee(name, departmentId, selectedItem.Text, salary);
            this.Close();
        }
    }
}
