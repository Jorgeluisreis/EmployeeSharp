using EmployeeSharp.Domain.Entities;
using EmployeeSharp.Domain.Interfaces;

namespace EmployeeSharp.Application.Services
{
    public class ColaboradorService
    {
        private readonly IColaboradorRepository _colaboradorRepository;
        private readonly ICargoRepository _cargoRepository;

        public ColaboradorService(IColaboradorRepository colaboradorRepository, ICargoRepository cargoRepository)
        {
            _colaboradorRepository = colaboradorRepository;
            _cargoRepository = cargoRepository;
        }

        public async Task<Colaborador> GetByIdAsync(int id)
        {
            return await _colaboradorRepository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<Colaborador>> GetAllAsync()
        {
            return await _colaboradorRepository.GetAllAsync();
        }

        public async Task<IEnumerable<Colaborador>> SearchByNameAsync(string nome)
        {
            return await _colaboradorRepository.SearchByNameAsync(nome);
        }

        public async Task AddAsync(Colaborador colaborador)
        {
            await _colaboradorRepository.AddAsync(colaborador);
        }

        public async Task UpdateAsync(Colaborador colaborador)
        {
            await _colaboradorRepository.UpdateAsync(colaborador);
        }

        public async Task DeleteAsync(int id)
        {
            await _colaboradorRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<Cargo>> GetCargosAsync()
        {
            return await _cargoRepository.GetAllAsync();
        }

        public async Task<bool> ColaboradorExists(int id)
        {
            var colaborador = await _colaboradorRepository.GetByIdAsync(id);
            return colaborador != null;
        }

        public async Task<IEnumerable<Colaborador>> GetByCargoIdAsync(int cargoId)
        {
            return await _colaboradorRepository.GetByCargoIdAsync(cargoId);
        }
    }
}
