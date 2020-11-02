using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

        private Repository()
        {
            Departments = new ObservableCollection<Department>();
            Clients = new ObservableCollection<Client>();
            Accounts = new ObservableCollection<Account>();

            departments.Add(new Department { Id = 1, Name = "Департамент_01" });
            departments.Add(new Department { Id = 2, Name = "Департамент_02" });

            clients.Add(new Client { Id = 1, Name = "Клиент_01", DepartmentId = 1 });
            clients.Add(new Client { Id = 2, Name = "Клиент_02", DepartmentId = 1 });
            clients.Add(new Client { Id = 3, Name = "Клиент_03", DepartmentId = 2 });

            accounts.Add(new Account { Id = 1, Name = "Основной счет", Balance = 10000, ClientId = 1 });
            accounts.Add(new Account { Id = 1, Name = "Основной счет", Balance = 10000, ClientId = 1 });
            accounts.Add(new Account { Id = 1, Name = "Основной счет", Balance = 10000, ClientId = 1 });
            accounts.Add(new Account { Id = 1, Name = "Основной счет", Balance = 10000, ClientId = 1 });
            accounts.Add(new Account { Id = 1, Name = "Основной счет", Balance = 10000, ClientId = 1 });
            accounts.Add(new Account { Id = 1, Name = "Основной счет", Balance = 10000, ClientId = 1 });
            accounts.Add(new Account { Id = 1, Name = "Основной счет", Balance = 10000, ClientId = 1 });
            accounts.Add(new Account { Id = 1, Name = "Основной счет", Balance = 10000, ClientId = 1 });
            accounts.Add(new Account { Id = 1, Name = "Основной счет", Balance = 10000, ClientId = 1 });
            accounts.Add(new Account { Id = 1, Name = "Основной счет", Balance = 10000, ClientId = 1 });
            accounts.Add(new Account { Id = 1, Name = "Основной счет", Balance = 10000, ClientId = 1 });
            accounts.Add(new Account { Id = 1, Name = "Основной счет", Balance = 10000, ClientId = 1 });
            accounts.Add(new Account { Id = 1, Name = "Основной счет", Balance = 10000, ClientId = 1 });
            accounts.Add(new Account { Id = 1, Name = "Основной счет", Balance = 10000, ClientId = 1 });
            accounts.Add(new Account { Id = 1, Name = "Основной счет", Balance = 10000, ClientId = 1 });
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

        public static void AddDepartment(Department department)
        {
            if(department.Id == 0)
            {
                department.Id = GetDepartmentId();
            }

            departments.Add(department);
        }
        public static void RemoveDepartment(Department department)
        {
            departments.Remove(department);
        }
        public static void AddClient(Client client)
        {
            if(client.Id == 0)
            {
                client.Id = GetClientId();
            }
            clients.Add(client);
        }
        public static void RemoveClient(Client client)
        {
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


        #endregion
    }
}
