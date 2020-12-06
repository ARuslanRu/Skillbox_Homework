﻿using Homework_13.Model;
using System;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;

namespace Homework_13.Services
{
    class ClientService
    {
        private static SqlConnectionStringBuilder connectionStringBuilder = new SqlConnectionStringBuilder
        {
            DataSource = @"(LocalDB)\MSSQLLocalDB",
            InitialCatalog = "BankDB",
            AttachDBFilename = Environment.CurrentDirectory + @"\DataBase\BankDB.mdf",
            IntegratedSecurity = true
        };

        public static ObservableCollection<Client> GetClientsInDepartment(Department department)
        {
            string sqlExpression = @"SELECT * FROM Clients
                                   WHERE DepartmentId = @DepartmentId";

            ObservableCollection<Client> clients = new ObservableCollection<Client>();

            using (SqlConnection connection = new SqlConnection(connectionStringBuilder.ConnectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                command.Parameters.Add("@DepartmentId", SqlDbType.Int).Value = department.Id;

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        Debug.WriteLine($"{reader.GetName(0)}\t{reader.GetName(1)}\t{reader.GetName(2)}");

                        while (reader.Read())
                        {
                            Debug.WriteLine($"{reader.GetValue(0)}\t{reader.GetValue(1)}\t{reader.GetValue(2)}");

                            clients.Add(new Client()
                            {
                                Id = reader.GetInt32(0),
                                DepartmentId = reader.GetInt32(1),
                                Name = reader.GetString(2)
                            });
                        }
                    }
                }
            }
            return clients;
        }

        public static void InsertClient(Client client)
        {
            string sqlExpression = @"INSERT INTO Clients (DepartmentId,  Name) 
                                   VALUES (@DepartmentId, @Name);
                                   SET @Id = @@IDENTITY;";

            ObservableCollection<Client> clients = new ObservableCollection<Client>();

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
                command.Parameters.Add("@DepartmentId", SqlDbType.Int).Value = client.DepartmentId;
                command.Parameters.Add("@Name", SqlDbType.NVarChar).Value = client.Name;

                int number = command.ExecuteNonQuery();

                Debug.WriteLine($"Добавлено клиентов: {number}");
                Debug.WriteLine($"\tId нового клиента: {idParam.Value}");

                client.Id = (int)idParam.Value;
            }
        }
        public static void UpdateClient(Client client)
        {
            string sqlExpression = @"UPDATE Clients SET 
                           DepartmentId = @DepartmentId, 
                           Name = @Name 
                           WHERE Id = @Id";

            using (SqlConnection connection = new SqlConnection(connectionStringBuilder.ConnectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);

                command.Parameters.Add("@Id", SqlDbType.Int).Value = client.Id;
                command.Parameters.Add("@DepartmentId", SqlDbType.Int).Value = client.DepartmentId;
                command.Parameters.Add("@Name", SqlDbType.NVarChar).Value = client.Name;

                int number = command.ExecuteNonQuery();

                Debug.WriteLine($"Изменено департаментов: {number}");
            }
        }
        public static void DeleteClient(Client client)
        {
            string sqlExpression = @"DELETE FROM Clients
                                   WHERE Id = @Id";

            using (SqlConnection connection = new SqlConnection(connectionStringBuilder.ConnectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);

                command.Parameters.Add("@Id", SqlDbType.Int).Value = client.Id;
                int number = command.ExecuteNonQuery();

                Debug.WriteLine($"Удалено клиентов: {number}");
            }
        }
    }
}
