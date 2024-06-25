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
        public static IServiceCollection ConfigureServices(this IServiceCollection services, IConfiguration config)
        {
            var connectionString = config["ConnectionStrings:DefaultConnection"];
            if (string.IsNullOrEmpty(connectionString))
            {
                throw new InvalidOperationException("A string de conexão 'DefaultConnection' não foi encontrada.");
            }

            services.AddDbContext<EmployeeSharpContext>(options =>
                options.UseMySql(connectionString, new MySqlServerVersion(new Version(8, 0, 0))));

            services.AddScoped<IColaboradorRepository, ColaboradorRepository>();
            services.AddScoped<ICargoRepository, CargoRepository>();
            services.AddScoped<ColaboradorService>();
            services.AddScoped<CargoService>();

            services.AddFluentValidationAutoValidation();
            services.AddFluentValidationClientsideAdapters();
            services.AddTransient<ColaboradorValidator>();
            services.AddValidatorsFromAssemblyContaining<CargoValidator>();
            services.AddValidatorsFromAssemblyContaining<ColaboradorValidator>();

            return services;
        }
    }
}