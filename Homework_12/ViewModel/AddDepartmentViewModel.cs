using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Homework_12.Model;
using System.Windows;

namespace Homework_12.ViewModel
{
    class AddDepartmentViewModel : INotifyPropertyChanged
    {

        private Department department;

        private ObservableCollection<Node> nodes;

        private string departmentName;
        public string DepartmentName
        {
            get { return departmentName; }
            set
            {
                departmentName = value;
                OnPropertyChanged("DepartmentName");
            }
        }

        private RelayCommand addDepartment;
        public RelayCommand AddDepartment
        {
            get
            {
                return addDepartment ??
                    (addDepartment = new RelayCommand(obj =>
                    {
                        Department newDepartment = new Department(DepartmentName, department.Id);

                        nodes.Add(new Node(newDepartment.Id, newDepartment.Name));

                        Window window = obj as Window;
                        window.Close();

                    }, obj => !string.IsNullOrEmpty(DepartmentName)));
            }
        }

        public AddDepartmentViewModel(ObservableCollection<Node> nodes, Department department)
        {
            this.nodes = nodes;
            this.department = department;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
