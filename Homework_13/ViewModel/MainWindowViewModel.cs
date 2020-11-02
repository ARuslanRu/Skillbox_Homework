using Homework_13.Helper;
using Homework_13.Model;
using Homework_13.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace Homework_13.ViewModel
{
    class MainWindowViewModel : BaseViewModel
    {

        #region fields
        private Department selectedDepartment;
        private ObservableCollection<Department> departments;
        private Client selectedClient;
        private IEnumerable<Client> clientsInDepartment;
        private IEnumerable<Account> accounts;
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
                OnPropertyChanged("SelectedDepartment");
            }
        }
        public ObservableCollection<Department> Departments
        {
            get { return departments; }
            set
            {
                departments = value;
                OnPropertyChanged("Departments");
            }
        }
        public Client SelectedClient
        {
            get { return selectedClient; }
            set
            {
                selectedClient = value;
                Accounts = Repository.Accounts.Where(x => x.ClientId == (selectedClient?.Id ?? 0));
                OnPropertyChanged("SelectedClient");
            }
        }
        public IEnumerable<Client> ClientsInDepartment
        {
            get { return clientsInDepartment; }
            set
            {
                clientsInDepartment = value;
                OnPropertyChanged("ClientsInDepartment");
            }
        }
        public IEnumerable<Account> Accounts
        {
            get { return accounts; }
            set
            {
                accounts = value;
                OnPropertyChanged("Accounts");
            }
        }
        #endregion

        #region constructor
        public MainWindowViewModel()
        {
            Repository.GetInstance();
            departments = Repository.Departments as ObservableCollection<Department>;
        }
        #endregion

        #region commands
        private RelayCommand addDepartment;
        private RelayCommand editDepartment;
        private RelayCommand removeDepartment;
        private RelayCommand addClient;
        private RelayCommand editClient;
        private RelayCommand removeClient;

        public RelayCommand AddDepartment
        {
            get
            {
                return addDepartment ??
                    (addDepartment = new RelayCommand(obj =>
                    {
                        DepartmentWindow groupWindow = new DepartmentWindow();
                        groupWindow.ShowDialog();
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
                        DepartmentWindow departmentWindow = new DepartmentWindow(selectedDepartment);
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
                        var clients = Repository.Clients.Where(x => x.DepartmentId == SelectedDepartment.Id).ToList();

                        foreach (var client in clients)
                        {
                            Repository.RemoveClient(client);
                            Debug.Print($"Удален клиент: ID {client.Id} | Name {client.Name} | DepID {client.DepartmentId}");
                        }

                        Repository.RemoveDepartment(SelectedDepartment);
                        ClientsInDepartment = null; //Для обновления отображения списка клиентов
                        Debug.Print(Repository.Departments.Count.ToString());
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
                        //Добавляем пока дефолтного клиента
                        //Repository.AddClient(new Client { Name = "Клиент_10", DepartmentId = SelectedDepartment.Id });
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
        #endregion
    }
}
