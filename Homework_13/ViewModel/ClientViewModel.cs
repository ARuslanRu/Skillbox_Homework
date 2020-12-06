using Homework_13.Helper;
using Homework_13.Model;
using System.Collections.Generic;
using System.Windows;
using System.Linq;
using Homework_13.Services;

namespace Homework_13.ViewModel
{
    class ClientViewModel : BaseViewModel
    {
        private Client client;
        private string name;

        private Department selectedDepartment;
        private IEnumerable<Department> departments = DepartmentService.GetAllDepartments();

        public ClientViewModel(Client client)
        {
            this.client = client;
            Name = client.Name;
            SelectedDepartment = DepartmentService.SelectDepartment(client.DepartmentId);
        }

        public string Name
        {
            get { return name; }
            set
            {
                this.name = value;
                OnPropertyChanged();
            }
        }
        public Department SelectedDepartment
        {
            get { return selectedDepartment; }
            set
            {
                this.selectedDepartment = value;
                OnPropertyChanged();
            }
        }
        public IEnumerable<Department> Departments
        {
            get { return departments; }
            set
            {
                this.departments = value;
                OnPropertyChanged();
            }
        }
        private RelayCommand saveCommand;
        public RelayCommand SaveCommand
        {
            get
            {
                return saveCommand ??
                   (saveCommand = new RelayCommand(obj =>
                   {
                       client.Name = Name;
                       client.DepartmentId = SelectedDepartment.Id;

                       Window window = obj as Window;
                       window.DialogResult = true;
                       window.Close();

                   },
                   obj => !string.IsNullOrEmpty(Name) && SelectedDepartment != null));
            }
        }
    }
}
