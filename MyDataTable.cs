using System;
using System.Data;
using System.Data.Common;
using Npgsql;

namespace ConsoleApp11
{
    public class MyDataTable
    {
        public static void Run()
        {
            string connString = "Host=wotacoma.ru;Port=5090;Username=students;Password=asd123qwe4ewq321dsa;Database=9/3-RPO-24/1";

            DbProviderFactories.RegisterFactory("Npgsql", NpgsqlFactory.Instance);
            DbProviderFactory factory = DbProviderFactories.GetFactory("Npgsql");

            var conn = factory.CreateConnection();
            conn.ConnectionString = connString;
            conn.Open();

            DataTable table = new DataTable("Students");
            var cmd = factory.CreateCommand();
            cmd.Connection = conn;
            cmd.CommandText = "SELECT id, nickname, first_name, last_name FROM students_lesson13";

            var adapter = factory.CreateDataAdapter();
            adapter.SelectCommand = cmd;
            adapter.Fill(table);

            conn.Close();
            

            table.Rows[0]["first_name"] = "Вася Пупкин";

            DataRow newRow = table.NewRow();
            newRow["id"] = 999;
            newRow["nickname"] = "meybi";
            newRow["first_name"] = "Маша Кондрашкина";
            newRow["last_name"] = "Кондашкина";
            table.Rows.Add(newRow);

            table.Rows[1].Delete();
            foreach (DataRow row in table.Rows)
            {
                Console.WriteLine($"{row["id"],-4}| {row["first_name"],-15}| {row.RowState}");
            }
            Console.WriteLine(" Журнал изменений\n");
            PrintJournal(table);
            

            table.RejectChanges();

            Console.WriteLine("\nПосле отката");
            Console.WriteLine($"Всего строк: {table.Rows.Count}");
            foreach (DataRow row in table.Rows)
            {
                Console.WriteLine($"{row["id"],-4}| {row["first_name"],-15}| {row.RowState}");
            }

            Console.ReadLine();
        }

        static void PrintJournal(DataTable table)
        {
            foreach (DataRow row in table.Rows)
            {
                switch (row.RowState)
                {
                    case DataRowState.Modified:
                        string oldName = (string)row["first_name", DataRowVersion.Original];
                        string newName = (string)row["first_name", DataRowVersion.Current];
                        Console.WriteLine($"[MODIFIED] {oldName} → {newName}");
                        break;

                    case DataRowState.Added:
                        string addedName = (string)row["first_name", DataRowVersion.Current];
                        Console.WriteLine($"[ADDED]    {addedName}");
                        break;

                    case DataRowState.Deleted:
                        string deletedName = (string)row["first_name", DataRowVersion.Original];
                        Console.WriteLine($"[DELETED]  {deletedName}");
                        break;
                }
            }
        }
    }
}