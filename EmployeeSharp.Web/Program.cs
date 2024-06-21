using EmployeeSharp.Infra.Data;
using EmployeeSharp.Domain.Interfaces;
using EmployeeSharp.Infra.Data.Repositories;
using EmployeeSharp.Application.Services;
using Microsoft.EntityFrameworkCore;
using EmployeeSharp.Application.Validators;
using FluentValidation.AspNetCore;
using FluentValidation;

var builder = WebApplication.CreateBuilder(args);

var env = builder.Environment;
var isDevelopment = env.IsDevelopment();

if (isDevelopment)
{
    builder.Logging.AddConsole();
}

builder.Services.AddRazorPages();

var config = new ConfigurationBuilder()
    .SetBasePath(builder.Environment.ContentRootPath)
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
    .AddEnvironmentVariables()
    .AddUserSecrets<Program>()
    .Build();

var connectionString = config["ConnectionStrings:DefaultConnection"];
if (string.IsNullOrEmpty(connectionString))
{
    throw new InvalidOperationException("A string de conexão 'DefaultConnection' não foi encontrada.");
}

builder.Services.AddDbContext<EmployeeSharpContext>(options =>
    options.UseMySql(connectionString, new MySqlServerVersion(new Version(8, 0, 0))));

builder.Services.AddScoped<IColaboradorRepository, ColaboradorRepository>();
builder.Services.AddScoped<ICargoRepository, CargoRepository>();
builder.Services.AddScoped<ColaboradorService>();
builder.Services.AddScoped<CargoService>();

builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddFluentValidationClientsideAdapters();
builder.Services.AddTransient<ColaboradorValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<CargoValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<ColaboradorValidator>();


builder.WebHost.ConfigureKestrel(serverOptions =>
{
    if (builder.Environment.IsDevelopment())
    {
        serverOptions.ListenAnyIP(5000);
        serverOptions.ListenAnyIP(5001, listenOptions =>
        {
            listenOptions.UseHttps();
        });
    }
    else
    {
        serverOptions.ListenAnyIP(2506);
    }
});

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Colaboradores}/{action=Index}/{id?}");

app.MapRazorPages();

app.Run();