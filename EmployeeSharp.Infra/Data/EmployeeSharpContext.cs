using Microsoft.EntityFrameworkCore;
using EmployeeSharp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeSharp.Infra.Data
{
    public class EmployeeSharpContext : DbContext
    {
        public EmployeeSharpContext(DbContextOptions options) : base(options) { }

        public DbSet<Colaborador> Colaboradores { get; set; }
        public DbSet<Cargo> Cargos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cargo>().HasData(
                new Cargo { Id = 1, Nome = "Desenvolvedor" },
                new Cargo { Id = 2, Nome = "Analista" },
                new Cargo { Id = 3, Nome = "Gerente" }
            );
        }
    }
}
