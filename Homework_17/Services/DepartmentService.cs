using System;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Data;
using Homework_17.Model;
using System.Collections.ObjectModel;

namespace Homework_17.Services
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

        public static ObservableCollection<Department> GetAllDepartments()
        {
            string sqlExpression = "SELECT * FROM Departments";
            ObservableCollection<Department> departments = new ObservableCollection<Department>();
            try
            {
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
                            Debug.WriteLine("Departments");
                            Debug.WriteLine($"{reader.GetName(0)}\t{reader.GetName(1)}\t{reader.GetName(2)}");

                            while (reader.Read()) // построчно считываем данные
                            {
                                Debug.WriteLine($"{reader.GetValue(0)}\t{reader.GetValue(1)}\t{reader.GetValue(2)}");

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
            }
            catch (SqlException ex)
            {
                throw ex;
            }

            return departments;
        }

        public static Department SelectDepartment(int id)
        {
            string sqlExpression = @"SELECT * FROM Departments
                                   WHERE Id = @Id";
            Department department = new Department();

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionStringBuilder.ConnectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(sqlExpression, connection);
                    command.Parameters.Add("@Id", SqlDbType.Int).Value = id;

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            Debug.WriteLine("Departments");
                            Debug.WriteLine($"{reader.GetName(0)}\t{reader.GetName(1)}\t{reader.GetName(2)}");

                            while (reader.Read())
                            {
                                Debug.WriteLine($"{reader.GetValue(0)}\t{reader.GetValue(1)}\t{reader.GetValue(2)}");

                                department.Id = reader.GetInt32(0);
                                department.ParentId = reader.GetInt32(1);
                                department.Name = reader.GetString(2);
                            }
                        }
                        else
                        {
                            department = null;
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            
            return department;
        }

        public static void InsertDepartment(Department department)
        {
            string sqlExpression = @"INSERT INTO Departments (ParentId,  Name) 
                                 VALUES (@ParentId, @Name);
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
                        Direction = ParameterDirection.Output // параметр выходной
                    };

                    command.Parameters.Add(idParam);
                    command.Parameters.Add("@ParentId", SqlDbType.Int).Value = department.ParentId;
                    command.Parameters.Add("@Name", SqlDbType.NVarChar).Value = department.Name;

                    int number = command.ExecuteNonQuery();

                    Debug.WriteLine($"Добавлено департаментов: {number}");
                    Debug.WriteLine($"\tId нового департамента: {idParam.Value}");

                    department.Id = (int)idParam.Value;
                }
            }
            catch (SqlException ex)
            {

                throw ex;
            }
            
        }

        public static void UpdateDepartment(Department department)
        {
            string sqlExpression = @"UPDATE Departments SET 
                                   ParentId = @ParentId, 
                                   Name = @Name 
                                   WHERE Id = @Id";
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionStringBuilder.ConnectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(sqlExpression, connection);

                    command.Parameters.Add("@Id", SqlDbType.Int).Value = department.Id;
                    command.Parameters.Add("@ParentId", SqlDbType.Int).Value = department.ParentId;
                    command.Parameters.Add("@Name", SqlDbType.NVarChar).Value = department.Name;

                    int number = command.ExecuteNonQuery();

                    Debug.WriteLine($"Изменено департаментов: {number}");
                }
            }
            catch (SqlException ex)
            {

                throw ex;
            }
        }

        public static void DeleteDepartment(Department department)
        {
            //Рекурсивное удаление всех вложенных департаментов
            string sqlExpression =
                @"WITH RecursiveQuery (Id, ParentId, Name)
                AS
                (
                SELECT Id, ParentId, Name
                FROM Departments dep
                WHERE dep.Id = @Id
                UNION ALL
                SELECT dep.Id, dep.ParentId, dep.Name
                FROM Departments dep
                JOIN RecursiveQuery rec ON dep.ParentId = rec.Id
                )
                
                DELETE FROM Departments
                WHERE Id in (SELECT Id From RecursiveQuery)";
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionStringBuilder.ConnectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(sqlExpression, connection);

                    command.Parameters.Add("@Id", SqlDbType.Int).Value = department.Id;
                    int number = command.ExecuteNonQuery();

                    Debug.WriteLine($"Удалено департаментов: {number}");
                }
            }
            catch (SqlException ex)
            {

                throw ex;
            }          
        }
    }
}
