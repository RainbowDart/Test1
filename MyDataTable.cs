using System;
using System.Data;
using Npgsql;

namespace ConsoleApp1
{
    public class MyDataTable
    {
        public static void Run()
        {
            string connString = "Host=wotacoma.ru;Port=5090;Username=students;Password=asd123qwe4ewq321dsa;Database=9/3-RPO-24/1";

            var conn = new NpgsqlConnection(connString);
            conn.Open();

            var insertCmd = new NpgsqlCommand("""
                INSERT INTO students_lesson13 (nickname, first_name, last_name, "group")
                VALUES (@nick, @fn, @ln, @gr)
                """, conn);

            insertCmd.Parameters.AddWithValue("@nick", "durbesel");
            insertCmd.Parameters.AddWithValue("@fn", "Vlad");
            insertCmd.Parameters.AddWithValue("@ln", "Chistalev");
            insertCmd.Parameters.AddWithValue("@gr", "RPO 24/1");

            try
            {
                int rows = insertCmd.ExecuteNonQuery();
                Console.WriteLine($"Добавлено строк: {rows}");
            }
            catch
            {
                Console.WriteLine("Повторка, такой есть уже! Ниче не добавил");
            }

            conn.Close();

            DataTable table = new DataTable("Students");

            var conn2 = new NpgsqlConnection(connString);
            conn2.Open();

            var selectCmd = new NpgsqlCommand(
                "SELECT id, nickname, first_name, last_name, \"group\" FROM students_lesson13", conn2);

            var adapter = new NpgsqlDataAdapter(selectCmd);
            adapter.Fill(table);

            conn2.Close();

            Console.WriteLine("\nСтуденты \n");
            Console.WriteLine("Id | Nickname    | FirstName | LastName   | Group");

            foreach (DataRow row in table.Rows)
            {
                Console.WriteLine(
                    $"{row["id"],-3}| {row["nickname"],-12}| {row["first_name"],-10}| {row["last_name"],-11}| {row["group"]}");
            }

            Console.WriteLine($"\nВсего студентов: {table.Rows.Count}");
            Console.ReadLine();
        }
    }
}