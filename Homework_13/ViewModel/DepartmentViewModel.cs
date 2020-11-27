using Homework_13.Enum;
using Homework_13.Helper;
using Homework_13.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;

namespace Homework_13.ViewModel
{
    class DepartmentViewModel : BaseViewModel
    {
        private Department department;
        private bool isEdit;
        private string name;
        private ObservableCollection<Node> nodes;

        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                OnPropertyChanged();
            }
        }

        public DepartmentViewModel(ActionType action, ObservableCollection<Node> nodes, Department department)
        {
            this.nodes = nodes;

            switch (action)
            {
                case ActionType.EDIT:
                    this.department = department;
                    Name = department.Name;
                    this.isEdit = true;
                    break;

                case ActionType.CREATE:
                    if (department == null)
                    {
                        this.department = new Department() { Name = "" , ParentId = 0};
                        this.isEdit = false;
                    }
                    else 
                    {
                        this.department = new Department() { Name = "", ParentId = department.Id };
                        this.isEdit = false;
                    }
                    break;
                default:
                    break;
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
                            nodes.Add(new Node(department.Id, department.Name));
                        }


                        Window window = obj as Window;
                        window.Close();

                    },
                    obj => !string.IsNullOrEmpty(Name)));
            }
        }
    }
}
