using EmployeeSharp.Infra.Data;
using EmployeeSharp.Domain.Interfaces;
using EmployeeSharp.Infra.Data.Repositories;
using EmployeeSharp.Application.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using System;

var builder = WebApplication.CreateBuilder(args);

// Configurar o ambiente
var env = builder.Environment;
var isDevelopment = env.IsDevelopment();

if (isDevelopment)
{
    // Adicionar serviços específicos do desenvolvimento
    builder.Logging.AddConsole();
}

// Adicionar serviços para Razor Pages
builder.Services.AddRazorPages();

// Carregar a configuração
var config = new ConfigurationBuilder()
    .SetBasePath(builder.Environment.ContentRootPath)
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
    .AddEnvironmentVariables()
    .AddUserSecrets<Program>() // Adiciona os segredos do usuário
    .Build();

// Obter a string de conexão
var connectionString = config["ConnectionStrings:DefaultConnection"];
if (string.IsNullOrEmpty(connectionString))
{
    throw new InvalidOperationException("A string de conexão 'DefaultConnection' não foi encontrada.");
}

// Configurar o contexto do banco de dados
builder.Services.AddDbContext<EmployeeSharpContext>(options =>
    options.UseMySql(connectionString, new MySqlServerVersion(new Version(8, 0, 0))));

// Registrar os repositórios e serviços
builder.Services.AddScoped<IColaboradorRepository, ColaboradorRepository>();
builder.Services.AddScoped<ICargoRepository, CargoRepository>();
builder.Services.AddScoped<ColaboradorService>();
builder.Services.AddScoped<CargoService>();

var app = builder.Build();

// Configurar o pipeline de solicitação HTTP
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication(); // Adicione esta linha
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Colaboradores}/{action=Index}/{id?}");

// Mapear as Razor Pages
app.MapRazorPages();

// Iniciar o aplicativo
app.Run();