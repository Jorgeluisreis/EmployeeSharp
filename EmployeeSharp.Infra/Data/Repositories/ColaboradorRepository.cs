using EmployeeSharp.Domain.Entities;
using EmployeeSharp.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EmployeeSharp.Infra.Data.Repositories
{
    public class ColaboradorRepository : IColaboradorRepository
    {
        private readonly EmployeeSharpContext _context;

        public ColaboradorRepository(EmployeeSharpContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Colaborador>> GetAllAsync()
        {
            return await _context.Colaboradores.ToListAsync();
        }

        public async Task<Colaborador> GetByIdAsync(int id)
        {
            return await _context.Colaboradores.FindAsync(id);
        }

        public async Task<IEnumerable<Colaborador>> SearchByNameAsync(string nome)
        {
            return await _context.Colaboradores.Where(c => c.Nome.Contains(nome)).ToListAsync();
        }

        public async Task AddAsync(Colaborador colaborador)
        {
            _context.Colaboradores.Add(colaborador);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Colaborador colaborador)
        {
            _context.Entry(colaborador).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var colaborador = await _context.Colaboradores.FindAsync(id);
            if (colaborador != null)
            {
                _context.Colaboradores.Remove(colaborador);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Colaborador>> GetByCargoIdAsync(int cargoId)
        {
            return await _context.Colaboradores.Where(c => c.CargoId == cargoId).ToListAsync();
        }
    }
}