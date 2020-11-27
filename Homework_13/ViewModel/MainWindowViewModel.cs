using Homework_13.Enum;
using Homework_13.Helper;
using Homework_13.Model;
using Homework_13.Model.Event;
using Homework_13.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows;

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
        private IEnumerable<IDeposit> deposites; //Переделать после добавления вкладов
        private Node selectedNode;
        private ObservableCollection<Node> nodes;
        //private IEnumerable<Credit> credits;

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
                SelectedDepartment = Repository.Departments.Where(x => x.Id == SelectedNode.Id).FirstOrDefault();
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
            Repository.GetInstance();
            Nodes = GetTreeViewNodes();
            //departments = Repository.Departments as ObservableCollection<Department>;
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

                        DepartmentWindow departmentWindow = new DepartmentWindow(ActionType.CREATE, Nodes);

                        departmentWindow.Owner = obj as Window;
                        departmentWindow.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                        departmentWindow.ShowDialog();
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
                        DepartmentWindow departmentWindow = new DepartmentWindow(ActionType.EDIT, null, selectedDepartment);
                        departmentWindow.Owner = obj as Window;
                        departmentWindow.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                        departmentWindow.ShowDialog();
                        SelectedNode.Name = SelectedDepartment.Name;
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
                        ObservableCollection<Node> ChildNodes = SelectedNode.Nodes;
                        DepartmentWindow departmentWindow = new DepartmentWindow(ActionType.CREATE, ChildNodes, selectedDepartment);
                        departmentWindow.Owner = obj as Window;
                        departmentWindow.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                        departmentWindow.ShowDialog();
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
                        Repository.RemoveDepartment(SelectedDepartment);
                        ObservableCollection<Node> parentNode = GetParentNode(Nodes, SelectedNode);
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
                        ClientWindow clientWindow = new ClientWindow();
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
                        ClientWindow clientWindow = new ClientWindow(selectedClient);
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
                        OpenDepositWindow openDepositWindow = new OpenDepositWindow(account);
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
                        Account.Notify += Account_Notify;
                        TransferBetweenAccountsWindow transferBetweenAccountsWindow = new TransferBetweenAccountsWindow(Account);
                        transferBetweenAccountsWindow.Owner = obj as Window;
                        transferBetweenAccountsWindow.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                        transferBetweenAccountsWindow.ShowDialog();
                    },
                    obj => SelectedClient != null));
            }
        }

        private void Account_Notify(object sender, AccountEventArgs e)
        {
            Debug.Print(e.Message);
        }

        #endregion

        #region Приватные методы

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
                var rootDep = Repository.Departments.Where(x => x.ParentId == 0).ToList();
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
                var subDep = Repository.Departments.Where(x => x.ParentId == department.Id).ToList();
                subDep.ForEach(e =>
                {
                    var node = new Node(e.Id, e.Name);
                    node.Nodes = GetTreeViewNodes(e);
                    nodes.Add(node);
                });
                return nodes;
            }
        }

        /// <summary>
        /// Получает коллекцию содержащую узел который необходимо удалить
        /// </summary>
        /// <param name="nodes"></param>
        /// <param name="node"></param>
        /// <returns></returns>
        private ObservableCollection<Node> GetParentNode(ObservableCollection<Node> nodes, Node node)
        {
            ObservableCollection<Node> resultCollection = new ObservableCollection<Node>();

            if (nodes.Contains(node))
            {
                resultCollection = nodes;
            }
            else
            {
                foreach (var item in nodes)
                {
                    resultCollection = GetParentNode(item.Nodes, node);
                    if (resultCollection.Count != 0)
                    {
                        break;
                    }
                }
            }
            return resultCollection;
        }

        #endregion
    }
}
