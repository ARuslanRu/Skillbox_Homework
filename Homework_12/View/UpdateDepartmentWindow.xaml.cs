using System.Windows;
using Homework_12.ViewModel;
using Homework_12.Model;

namespace Homework_12
{
    /// <summary>
    /// Interaction logic for Department.xaml
    /// </summary>
    public partial class UpdateDepartmentWindow : Window
    {
        public UpdateDepartmentWindow(Node node, Department department)
        {
            InitializeComponent();
            DataContext = new UpdateDepartmentViewModel(node, department);
        }
    }
}
