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
        Task<IEnumerable<Colaborador>> GetAllAsync();
        Task<Colaborador> GetByIdAsync(int id);
        Task<IEnumerable<Colaborador>> SearchByNameAsync(string nome);
        Task AddAsync(Colaborador colaborador);
        Task UpdateAsync(Colaborador colaborador);
        Task DeleteAsync(int id);
        Task<IEnumerable<Colaborador>> GetByCargoIdAsync(int cargoId);
    }
}