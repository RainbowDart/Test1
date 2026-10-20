using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using ConsoleApp1;


var builder = Host.CreateApplicationBuilder(args);
string connString =
    builder.Configuration.GetConnectionString("Default")
    ?? throw new InvalidOperationException("not found string connection 'default'");
builder.Services.AddDbContext<AppDbContext>(opt => opt.UseNpgsql(connString));
using var host = builder.Build();
using var scope = host.Services.CreateScope();
var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
try
{
    bool canConnect = await db.Database.CanConnectAsync();

    if (!canConnect)
    {
        Console.WriteLine("Не удалось подключиться");
        return;

    }
    Console.WriteLine("Подключение прошло норм!");
    Console.WriteLine($"База данных: {db.Database.GetDbConnection().Database}");
    var students = await db.Students.ToListAsync();

    Console.WriteLine("Список студентов");
    foreach (var st in students)
    {
        Console.WriteLine($"{st.Id} : {st.FirstName} " +
            $"{st.LastName}, " +
            $"Возраст: {st.Age}");
    }
    Console.WriteLine($"Кол-во студентов: {students.Count}");
}
catch
{
    Console.WriteLine("Че-то не так(");
}