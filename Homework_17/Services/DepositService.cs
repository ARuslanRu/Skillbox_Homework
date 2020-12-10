using Homework_17.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Text;

namespace Homework_17.Services
{
    class DepositService
    {
        private static SqlConnectionStringBuilder connectionStringBuilder = new SqlConnectionStringBuilder
        {
            DataSource = @"(LocalDB)\MSSQLLocalDB",
            InitialCatalog = "BankDB",
            AttachDBFilename = Environment.CurrentDirectory + @"\DataBase\BankDB.mdf",
            IntegratedSecurity = true
        };


        public static ObservableCollection<Deposit> SelectClientDeposites(int clientId)
        {
            string sqlExpression = @"SELECT * FROM Deposites
                                   WHERE ClientId = @ClientId";

            ObservableCollection<Deposit> deposites = new ObservableCollection<Deposit>();

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

                            Debug.WriteLine("Deposites");
                            Debug.WriteLine($"{reader.GetName(0)}\t{reader.GetName(1)}\t{reader.GetName(2)}\t{reader.GetName(3)}\t{reader.GetName(4)}\t{reader.GetName(5)}");

                            while (reader.Read())
                            {
                                Debug.WriteLine($"{reader.GetValue(0)}\t{reader.GetValue(1)}\t{reader.GetValue(2)}\t{reader.GetValue(3)}\t{reader.GetValue(4)}\t{reader.GetValue(5)}");

                                deposites.Add(new Deposit()
                                {
                                    Id = reader.GetInt32(0),
                                    ClientId = reader.GetInt32(1),
                                    Name = reader.GetString(2),
                                    Balance = reader.GetDecimal(3),
                                    CreateDate = reader.GetDateTime(4),
                                    IsWithCapitalization = reader.GetBoolean(5)
                                });
                            }
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            return deposites;
        }
        public static void InsertDeposit(Deposit deposit)
        {

            string sqlExpression = @"INSERT INTO Deposites (ClientId,  Name, Balance, CreateDate, IsWithCapitalization) 
                                 VALUES (@ClientId, @Name, @Balance, @CreateDate, @IsWithCapitalization);
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
                    command.Parameters.Add("@ClientId", SqlDbType.Int).Value = deposit.ClientId;
                    command.Parameters.Add("@Name", SqlDbType.NVarChar).Value = deposit.Name;
                    command.Parameters.Add("@Balance", SqlDbType.Decimal).Value = deposit.Balance;
                    command.Parameters.Add("@CreateDate", SqlDbType.DateTime).Value = deposit.CreateDate;
                    command.Parameters.Add("@IsWithCapitalization", SqlDbType.Bit).Value = deposit.IsWithCapitalization;

                    int number = command.ExecuteNonQuery();

                    Debug.WriteLine($"Добавлено департаментов: {number}");
                    Debug.WriteLine($"\tId нового департамента: {idParam.Value}");

                    deposit.Id = (int)idParam.Value;
                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }
    }
}
