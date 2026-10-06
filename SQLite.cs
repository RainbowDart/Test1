using System;
using System.Data;
using System.Data.Common;
using Npgsql;

namespace ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            DbProviderFactories.RegisterFactory("Npgsql", NpgsqlFactory.Instance);
            DbProviderFactory factory = DbProviderFactories.GetFactory("Npgsql");

            using (DbConnection connection = factory.CreateConnection())
            {
                connection.ConnectionString = "";//палево
                connection.Open();

                using (DbCommand command = factory.CreateCommand())
                {
                    command.Connection = connection;

                    command.CommandText = "INSERT INTO students (first_name, last_name) VALUES ('Владислав', 'Чисталев')";
                    int rows = command.ExecuteNonQuery();
                    command.CommandText = "SELECT id, first_name, last_name FROM students";
                    using (DbDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Console.WriteLine($"{reader.GetInt32(0)} | {reader.GetString(1)} | {reader.GetString(2)}");
                        }
                    }
                }
            }

            Console.ReadLine();
        }
    }
}