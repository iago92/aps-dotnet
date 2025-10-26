using Microsoft.EntityFrameworkCore;
using University.Infrastructure.Data;
using University.Infrastructure.Repositories;
using University.Application.Interfaces;
using University.Application.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Configure DbContext (SQL Server) - connection string in appsettings.json
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? "Server=localhost,1433;Database=UniversityDb;User Id=sa;Password=Your_password123;";

builder.Services.AddDbContext<UniversityDbContext>(options =>
    options.UseSqlServer(connectionString));

// DI registrations (IoC)
builder.Services.AddScoped<IStudentService, StudentService>();
builder.Services.AddScoped<University.Domain.Repositories.IStudentRepository, StudentRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
