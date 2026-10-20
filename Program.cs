using System;
using ConsoleApp1;

var student = new Student
{
    Id = 1,
    FirstName = "Влас",
    LastName = "Петрович",
    Age = 19,
    Email = "vlas@gmail.com",
    EnrollmentYear = 2025
};

Console.WriteLine("Анкета студента");
Console.WriteLine($"Студент: {student.FirstName} {student.LastName}");
Console.WriteLine($"Возраст: {student.Age}");
Console.WriteLine($"Email: {student.Email}");
Console.WriteLine($"Год поступления: {student.EnrollmentYear}");

student.Age = 27;
student.LastName = "Педоренко";
student.FirstName = "Федот";
student.Email = "raindow@yandex.sru";

Console.WriteLine("\nПосле изменения:");
Console.ForegroundColor = ConsoleColor.Red;
Console.WriteLine($"Студент: {student.FirstName} {student.LastName}");
Console.WriteLine($"Возраст: {student.Age}");
Console.WriteLine($"Email: {student.Email}");
Console.WriteLine($"Год поступления: {student.EnrollmentYear}");
