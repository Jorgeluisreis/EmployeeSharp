using EmployeeSharp.Domain.Entities;
using EmployeeSharp.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeSharp.Infra.Data.Repositories
{
    public class ColaboradorRepository : IColaboradorRepository
    {
        private readonly EmployeeSharpContext _context;

        public ColaboradorRepository(EmployeeSharpContext context)
        {
            _context = context;
        }

        public async Task<Colaborador> GetByIdAsync(int id)
        {
            return await _context.Colaboradores.Include(c => c.Cargo).FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<IEnumerable<Colaborador>> GetAllAsync()
        {
            return await _context.Colaboradores.Include(c => c.Cargo).ToListAsync();
        }

        public async Task<IEnumerable<Colaborador>> SearchByNameAsync(string nome)
        {
            return await _context.Colaboradores.Include(c => c.Cargo)
                .Where(c => c.Nome.Contains(nome)).ToListAsync();
        }

        public async Task AddAsync(Colaborador colaborador)
        {
            await _context.Colaboradores.AddAsync(colaborador);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Colaborador colaborador)
        {
            _context.Colaboradores.Update(colaborador);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var colaborador = await GetByIdAsync(id);
            _context.Colaboradores.Remove(colaborador);
            await _context.SaveChangesAsync();
        }
    }
}
