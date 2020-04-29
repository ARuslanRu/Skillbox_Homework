using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Homework_12.Model;
using System.Windows;

namespace Homework_12.ViewModel
{
    class UpdateDepartmentViewModel : INotifyPropertyChanged
    {
        private Node node;
        private Department department;


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

        private RelayCommand updateDepartment;
        public RelayCommand UpdateDepartment
        {
            get
            {
                return updateDepartment ??
                    (updateDepartment = new RelayCommand(obj =>
                    {
                        this.department.Name = DepartmentName;
                        this.node.Name = DepartmentName;

                        Window window = obj as Window;
                        window.Close();

                    }, obj => !string.IsNullOrEmpty(DepartmentName)));

            }
        }




        public UpdateDepartmentViewModel(Node node, Department department)
        {
            this.node = node;
            this.department = department;
            DepartmentName = department.Name;
        }


        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
