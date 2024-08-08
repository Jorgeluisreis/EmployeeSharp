using Microsoft.EntityFrameworkCore;
using EmployeeSharp.Domain.Entities;

namespace EmployeeSharp.Infra.Data
{
    public class EmployeeSharpContext : DbContext
    {
        public EmployeeSharpContext(DbContextOptions<EmployeeSharpContext> options) : base(options) { }

        public DbSet<Colaborador> Colaboradores { get; set; }
        public DbSet<Cargo> Cargos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Colaborador>()
                .HasOne(c => c.Cargo)
                .WithMany()
                .HasForeignKey(c => c.CargoId)
                .OnDelete(DeleteBehavior.SetNull);

            base.OnModelCreating(modelBuilder);
        }
    }
}
