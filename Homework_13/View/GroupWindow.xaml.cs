using Homework_13.Model;
using Homework_13.ViewModel;
using System.Windows;

namespace Homework_13.View
{
    /// <summary>
    /// Interaction logic for GroupWindow.xaml
    /// </summary>
    public partial class GroupWindow : Window
    {
        public GroupWindow(Group group = null)
        {
            InitializeComponent();
            DataContext = new GroupViewModel(group);
        }
    }
}
