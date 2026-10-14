using System;
using System.Data;
using Npgsql;

namespace ConsoleApp11
{
    public class MyDataTable
    {
        public static void Run()
        {
            string sourceConn = "Host=wotacoma.ru;Port=5090;Username=students;Password=asd123qwe4ewq321dsa;Database=9/3-RPO-24/1";
            string targetConn = "Host=wotacoma.ru;Port=5090;Username=rainbowdart;Password=123321;Database=rainbowdart";
            DataTable source = new DataTable();
            var conn1 = new NpgsqlConnection(sourceConn);
            conn1.Open();

            var selectCmd = new NpgsqlCommand(
                "SELECT id, nickname, first_name, last_name, \"group\" FROM students_lesson13", conn1);
            var adapter = new NpgsqlDataAdapter(selectCmd);
            adapter.Fill(source);

            conn1.Close();

            Console.WriteLine($"Прочитано записей: {source.Rows.Count}");

            var conn2 = new NpgsqlConnection(targetConn);
            conn2.Open();
            var createCmd = new NpgsqlCommand("""
                CREATE TABLE IF NOT EXISTS students_lesson13 (
                    id SERIAL PRIMARY KEY,
                    nickname TEXT,
                    first_name TEXT,
                    last_name TEXT,
                    "group" TEXT
                )
                """, conn2);
            createCmd.ExecuteNonQuery();


            foreach (DataRow row in source.Rows)
            {
                try
                {
                    var insertCmd = new NpgsqlCommand("""
                        INSERT INTO students_lesson13 (nickname, first_name, last_name, "group")
                        VALUES (@nick, @fn, @ln, @gr)
                        """, conn2);

                    insertCmd.Parameters.AddWithValue("@nick", row["nickname"]);
                    insertCmd.Parameters.AddWithValue("@fn", row["first_name"]);
                    insertCmd.Parameters.AddWithValue("@ln", row["last_name"]);
                    insertCmd.Parameters.AddWithValue("@gr", row["group"]);

                    insertCmd.ExecuteNonQuery();
                    Console.WriteLine($"Перенесён: {row["first_name"]} {row["last_name"]}");
                }
                catch
                {
                    Console.WriteLine($"Дубликат: {row["first_name"]} {row["last_name"]}");
                }
            }

            conn2.Close();
            Console.ReadLine();
        }
    }
}