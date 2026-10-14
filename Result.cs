using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;

namespace ConsoleApp1
{
    public class Result
    {
        public static void RunResult()
        {
            string connString = "Host=wotacoma.ru;Port=5090;Username=students;Password=asd123qwe4ewq321dsa;Database=9/3-RPO-24/1";

            var conn = new NpgsqlConnection(connString);
            conn.Open();

            string name = "Chingook";
            string nickname = "durbesel";
            string lastName = "Chistalev";
            string firstName = "Vlad";

            //1
            using var cmd1 = new NpgsqlCommand($"""
                INSERT INTO students_lesson2 (nickname, first_name, last_name)
                VALUES ('{nickname}', '{firstName}', '{lastName}')
                """,
                conn
                );
            var rows1 = cmd1.ExecuteNonQuery();
            Console.WriteLine($"Количество затронутых строк: {rows1}");

            //2
            //using var cmd2 = new NpgsqlCommand($"""
            //    SELECT id FROM students_lesson2 WHERE nickname = '{nickname}'
            //    """,
            //    conn
            //    );
            //var id = cmd2.ExecuteScalar();
            //Console.WriteLine($"идентификатор: {id}");

            ////3
            using var cmd3 = new NpgsqlCommand("""
                SELECT id, name
                FROM dota_heroes
                WHERE name = @name
                """,
                conn
                );
            cmd3.Parameters.AddWithValue("@name", name);
            var reader = cmd3.ExecuteReader();

            //string nameHero = "";
            while (reader.Read())
            {
                //nameHero = reader.GetString(0);
                Console.WriteLine($"{reader.GetInt32(0),-20} | {reader.GetString(1),-20}");
            }

            ////4
            //using var cmd4 = new NpgsqlCommand($"""
            //    INSERT INTO result_lesson2 (nickname, name_hero)
            //    VALUES ('{nickname}', '{nameHero}')
            //    """,
            //    conn
            //    );
            //var rows4 = cmd4.ExecuteNonQuery();
            //Console.WriteLine($"Количество затронутых строк: {rows4}");

            ////5
            //using var cmd = new NpgsqlCommand($"UPDATE dota_heroes SET reserved_nickname='{nickname}' WHERE id = {id} ", conn);
            //var rows = cmd.ExecuteNonQuery();
            //Console.WriteLine($"Количество затронутых строк: {rows}");
        }
    }
}
