using Homework_12.Model;
using System.Windows;
using Homework_12.ViewModel;
using System.Collections.ObjectModel;

namespace Homework_12
{
    /// <summary>
    /// Interaction logic for Department.xaml
    /// </summary>
    public partial class AddDepartmentWindow : Window
    {
        public AddDepartmentWindow(ObservableCollection<Node> nodes, Department department)
        {
            InitializeComponent();
            DataContext = new DepartmentViewModel(nodes, department);
        }
    }
}
