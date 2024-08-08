using EmployeeSharp.Domain.Entities;
using EmployeeSharp.Domain.Interfaces;

namespace EmployeeSharp.Application.Services
{
    public class CargoService
    {
        private readonly ICargoRepository _cargoRepository;

        public CargoService(ICargoRepository cargoRepository)
        {
            _cargoRepository = cargoRepository;
        }

        public async Task<IEnumerable<Cargo>> GetAllAsync()
        {
            return await _cargoRepository.GetAllAsync();
        }

        public async Task<Cargo> GetByIdAsync(int id)
        {
            return await _cargoRepository.GetByIdAsync(id);
        }

        public async Task AddAsync(Cargo cargo)
        {
            await _cargoRepository.AddAsync(cargo);
        }

        public async Task UpdateAsync(Cargo cargo)
        {
            await _cargoRepository.UpdateAsync(cargo);
        }

        public async Task DeleteAsync(int id)
        {
            await _cargoRepository.DeleteAsync(id);
        }

        public async Task<Cargo> GetByNameAsync(string name)
        {
            return await _cargoRepository.GetByNameAsync(name);
        }
    }
}

