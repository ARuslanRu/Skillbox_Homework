using Homework_13.Helper;
using Homework_13.Model;
using System.Collections.Generic;
using System.Windows;
using System.Linq;

namespace Homework_13.ViewModel
{
    class ClientViewModel : BaseViewModel
    {
        private Client client;
        private bool isEdit;
        private string name;

        private Department selectedDepartment;
        private IEnumerable<Department> departments = Repository.Departments;

        public ClientViewModel()
        {
            this.client = new Client() { Name = "" };
            this.isEdit = false;
        }

        public ClientViewModel(Client client)
        {
            this.client = client;
            Name = client.Name;
            SelectedDepartment = Repository.Departments.Where(x => x.Id == client.DepartmentId).FirstOrDefault();
            this.isEdit = true;
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
                       if (!isEdit)
                       {
                           Repository.AddClient(client);
                       }


                       Window window = obj as Window;
                       window.Close();

                   },
                   obj => !string.IsNullOrEmpty(Name) && SelectedDepartment != null));
            }
        }
    }
}
