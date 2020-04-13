using Homework_11.Model;
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

namespace Homework_11
{
    /// <summary>
    /// Interaction logic for Department.xaml
    /// </summary>
    public partial class DepartmentWindow : Window
    {
        private int parentId;
        public DepartmentWindow(int parentId)
        {
            this.parentId = parentId;
            InitializeComponent();

            btnSave.Click += BtnSave_Click;
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            string departmentName = tbDepartmentName.Text;
            Repository.AddDepartment(departmentName, parentId);
            this.Close();
        }
    }
}
