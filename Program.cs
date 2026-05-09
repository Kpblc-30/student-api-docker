using Microsoft.EntityFrameworkCore;
using StudentApi.Data;
using StudentApi.Models;
using StudentApi.Services;

var builder = WebApplication.CreateBuilder(args);

// 1. Регистрация DbContext
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

// 2. Регистрация нашего сервиса в DI-контейнере 
builder.Services.AddScoped<IStudentService, StudentService>();

var app = builder.Build();

// 3. Инициализация базы данных при старте приложения
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.EnsureCreated(); // Автоматически создаст БД и таблицы, если их нет

    if (!db.Students.Any())
    {
        db.Students.AddRange(
            // Список отсортирован по алфавиту
            new Student { Name = "Горкин Дмитрий Алексеевич", Age = 20, Email = "d.gorkin@student.ru", AverageGrade = 4.7 },
            new Student { Name = "Иванов Иван Иванович", Age = 19, Email = "i.ivanov@student.ru", AverageGrade = 4.5 },
            new Student { Name = "Козочкин Игнат Сергеевич", Age = 21, Email = "i.kozochkin@student.ru", AverageGrade = 4.2 },
            new Student { Name = "Курочкин Василий Петрович", Age = 19, Email = "v.kurochkin@student.ru", AverageGrade = 3.9 },
            new Student { Name = "Морозова Анна Алексеевна", Age = 21, Email = "a.morozova@student.ru", AverageGrade = 4.6 },
            new Student { Name = "Опушкина Мария Павловна", Age = 20, Email = "m.opushkina@student.ru", AverageGrade = 4.8 },
            new Student { Name = "Петрова Зоя Фёдоровна", Age = 18, Email = "z.petrova@student.ru", AverageGrade = 4.0 }
        );
        db.SaveChanges();
    }
}

app.MapGet("/api/data", (IStudentService studentService) =>
{
    var students = studentService.GetAllStudents();
    return Results.Json(students);
});

app.MapGet("/api/config", (IConfiguration config) =>
{
    return Results.Json(new
    {
        AppName = config["AppName"],
        Version = config["Version"],
        MaxItems = config["MaxItems"]
    });
});

app.Run();