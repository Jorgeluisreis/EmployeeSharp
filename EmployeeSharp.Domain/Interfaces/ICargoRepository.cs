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
        Task<Cargo> GetByIdAsync(int id);
        Task<Cargo> GetByNameAsync(string name);
        Task<IEnumerable<Cargo>> GetAllAsync();
        Task AddAsync(Cargo entity);
        Task UpdateAsync(Cargo entity);
        Task DeleteAsync(int id);
    }
}
