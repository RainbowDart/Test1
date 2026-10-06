using System;
using System.Data;
using System.Data.Common;
using Microsoft.Data.Sqlite;

namespace ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {

            DbProviderFactories.RegisterFactory("Microsoft.Data.Sqlite", SqliteFactory.Instance);


            DbProviderFactory factory = DbProviderFactories.GetFactory("Microsoft.Data.Sqlite");
            using (DbConnection connection = factory.CreateConnection())
            {
                connection.ConnectionString = "Data Source=wotacoma.db";
                connection.Open();

                using (DbCommand command = factory.CreateCommand())
                {
                    command.Connection = connection;

                    command.CommandText = "CREATE TABLE IF NOT EXISTS test01 (id INTEGER PRIMARY KEY, last_name TEXT, first_name TEXT)";
                    command.ExecuteNonQuery();
                    command.CommandText = "INSERT INTO test01 (last_name, first_name) VALUES ('Чисталев', 'Владислав')";
                    command.ExecuteNonQuery();
                    command.CommandText = "SELECT * FROM test01";
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