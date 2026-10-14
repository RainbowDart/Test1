using System;
using System.Data;
using System.Data.Common;
using Npgsql;

namespace ConsoleApp2
{
    public class MyDataTable
    {
        public static void Run()
        {
            string connString = "Host=wotacoma.ru;Port=5090;Username=students;Password=asd123qwe4ewq321dsa;Database=9/3-RPO-24/1";
            DbProviderFactories.RegisterFactory("Npgsql", NpgsqlFactory.Instance);
            DbProviderFactory factory = DbProviderFactories.GetFactory("Npgsql");

            DataTable table = new DataTable("Students");
            var conn = factory.CreateConnection();
            conn.ConnectionString = connString;
            conn.Open();

            var cmd = factory.CreateCommand();
            cmd.Connection = conn;
            cmd.CommandText = "SELECT id, nickname, first_name, last_name FROM students_lesson13";

            var adapter = factory.CreateDataAdapter();
            adapter.SelectCommand = cmd;
            adapter.Fill(table);

            conn.Close();

            Console.WriteLine($"После загрузки");
            Console.WriteLine($"Всего строк: {table.Rows.Count}\n");
            PrintStates(table);

            DataRow newRow = table.NewRow();
            newRow["id"] = 0;
            newRow["nickname"] = "vladik";
            newRow["first_name"] = "Vlad";
            newRow["last_name"] = "Chistalev";

            Console.WriteLine("Новая строка до добавления");
            Console.WriteLine($"RowState: {newRow.RowState}");
            table.Rows.Add(newRow);

            Console.WriteLine("После добавления");
            Console.WriteLine($"RowState: {newRow.RowState}");

            DataRow first = table.Rows[0];
            Console.WriteLine($"Меняем имя студента: {first["first_name"]}");
            first["first_name"] = "изменено";

            Console.WriteLine("После изменения");
            Console.WriteLine($"RowState: {first.RowState}");

            DataRow second = table.Rows[1];
            Console.WriteLine($"Удаляем студента: {second["first_name"]}");
            second.Delete();

            Console.WriteLine("После удаления");
            Console.WriteLine($"RowState: {second.RowState}");

            Console.WriteLine("\nСостояния всех строк после всех изменений");
            PrintStates(table);


            table.RejectChanges();

            Console.WriteLine("\n После отката");
            Console.WriteLine($"Всего строк: {table.Rows.Count}");
            PrintStates(table);

            DataRow newRow2 = table.NewRow();
            newRow2["id"] = 0;
            newRow2["nickname"] = "vladik";
            newRow2["first_name"] = "Vlad";
            newRow2["last_name"] = "Chistalev";
            table.Rows.Add(newRow2);

            table.Rows[0]["first_name"] = "измененно2";
            table.Rows[1].Delete();

            Console.WriteLine("\nПеред AcceptChanges");
            PrintStates(table);

            table.AcceptChanges();

            Console.WriteLine("\nПосле AcceptChanges ");
            PrintStates(table);
            Console.ReadLine();
        }

        static void PrintStates(DataTable table)
        {
            Console.WriteLine("Id | FirstName   | RowState");

            foreach (DataRow row in table.Rows)
            {
                if (row.RowState == DataRowState.Deleted)
                {
                    int id = (int)row["id", DataRowVersion.Original];
                    string name = (string)row["first_name", DataRowVersion.Original];
                    Console.WriteLine($"{id,-3}| {name,-12}| Deleted");
                }
                else
                {
                    Console.WriteLine($"{row["id"],-3}| {row["first_name"],-12}| {row.RowState}");
                }
            }
        }
    }
}