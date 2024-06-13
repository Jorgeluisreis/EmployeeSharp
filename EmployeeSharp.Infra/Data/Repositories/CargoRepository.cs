using EmployeeSharp.Domain.Entities;
using EmployeeSharp.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmployeeSharp.Infra.Data.Repositories
{
    public class CargoRepository : ICargoRepository
    {
        private readonly EmployeeSharpContext _context;

        public CargoRepository(EmployeeSharpContext context)
        {
            _context = context;
        }

        public async Task<Cargo> GetByIdAsync(int id)
        {
            return await _context.Cargos.FindAsync(id);
        }

        public async Task<IEnumerable<Cargo>> GetAllAsync()
        {
            return await _context.Cargos.ToListAsync();
        }

        public async Task<Cargo> GetByNameAsync(string nome)
        {
            return await _context.Cargos.FirstOrDefaultAsync(c => c.Nome == nome);
        }

        public async Task AddAsync(Cargo cargo)
        {
            _context.Cargos.Add(cargo);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var cargo = await _context.Cargos.FindAsync(id);
            if (cargo != null)
            {
                _context.Cargos.Remove(cargo);
                await _context.SaveChangesAsync();
            }
        }

        public async Task UpdateAsync(Cargo cargo)
        {
            _context.Entry(cargo).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
