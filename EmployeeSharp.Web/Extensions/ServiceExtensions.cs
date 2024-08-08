using EmployeeSharp.Infra.Data;
using EmployeeSharp.Domain.Interfaces;
using EmployeeSharp.Infra.Data.Repositories;
using EmployeeSharp.Application.Services;
using EmployeeSharp.Application.Validators;
using FluentValidation.AspNetCore;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace EmployeeSharp.Web.Extensions
{
    public static class ServiceExtensions
    {
        public static IHostApplicationBuilder AddEmployeeSharpServices(this IHostApplicationBuilder builder)
        {
            var connectionString = builder.Configuration["ConnectionStrings:DefaultConnection"];
            if (string.IsNullOrEmpty(connectionString))
            {
                throw new InvalidOperationException("A string de conexão 'DefaultConnection' não foi encontrada.");
            }

            builder.Services.AddDbContext<EmployeeSharpContext>(options =>
                options.UseMySql(connectionString, new MySqlServerVersion(new Version(9, 0, 1))));

            builder.Services.AddScoped<IColaboradorRepository, ColaboradorRepository>();
            builder.Services.AddScoped<ICargoRepository, CargoRepository>();
            builder.Services.AddScoped<ColaboradorService>();
            builder.Services.AddScoped<CargoService>();

            builder.Services.AddFluentValidationAutoValidation();
            builder.Services.AddFluentValidationClientsideAdapters();
            builder.Services.AddTransient<ColaboradorValidator>();
            builder.Services.AddValidatorsFromAssemblyContaining<CargoValidator>();
            builder.Services.AddValidatorsFromAssemblyContaining<ColaboradorValidator>();

            return builder;
        }
    }
}