using EmployeeSharp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeSharp.Domain.Interfaces
{
    public interface ICargoRepository
    {
        Task<bool> AnyAsync();
        Task<IEnumerable<Cargo>> GetAllAsync();
        Task<Cargo> GetByIdAsync(int id);
        Task AddAsync(Cargo cargo);
        Task UpdateAsync(Cargo cargo);
        Task DeleteAsync(int id);
        Task<Cargo> GetByNameAsync(string name);
    }
}
