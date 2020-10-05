using Homework_13.Helper;
using Homework_13.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace Homework_13.ViewModel
{
    class DepartmentViewModel : BaseViewModel
    {
        private Department department;
        private bool isEdit;
        private string name;

        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                OnPropertyChanged("Name");
            }
        }

        public DepartmentViewModel(Department department)
        {
            if (department == null)
            {
                this.department = new Department() { Name = "" };         
                this.isEdit = false;
            }
            else
            {

                this.department = department;
                Name = department.Name;
                this.isEdit = true;
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
                        department.Name = Name;
                        if (!isEdit)
                        {
                            Repository.AddDepartment(department);
                        }


                        Window window = obj as Window;
                        window.Close();

                    },
                    obj => !string.IsNullOrEmpty(Name)));
            }
        }
    }
}
