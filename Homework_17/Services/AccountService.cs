using Homework_17.Model;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;

namespace Homework_17.Services
{
    class AccountService
    {
        private static SqlConnectionStringBuilder connectionStringBuilder = new SqlConnectionStringBuilder
        {
            DataSource = @"(LocalDB)\MSSQLLocalDB",
            InitialCatalog = "BankDB",
            AttachDBFilename = Environment.CurrentDirectory + @"\DataBase\BankDB.mdf",
            IntegratedSecurity = true
        };

        public static Account SelectAccount(int clientId)
        {
            string sqlExpression = @"SELECT * FROM Accounts
                                   WHERE ClientId = @ClientId";

            Account account = new Account();

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionStringBuilder.ConnectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(sqlExpression, connection);
                    command.Parameters.Add("@ClientId", SqlDbType.Int).Value = clientId;

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            Debug.WriteLine("Accounts");
                            Debug.WriteLine($"{reader.GetName(0)}\t{reader.GetName(1)}\t{reader.GetName(2)}\t{reader.GetName(3)}");

                            while (reader.Read())
                            {
                                Debug.WriteLine($"{reader.GetValue(0)}\t{reader.GetValue(1)}\t{reader.GetValue(2)}\t{reader.GetValue(3)}");
                                account.Id = reader.GetInt32(0);
                                account.ClientId = reader.GetInt32(1);
                                account.Balance = reader.GetDecimal(2);
                                account.CreateDate = reader.GetDateTime(3);
                            }
                        }
                        else
                        {
                            account = null;
                        }
                    }
                }
            }
            catch (SqlException ex)
            {

                throw ex;
            }      
            return account;
        }

        public static void InsertAccount(Account account)
        {
            string sqlExpression = @"INSERT INTO Accounts (ClientId,  Balance, CreateDate) 
                                   VALUES (@ClientId, @Balance, @CreateDate);
                                   SET @Id = @@IDENTITY;";
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionStringBuilder.ConnectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(sqlExpression, connection);

                    SqlParameter idParam = new SqlParameter
                    {
                        ParameterName = "@Id",
                        SqlDbType = SqlDbType.Int,
                        Direction = ParameterDirection.Output
                    };

                    command.Parameters.Add(idParam);
                    command.Parameters.Add("@ClientId", SqlDbType.Int).Value = account.ClientId;
                    command.Parameters.Add("@Balance", SqlDbType.Decimal).Value = account.Balance;
                    command.Parameters.Add("@CreateDate", SqlDbType.DateTime).Value = account.CreateDate;

                    int number = command.ExecuteNonQuery();

                    Debug.WriteLine($"Добавлено счетов: {number}");
                    Debug.WriteLine($"\tId нового счета: {idParam.Value}");

                    account.Id = (int)idParam.Value;
                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            
        }

        public static void UpdateAccount(Account account)
        {
            string sqlExpression = @"UPDATE Accounts SET 
                                   ClientId = @ClientId, 
                                   Balance = @Balance,
                                   CreateDate = @CreateDate
                                   WHERE Id = @Id";
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionStringBuilder.ConnectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(sqlExpression, connection);

                    command.Parameters.Add("@Id", SqlDbType.Int).Value = account.Id;
                    command.Parameters.Add("@ClientId", SqlDbType.Int).Value = account.ClientId;
                    command.Parameters.Add("@Balance", SqlDbType.Decimal).Value = account.Balance;
                    command.Parameters.Add("@CreateDate", SqlDbType.DateTime).Value = account.CreateDate;

                    int number = command.ExecuteNonQuery();

                    Debug.WriteLine($"Изменено счетов: {number}");
                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }

        public static void DeleteAccount(Account account)
        {
            string sqlExpression = @"DELETE FROM Accounts
                                   WHERE Id = @Id";
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionStringBuilder.ConnectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(sqlExpression, connection);

                    command.Parameters.Add("@Id", SqlDbType.Int).Value = account.Id;
                    int number = command.ExecuteNonQuery();

                    Debug.WriteLine($"Удалено счетов: {number}");
                }
            }
            catch (SqlException ex)
            {

                throw ex;
            }
           
        }
    }
}
