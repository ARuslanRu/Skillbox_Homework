using Homework_13.Enum;
using Homework_13.Model;
using Homework_13.ViewModel;
using System.Collections.ObjectModel;
using System.Windows;

namespace Homework_13.View
{
    /// <summary>
    /// Interaction logic for DepartmentWindow.xaml
    /// </summary>
    public partial class DepartmentWindow : Window
    {
        public DepartmentWindow(ActionType action, ObservableCollection<Node> nodes, Department department = null)
        {
            InitializeComponent();
            DataContext = new DepartmentViewModel(action, nodes, department);
        }
    }
}
