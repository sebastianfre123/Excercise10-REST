

using Microsoft.EntityFrameworkCore;
using MySql.EntityFrameworkCore;
using Registration_Service;

HttpClient httpClient = new HttpClient();

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySQL("Server=localhost;Port=3306;Database=Registration;User Id=root;Password=123mysql;"));
builder.Services.AddControllers(); // Add this line to enable controllers
var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.MapControllers(); // Map controllers

app.Run();