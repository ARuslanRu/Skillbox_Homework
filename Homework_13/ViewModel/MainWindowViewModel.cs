using Homework_13.Enum;
using Homework_13.Exception;
using Homework_13.Helper;
using Homework_13.Model;
using Homework_13.Model.Event;
using Homework_13.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Homework_13.Extensions;
using Homework_13.Services;

namespace Homework_13.ViewModel
{
    class MainWindowViewModel : BaseViewModel
    {
        #region fields
        private Department selectedDepartment;
        private ObservableCollection<Department> departments;
        private Client selectedClient;
        private IEnumerable<Client> clientsInDepartment;
        private Account account;
        private IEnumerable<IDeposit> deposites;
        private Node selectedNode;
        private ObservableCollection<Node> nodes;
        #endregion

        #region properties
        public Department SelectedDepartment
        {
            get { return selectedDepartment; }
            set
            {
                selectedDepartment = value;
                ClientsInDepartment = Repository.Clients.Where(x => x.DepartmentId == selectedDepartment.Id);
                OnPropertyChanged();
            }
        }
        public ObservableCollection<Department> Departments
        {
            get { return departments; }
            set
            {
                departments = value;
                OnPropertyChanged();
            }
        }
        public Client SelectedClient
        {
            get { return selectedClient; }
            set
            {
                selectedClient = value;
                Account = Repository.Accounts.Where(x => x.ClientId == (selectedClient?.Id ?? 0)).FirstOrDefault();
                if (Account == null)
                {
                    throw new СlientHasNoAccountException();
                }
                Deposites = Repository.Deposites.Where(x => x.ClientId == (selectedClient?.Id ?? 0));
                OnPropertyChanged();
            }
        }
        public IEnumerable<Client> ClientsInDepartment
        {
            get { return clientsInDepartment; }
            set
            {
                clientsInDepartment = value;
                OnPropertyChanged();
            }
        }
        public Account Account
        {
            get { return account; }
            set
            {
                account = value;
                OnPropertyChanged();
            }
        }
        public IEnumerable<IDeposit> Deposites
        {
            get { return deposites; }
            set
            {
                deposites = value;
                OnPropertyChanged();
            }
        }
        public Node SelectedNode
        {
            get { return selectedNode; }
            set
            {
                selectedNode = value;
                SelectedDepartment = Departments.Where(x => x.Id == SelectedNode.Id).FirstOrDefault();
                ClientsInDepartment = Repository.Clients.Where(x => x.DepartmentId == SelectedNode.Id).ToList();
                OnPropertyChanged();
            }
        }
        public ObservableCollection<Node> Nodes
        {
            get { return nodes; }
            set
            {
                nodes = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region constructor
        public MainWindowViewModel()
        {
            Departments = DepartmentService.GetAllDepartments();

            Repository.GetInstance();

            Nodes = GetTreeViewNodes();
            Account.Notify += Account_Notify;
            Account.Notify += Logging;
        }
        #endregion

        #region commands
        private RelayCommand addDepartment;
        private RelayCommand editDepartment;
        private RelayCommand removeDepartment;
        private RelayCommand addClient;
        private RelayCommand editClient;
        private RelayCommand removeClient;
        private RelayCommand addDeposit;
        private RelayCommand addChildDepartment;
        private RelayCommand sendTo;

        public RelayCommand AddDepartment
        {
            get
            {
                return addDepartment ??
                    (addDepartment = new RelayCommand(obj =>
                    {
                        if (Nodes == null)
                        {
                            Nodes = new ObservableCollection<Node>();
                        }

                        Department newDepartment = new Department();
                        DepartmentWindow departmentWindow = new DepartmentWindow()
                        {
                            DataContext = new DepartmentViewModel(newDepartment)
                        };

                        departmentWindow.Owner = obj as Window;
                        departmentWindow.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                        departmentWindow.ShowDialog();

                        if (departmentWindow.DialogResult.Value)
                        {
                            DepartmentService.InsertDepartment(newDepartment);
                            Departments.Add(newDepartment);
                            Nodes.Add(new Node(newDepartment.Id, newDepartment.Name));
                        }
                    }));
            }
        }
        public RelayCommand EditDepartment
        {
            get
            {
                return editDepartment ??
                    (editDepartment = new RelayCommand(obj =>
                    {
                        Department updatedDepartment = SelectedDepartment;
                        DepartmentWindow departmentWindow = new DepartmentWindow()
                        {
                            DataContext = new DepartmentViewModel(updatedDepartment)
                        };

                        departmentWindow.Owner = obj as Window;
                        departmentWindow.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                        departmentWindow.ShowDialog();
                        Debug.WriteLine($"Результат закрытия диалогового окна: {departmentWindow.DialogResult}");

                        if (departmentWindow.DialogResult.Value)
                        {
                            DepartmentService.UpdateDepartment(updatedDepartment);
                            SelectedNode.Id = updatedDepartment.Id;
                            SelectedNode.Name = updatedDepartment.Name;
                        }
                    },
                    obj => SelectedDepartment != null));
            }
        }
        public RelayCommand AddChildDepartment
        {
            get
            {
                return addChildDepartment ??
                    (addChildDepartment = new RelayCommand(obj =>
                    {
                        Department newChildDepartment = new Department()
                        {
                            ParentId = SelectedDepartment.Id
                        };

                        DepartmentWindow departmentWindow = new DepartmentWindow()
                        {
                            DataContext = new DepartmentViewModel(newChildDepartment)
                        };

                        departmentWindow.Owner = obj as Window;
                        departmentWindow.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                        departmentWindow.ShowDialog();

                        if (departmentWindow.DialogResult.Value)
                        {
                            DepartmentService.InsertDepartment(newChildDepartment);
                            Departments.Add(newChildDepartment);
                            SelectedNode.Nodes.Add(new Node(newChildDepartment.Id, newChildDepartment.Name));
                        }
                    },
                    obj => SelectedDepartment != null));
            }
        }
        public RelayCommand RemoveDepartment
        {
            get
            {
                return removeDepartment ??
                    (removeDepartment = new RelayCommand(obj =>
                    {
                        DepartmentService.DeleteDepartment(SelectedDepartment);
                        Departments.Remove(SelectedDepartment);
                        ObservableCollection<Node> parentNode = Nodes.GetParentNode(SelectedNode);
                        parentNode.Remove(SelectedNode);
                        ClientsInDepartment = null; //Для обновления отображения пустого списка клиентов
                    },
                    obj => SelectedDepartment != null));
            }
        }
        public RelayCommand AddClient
        {
            get
            {
                return addClient ??
                    (addClient = new RelayCommand(obj =>
                    {
                        ClientWindow clientWindow = new ClientWindow()
                        {
                            DataContext = new ClientViewModel()
                        };
                        clientWindow.ShowDialog();

                        if (SelectedDepartment != null)
                        {
                            //Обновляем информацию отображения клиентов в текущем выбранном департаменте
                            ClientsInDepartment = Repository.Clients.Where(x => x.DepartmentId == selectedDepartment.Id);
                        }
                    }));
            }
        }
        public RelayCommand EditClient
        {
            get
            {
                return editClient ??
                    (editClient = new RelayCommand(obj =>
                    {
                        ClientWindow clientWindow = new ClientWindow()
                        {
                            DataContext = new ClientViewModel(selectedClient)
                        };
                        clientWindow.ShowDialog();
                        ClientsInDepartment = Repository.Clients.Where(x => x.DepartmentId == selectedDepartment.Id);

                    },
                    obj => SelectedClient != null));
            }
        }
        public RelayCommand RemoveClient
        {
            get
            {
                return removeClient ??
                    (removeClient = new RelayCommand(obj =>
                    {
                        Repository.RemoveClient(SelectedClient);
                        //Обновляем информацию отображения клиентов в текущем выбранном департаменте
                        ClientsInDepartment = Repository.Clients.Where(x => x.DepartmentId == selectedDepartment.Id);
                    },
                    obj => SelectedClient != null));
            }
        }
        public RelayCommand AddDeposit
        {
            get
            {
                return addDeposit ??
                    (addDeposit = new RelayCommand(obj =>
                    {
                        OpenDepositWindow openDepositWindow = new OpenDepositWindow()
                        {
                            DataContext = new OpenDepositViewModel(account)
                        };
                        openDepositWindow.ShowDialog();
                        Deposites = Repository.Deposites.Where(x => x.ClientId == (selectedClient?.Id ?? 0));
                        Account = Repository.Accounts.Where(x => x.ClientId == (selectedClient?.Id ?? 0)).FirstOrDefault();
                    },
                    obj => SelectedClient != null));
            }
        }
        public RelayCommand SendTo
        {
            get
            {
                return sendTo ??
                    (sendTo = new RelayCommand(obj =>
                    {
                        Account = Repository.Accounts.Where(x => x.ClientId == (selectedClient?.Id ?? 0)).FirstOrDefault();
                        TransferBetweenAccountsWindow transferBetweenAccountsWindow = new TransferBetweenAccountsWindow()
                        {
                            DataContext = new TransferBetweenAccountsViewModel(Account)
                        };
                        transferBetweenAccountsWindow.Owner = obj as Window;
                        transferBetweenAccountsWindow.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                        transferBetweenAccountsWindow.ShowDialog();
                    },
                    obj => SelectedClient != null));
            }
        }

        #endregion

        #region Приватные методы

        private void Logging(object sender, AccountEventArgs e)
        {
            using (StreamWriter sw = new StreamWriter("log.txt", true))
            {
                sw.WriteLine(DateTime.Now + " " + e.Message);
            }
        }

        private void Account_Notify(object sender, AccountEventArgs e)
        {
            Debug.Print(e.Message);

            Application.Current.Dispatcher.InvokeAsync(async () =>
            {
                Window mainWindow = Application.Current.MainWindow;
                NotificationWindow alarm = new NotificationWindow()
                {
                    DataContext = new NotificationWindowViewModel()
                    {
                        Message = e.Message
                    }
                };
                alarm.Owner = mainWindow;
                alarm.Left = mainWindow.Left + mainWindow.Width - alarm.Width - 8;
                alarm.Top = mainWindow.Top + mainWindow.Height - alarm.Height - 8;
                alarm.Show();
                await Task.Delay(2000);
                alarm.Close();
            });
        }

        /// <summary>
        /// Формируем узлы для TreeView из департаментов
        /// </summary>
        /// <param name="dep"></param>
        /// <returns></returns>
        private ObservableCollection<Node> GetTreeViewNodes(Department department = null)
        {
            ObservableCollection<Node> nodes = new ObservableCollection<Node>();

            if (department == null)
            {
                var rootDep = Departments.Where(x => x.ParentId == 0).ToList();
                rootDep.ForEach(e =>
                {
                    var node = new Node(e.Id, e.Name);
                    node.Nodes = GetTreeViewNodes(e);
                    nodes.Add(node);
                });
                return nodes;
            }
            else
            {
                var subDep = Departments.Where(x => x.ParentId == department.Id).ToList();
                subDep.ForEach(e =>
                {
                    var node = new Node(e.Id, e.Name);
                    node.Nodes = GetTreeViewNodes(e);
                    nodes.Add(node);
                });
                return nodes;
            }
        }

        #endregion
    }
}
