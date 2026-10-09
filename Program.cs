using System;
using System.Data;

namespace ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            DataTable table = new DataTable("Students");
            table.Columns.Add("Id", typeof(int));
            table.Columns.Add("Name", typeof(string));
            table.Columns.Add("Age", typeof(int));
            table.Columns.Add("GroupName", typeof(string));

            table.Rows.Add(1, "Иван Иванов", 20, "ИС-21");
            table.Rows.Add(2, "Мария Марья", 22, "ИС-21");
            table.Rows.Add(3, "Влда Владович", 19, "ИС-22");
            table.Rows.Add(4, "Ольга Попова", 23, "ИС-22");
            table.Rows.Add(5, "Сергей БУрунов", 21, "ИС-21");

            Print(table);

            int searchId = 2;
            foreach (DataRow row in table.Rows)
            {
                if ((int)row["Id"] == searchId)
                {
                    row["Age"] = 25;
                    Console.WriteLine($"\nИзменён возраст студента с Id={searchId} на 25");
                    break;
                }
            }
            table.Rows.Add(6, "Никита Андрушкевич", 20, "ИС-22");
            Console.WriteLine("Добавлен новый студент");
            int deleteId = 3;
            for (int i = 0; i < table.Rows.Count; i++)
            {
                if ((int)table.Rows[i]["Id"] == deleteId)
                {
                    table.Rows.RemoveAt(i);
                    Console.WriteLine($"Удалён студент с Id={deleteId}");
                    break;
                }
            }
            Console.WriteLine("\nТаблица после изменений");
            Print(table);
            Console.ReadLine();
        }
        static void Print(DataTable table)
        {
            Console.WriteLine("Id | Name               | Age | GroupName");
            Console.WriteLine("---------------------------------------------");
            foreach (DataRow row in table.Rows)
            {
                Console.WriteLine($"{row["Id"],-3}| {row["Name"],-19}| {row["Age"],-4}| {row["GroupName"]}");
            }
        }
    }
}