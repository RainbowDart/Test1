
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

            Console.WriteLine("Id | Name               | Age | GroupName");
            foreach (DataRow row in table.Rows)
            {
                Console.WriteLine($"{row["Id"],-3}| {row["Name"],-19}| {row["Age"],-4}| {row["GroupName"]}");
            }
            DataRow oldest = table.Rows[0];
            for (int i = 1; i < table.Rows.Count; i++)
            {
                if ((int)table.Rows[i]["Age"] > (int)oldest["Age"])
                {
                    oldest = table.Rows[i];
                }
            }

            Console.WriteLine($"\nСамый старший: {oldest["Name"]}, возраст: {oldest["Age"]}");
            Console.ReadLine();
        }
    }
}