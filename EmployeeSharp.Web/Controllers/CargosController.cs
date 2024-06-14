using Microsoft.AspNetCore.Mvc;
using EmployeeSharp.Application.Services;
using EmployeeSharp.Domain.Entities;
using EmployeeSharp.Web.Models;
using System.Threading.Tasks;

namespace EmployeeSharp.Web.Controllers
{
    public class CargosController : Controller
    {
        private readonly CargoService _cargoService;

        public CargosController(CargoService cargoService)
        {
            _cargoService = cargoService;
        }

        public async Task<IActionResult> Index()
        {
            var cargos = await _cargoService.GetAllAsync();
            return View(cargos);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Cargo cargo)
        {
            if (ModelState.IsValid)
            {
                await _cargoService.AddAsync(cargo);
                return RedirectToAction(nameof(Index));
            }
            return View(cargo);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var cargo = await _cargoService.GetByIdAsync(id);
            if (cargo == null)
            {
                return NotFound();
            }
            return View(cargo);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Cargo cargo)
        {
            if (id != cargo.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _cargoService.UpdateAsync(cargo);
                return RedirectToAction(nameof(Index));
            }
            return View(cargo);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var cargo = await _cargoService.GetByIdAsync(id);
            if (cargo == null)
            {
                return NotFound();
            }
            return View(cargo);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _cargoService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
