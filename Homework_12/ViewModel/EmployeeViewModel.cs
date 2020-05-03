using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Homework_12.Model;
using System.Windows;

namespace Homework_12.ViewModel
{
    class EmployeeViewModel : INotifyPropertyChanged
    {
        private Employee employee;
        private Department department;
        private bool isFieldNotEmpty;

        private Visibility salaryFieldVisibility;
        public Visibility SalaryFieldVisibility
        {
            get { return salaryFieldVisibility; }
            set
            {
                salaryFieldVisibility = value;
                OnPropertyChanged("SalaryFieldVisibility");
            }
        }

        private string employeeName;
        public string EmployeeName
        {
            get { return employeeName; }
            set
            {
                employeeName = value;
                isFieldNotEmpty = !string.IsNullOrEmpty(EmployeeSalary) && !string.IsNullOrEmpty(EmployeeName) && !string.IsNullOrEmpty(SelectedPosition);
                OnPropertyChanged("EmployeeName");
            }
        }

        private string employeeSalary;
        public string EmployeeSalary
        {
            get { return employeeSalary; }
            set
            {
                employeeSalary = value;
                isFieldNotEmpty = !string.IsNullOrEmpty(EmployeeSalary) && !string.IsNullOrEmpty(EmployeeName) && !string.IsNullOrEmpty(SelectedPosition);
                OnPropertyChanged("EmployeeSalary");
            }
        }

        private string selectedPosition;
        public string SelectedPosition
        {
            get { return selectedPosition; }
            set
            {
                selectedPosition = value;
                if(SelectedPosition == "Начальник")
                {
                    SalaryFieldVisibility = Visibility.Collapsed;
                    //EmployeeSalary = "0";
                    isFieldNotEmpty = !string.IsNullOrEmpty(EmployeeName) && !string.IsNullOrEmpty(SelectedPosition);
                }
                else
                {
                    SalaryFieldVisibility = Visibility.Visible;
                    isFieldNotEmpty = !string.IsNullOrEmpty(EmployeeSalary) && !string.IsNullOrEmpty(EmployeeName) && !string.IsNullOrEmpty(SelectedPosition);
                }           
                OnPropertyChanged("SelectedPosition");
            }
        }


        private ObservableCollection<string> positions;
        public ObservableCollection<string> Positions
        {
            get { return positions; }
            set
            {
                positions = value;
                OnPropertyChanged("Positions");
            }
        }
     
        private RelayCommand saveEmployee;
        public RelayCommand SaveEmployee
        {
            get
            {
                return saveEmployee ??
                    (saveEmployee = new RelayCommand(obj =>
                    {
                        if(employee == null)
                        {
                            decimal salary = 0;
                            if (!SelectedPosition.Equals("Начальник"))
                            {
                                salary = decimal.Parse(EmployeeSalary);
                            }

                            switch (SelectedPosition)
                            {
                                case "Начальник":
                                    Manager manager = new Manager(EmployeeName, department.Id, SelectedPosition);
                                    break;
                                case "Рабочий":
                                    Worker worker = new Worker(EmployeeName, department.Id, SelectedPosition, salary);
                                    break;
                                case "Стажер":
                                    Intertn intertn = new Intertn(EmployeeName, department.Id, SelectedPosition, salary);
                                    break;
                                default:
                                    break;
                            }                           
                        }
                        else
                        {
                            Employee.DeleteEmployee(employee);

                            decimal salary = 0;
                            if (!SelectedPosition.Equals("Начальник"))
                            {
                                salary = decimal.Parse(EmployeeSalary);
                            }

                            switch (SelectedPosition)
                            {
                                case "Начальник":
                                    Manager manager = new Manager(EmployeeName, department.Id, SelectedPosition);
                                    manager.Id = employee.Id;
                                    break;
                                case "Рабочий":
                                    Worker worker = new Worker(EmployeeName, department.Id, SelectedPosition, salary);
                                    worker.Id = employee.Id;
                                    break;
                                case "Стажер":
                                    Intertn intertn = new Intertn(EmployeeName, department.Id, SelectedPosition, salary);
                                    intertn.Id = employee.Id;
                                    break;
                                default:
                                    break;
                            }
                        }


                        Window wnd = obj as Window;
                        wnd.Close();
                    }, obj => isFieldNotEmpty));
            }
        }

        public EmployeeViewModel(Department department, Employee employee)
        {
            this.department = department;
            this.employee = employee;

            Positions = new ObservableCollection<string>()
            {
                "Начальник", "Рабочий", "Стажер"
            };

            if (employee != null)
            {
                EmployeeName = employee.Name;
                SelectedPosition = employee.Position;
                EmployeeSalary = employee.Salary.ToString();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}



