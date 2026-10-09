using System;
using System.Data;

namespace ConsoleApp2
{
    internal class MyDataTable
    {
        public static void Run()
        {
            DataSet dataSet = new DataSet("Sharaga");

            DataTable groups = new DataTable("Groups");
            groups.Columns.Add("Id", typeof(int));
            groups.Columns.Add("Name", typeof(string));
            groups.PrimaryKey = new[] { groups.Columns["Id"]! };

            groups.Rows.Add(1, "Васькина контора");
            groups.Rows.Add(2, "Автолайф11");
            groups.Rows.Add(3, "ОООпокайфу");

            DataTable students = new DataTable("Students");
            students.Columns.Add("Id", typeof(int));
            students.Columns.Add("Name", typeof(string));
            students.Columns.Add("Age", typeof(int));
            students.Columns.Add("GroupId", typeof(int));
            students.PrimaryKey = new[] { students.Columns["Id"]! };

            students.Rows.Add(1, "Пупкин Саня", 15, 1);
            students.Rows.Add(2, "Васин Степа", 23, 2);
            students.Rows.Add(3, "Гренки Наталья", 53, 1);
            students.Rows.Add(4, "Грехов Виталя", 42, 2);
            students.Rows.Add(5, "Кукин Петя", 33, 3);

            dataSet.Tables.Add(groups);
            dataSet.Tables.Add(students);

            dataSet.Relations.Add(
                "GroupStudents",
                groups.Columns["Id"]!,
                students.Columns["GroupId"]!);

            Console.WriteLine("Студенты по группам\n");
            foreach (DataRow group in groups.Rows)
            {
                Console.WriteLine($"Группа: {group["Name"]}");
                DataRow[] childRows = group.GetChildRows("GroupStudents");
                foreach (DataRow student in childRows)
                {
                    Console.WriteLine($"  #{student["Id"],-3} {student["Name"],-20} {student["Age"]} лет");
                }
                Console.WriteLine();
            }
            try
            {
                groups.Rows.Add(1, "Дубликат");
                Console.WriteLine("Добавлено (не сработал PrimaryKey)");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }

            try
            {
                groups.Rows.Add(4, "");
                Console.WriteLine("Добавлено (не сработал AllowDBNull)");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }

            Console.WriteLine("\n3. Студенты без группы");
            bool found = false;
            foreach (DataRow student in students.Rows)
            {
                int groupId = (int)student["GroupId"];
                DataRow[] parent = groups.Select($"Id = {groupId}");

                if (parent.Length == 0)
                {
                    Console.WriteLine($"Студент без группы: #{student["Id"]} {student["Name"]} (GroupId={groupId})");
                    found = true;
                }
            }
            if (!found)
                Console.WriteLine("Все студенты привязаны к существующим группам");
            Console.ReadLine();
        }
    }
}