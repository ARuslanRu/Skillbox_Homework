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
    /// Interaction logic for Department.xaml
    /// </summary>
    public partial class UpdateDepartmentWindow : Window
    {
        private int id;

        public string DepartmentName { get; private set; }

        public UpdateDepartmentWindow(int id)
        {
            this.id = id;
            InitializeComponent();
            DepartmentName = Department.Departments.Where(x => x.Id == id).FirstOrDefault().Name;
            tbDepartmentName.Text = DepartmentName;

            btnSave.Click += BtnSave_Click;
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
            DepartmentName = tbDepartmentName.Text;
            Department.UpdateDepartment(id, DepartmentName);
        }
    }
}
