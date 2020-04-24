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
    public partial class DepartmentWindow : Window
    {
        private int parentId;

        public Department Department { get; private set; }

        public DepartmentWindow(int parentId)
        {
            this.parentId = parentId;
            InitializeComponent();

            btnSave.Click += BtnSave_Click;
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
            string departmentName = tbDepartmentName.Text;
            if (string.IsNullOrEmpty(departmentName))
            {
                MessageBox.Show("Не указано название департамента");
                return;
            }
            Department = new Department(departmentName, parentId);
        }
    }
}
