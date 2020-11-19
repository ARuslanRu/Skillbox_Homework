using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;

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

            AddDepartment(new Department { Name = "Департамент_01" });
            AddDepartment(new Department { Name = "Департамент_02" });

            AddClient(new Client { Name = "Клиент_01", DepartmentId = 1, Id = 1 });
            AddClient(new Client { Name = "Клиент_02", DepartmentId = 1 });
            AddClient(new Client { Name = "Клиент_03", DepartmentId = 2 });
            AddAcount(new Account { Balance = 10000, ClientId = 1, CreateDate = DateTime.Now, Id = 1 });
            AddDeposit(new Deposit { Name = "Вклад_01", ClientId = Clients.First().Id, Balance = 1000, CreateDate = DateTime.Now.AddMonths(-3) });
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
                department.Id = GetDepartmentId();
            }

            departments.Add(department);
        }
        public static void RemoveDepartment(Department department)
        {
            var clients = Repository.Clients.Where(x => x.DepartmentId == department.Id).ToList();
            foreach (var client in clients)
            {
                Repository.RemoveClient(client);
                Debug.Print($"Удален клиент: ID {client.Id} | Name {client.Name} | DepID {client.DepartmentId}");
            }

            departments.Remove(department);
        }
        public static void AddClient(Client client)
        {
            if (client.Id == 0)
            {
                client.Id = GetClientId();
                accounts.Add(new Account { Id = GetAccountId(), Balance = 0, ClientId = client.Id });
            }
            clients.Add(client);
        }
        public static void RemoveClient(Client client)
        {
            //TODO: Удалять у клиента все счета, вклады и кредиты. После этого удалять самого клиента.
            clients.Remove(client);
        }
        public static void AddAcount(Account account)
        {
            if (account.Id == 0)
            {
                account.Id = GetClientId();
            }
            accounts.Add(account);
        }
        public static void RemoveAccount(Account account)
        {
            accounts.Remove(account);
        }
        public static void AddDeposit(IDeposit deposit)
        {
            if (deposit.Id == 0)
            {
                deposit.Id = GetDepositId();
            }
            deposites.Add(deposit);
        }
        public static void RemoveDeposit(IDeposit deposit)
        {
            deposites.Remove(deposit);
        }


        #region Возможно тут как раз можно использовать Generic
        private static int GetDepartmentId()
        {
            int departmentId;
            if (Departments.Count != 0)
            {
                int[] number = Departments.Select(x => x.Id).ToArray();
                int[] missingNumbers = Enumerable.Range(1, number[number.Length - 1]).Except(number).ToArray();
                departmentId = missingNumbers.Length == 0 ? number.Max() + 1 : missingNumbers.FirstOrDefault();
            }
            else
            {
                departmentId = 1;
            }
            return departmentId;
        }
        private static int GetClientId()
        {
            int clientId;
            if (Clients.Count != 0)
            {
                int[] number = Clients.Select(x => x.Id).ToArray();
                int[] missingNumbers = Enumerable.Range(1, number[number.Length - 1]).Except(number).ToArray();
                clientId = missingNumbers.Length == 0 ? number.Max() + 1 : missingNumbers.FirstOrDefault();
            }
            else
            {
                clientId = 1;
            }
            return clientId;
        }
        private static int GetAccountId()
        {
            int accountId;
            if (Accounts.Count != 0)
            {
                int[] number = Accounts.Select(x => x.Id).ToArray();
                int[] missingNumbers = Enumerable.Range(1, number[number.Length - 1]).Except(number).ToArray();
                accountId = missingNumbers.Length == 0 ? number.Max() + 1 : missingNumbers.FirstOrDefault();
            }
            else
            {
                accountId = 1;
            }
            return accountId;
        }

        private static int GetDepositId()
        {
            int depositId;
            if (Deposites.Count != 0)
            {
                int[] number = Deposites.Select(x => x.Id).ToArray();
                int[] missingNumbers = Enumerable.Range(1, number[number.Length - 1]).Except(number).ToArray();
                depositId = missingNumbers.Length == 0 ? number.Max() + 1 : missingNumbers.FirstOrDefault();
            }
            else
            {
                depositId = 1;
            }
            return depositId;
        }


        #endregion
    }
}
