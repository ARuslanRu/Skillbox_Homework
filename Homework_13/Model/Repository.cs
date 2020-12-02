using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;

namespace Homework_13.Model
{
    class Repository
    {
        private static Repository instance;
        private static ObservableCollection<Department> departments;
        private static ObservableCollection<Client> clients;
        private static ObservableCollection<Account> accounts;
        private static ObservableCollection<IDeposit> deposites;

        private Repository()
        {
            Departments = new ObservableCollection<Department>();
            Clients = new ObservableCollection<Client>();
            Accounts = new ObservableCollection<Account>();
            Deposites = new ObservableCollection<IDeposit>();

            AddDepartment(new Department { Name = "Департамент_01", ParentId = 0 });
            AddDepartment(new Department { Name = "Департамент_02", ParentId = 0 });
            AddDepartment(new Department { Name = "Департамент_03", ParentId = 1 });

            AddClient(new Client { Name = "Клиент_01", DepartmentId = 1, Id = 1 });
            AddClient(new Client { Name = "Клиент_02", DepartmentId = 1 });
            AddClient(new Client { Name = "Клиент_03", DepartmentId = 2 });
            AddAcount(new Account { Balance = 10000, ClientId = 1, CreateDate = DateTime.Now, Id = 1 });
            AddDeposit(new Deposit { Name = "Вклад открытый 3 месяца назад", ClientId = Clients.First().Id, Balance = 1000, CreateDate = DateTime.Now.AddMonths(-3) });
            AddDeposit(new Deposit { Name = "Вклад открытый 12 месяца назад", ClientId = Clients.First().Id, Balance = 1000, CreateDate = DateTime.Now.AddMonths(-12) });
            AddDeposit(new DepositWithCapitalization { Name = "С капитализацией открытый 3 месяца назад", ClientId = Clients.First().Id, Balance = 1000, CreateDate = DateTime.Now.AddMonths(-3) });
            AddDeposit(new DepositWithCapitalization { Name = "С капитализацией открытый 6 месяца назад", ClientId = Clients.First().Id, Balance = 1000, CreateDate = DateTime.Now.AddMonths(-6) });
        }

        public static Repository GetInstance()
        {
            if (instance == null)
                instance = new Repository();
            return instance;
        }

        public static IReadOnlyCollection<Department> Departments
        {
            get
            {
                return departments;
            }
            private set
            {
                departments = value as ObservableCollection<Department>;
            }
        }
        public static IReadOnlyCollection<Client> Clients
        {
            get
            {
                return clients;
            }
            private set
            {
                clients = value as ObservableCollection<Client>;
            }
        }
        public static IReadOnlyCollection<Account> Accounts
        {
            get
            {
                return accounts;
            }
            private set
            {
                accounts = value as ObservableCollection<Account>;
            }
        }
        public static IReadOnlyCollection<IDeposit> Deposites
        {
            get
            {
                return deposites;
            }
            private set
            {
                deposites = value as ObservableCollection<IDeposit>;
            }
        }
        public static void AddDepartment(Department department)
        {
            if (department.Id == 0)
            {
                department.Id = GetId<Department>(departments);
            }

            departments.Add(department);
        }
        public static void RemoveDepartment(Department department)
        {
            var clients = Clients.Where(x => x.DepartmentId == department.Id).ToList();
            foreach (var client in clients)
            {
                RemoveClient(client);
            }

            var childDepartments = Departments.Where(x => x.ParentId == department.Id).ToList();
            foreach (var childDepartment in childDepartments)
            {
                RemoveDepartment(childDepartment);
            }

            departments.Remove(department);
            Debug.Print($"Удален департамент: ID {department.Id} | Name {department.Name}");
        }
        public static void AddClient(Client client)
        {
            if (client.Id == 0)
            {
                client.Id = GetId<Client>(clients);
                accounts.Add(new Account { Id = GetId<Account>(accounts), Balance = 0, ClientId = client.Id });
            }
            clients.Add(client);
        }
        public static void RemoveClient(Client client)
        {
            var clientAccounts = Accounts.Where(x => x.ClientId == client.Id).ToList();
            foreach (var account in clientAccounts)
            {
                RemoveAccount(account);
            }

            var clientDeposits = Deposites.Where(x => x.ClientId == client.Id).ToList();
            foreach (var deposit in clientDeposits)
            {
                RemoveDeposit(deposit);             
            }

            clients.Remove(client);
            Debug.Print($"Удален клиент: ID {client.Id} | Name {client.Name} | DepID {client.DepartmentId}");
        }
        public static void AddAcount(Account account)
        {
            if (account.Id == 0)
            {
                account.Id = GetId<Account>(accounts);
            }
            accounts.Add(account);
        }
        public static void RemoveAccount(Account account)
        {
            accounts.Remove(account);
            Debug.Print($"Удален счет: ID {account.Id}");
        }
        public static void AddDeposit(IDeposit deposit)
        {
            if (deposit.Id == 0)
            {
                deposit.Id = GetId<IDeposit>(deposites);
            }
            deposites.Add(deposit);
        }
        public static void RemoveDeposit(IDeposit deposit)
        {
            deposites.Remove(deposit);
            Debug.Print($"Удален депосит: ID {deposit.Id} | Name {deposit.Name}");
        }


        #region Возможно тут как раз можно использовать Generic
        private static int GetId<T>(ObservableCollection<T> collections) where T : IIdentity
        {
            int id;
            if (collections.Count != 0)
            {
                int[] number = collections.Select(x => x.Id).ToArray();
                int[] missingNumbers = Enumerable.Range(1, number[number.Length - 1]).Except(number).ToArray();
                id = missingNumbers.Length == 0 ? number.Max() + 1 : missingNumbers.FirstOrDefault();
            }
            else
            {
                id = 1;
            }
            return id;
        }

        #endregion
    }
}
