using EmployeeSharp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeSharp.Domain.Interfaces
{
    public interface IColaboradorRepository
    {
        Task<Colaborador> GetByIdAsync(int id);
        Task<IEnumerable<Colaborador>> GetAllAsync();
        Task<IEnumerable<Colaborador>> SearchByNameAsync(string nome);
        Task AddAsync(Colaborador colaborador);
        Task UpdateAsync(Colaborador colaborador);
        Task DeleteAsync(int id);
    }
}