using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Configuration;
using System.Diagnostics;
using System.Data;
using Homework_13.Model;
using System.Collections.ObjectModel;

namespace Homework_13.Services
{
    class DepartmentService
    {
        private static SqlConnectionStringBuilder connectionStringBuilder = new SqlConnectionStringBuilder
        {
            DataSource = @"(LocalDB)\MSSQLLocalDB",
            InitialCatalog = "BankDB",
            AttachDBFilename = Environment.CurrentDirectory + @"\DataBase\BankDB.mdf",
            IntegratedSecurity = true
        };

        public static ObservableCollection<Department> GetAll()
        {
            string sqlExpression = "SELECT * FROM Departments";
            ObservableCollection<Department> departments = new ObservableCollection<Department>();
            using (SqlConnection connection = new SqlConnection(connectionStringBuilder.ConnectionString))
            {
                connection.Open();
                Debug.WriteLine("Подключение открыто");

                Debug.WriteLine("Свойства подключения:");
                Debug.WriteLine($"\tСтрока подключения: {connection.ConnectionString}");
                Debug.WriteLine($"\tБаза данных: {connection.Database}");
                Debug.WriteLine($"\tСервер: {connection.DataSource}");
                Debug.WriteLine($"\tВерсия сервера: {connection.ServerVersion}");
                Debug.WriteLine($"\tСостояние: {connection.State}");
                Debug.WriteLine($"\tWorkstationld: {connection.WorkstationId}");

                SqlCommand command = new SqlCommand(sqlExpression, connection);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows) // если есть данные
                    {
                        Debug.WriteLine($"{reader.GetName(0)}\t{reader.GetName(1)}\t{reader.GetName(2)}");

                        while (reader.Read()) // построчно считываем данные
                        {
                            object id = reader.GetValue(0);
                            object parentId = reader.GetValue(1);
                            object name = reader.GetValue(2);

                            Debug.WriteLine($"{id}\t{parentId}\t{name}");

                            departments.Add(new Department()
                            {
                                Id = reader.GetInt32(0),
                                ParentId = reader.GetInt32(1),
                                Name = reader.GetString(2)
                            });

                        }
                    }
                }
            }

            return departments;
        }

        public bool AddDepartment(Department department)
        {


            return false;
        }

    }
}
