using Microsoft.AspNetCore.Mvc;
using EmployeeSharp.Application.Services;
using EmployeeSharp.Domain.Entities;

namespace EmployeeSharp.Web.Controllers
{
    public class CargosController : Controller
    {
        private readonly CargoService _cargoService;
        private readonly ColaboradorService _colaboradorService;

        public CargosController(CargoService cargoService, ColaboradorService colaboradorService)
        {
            _cargoService = cargoService;
            _colaboradorService = colaboradorService;
        }

        [HttpGet]
        [Route("Cargos/GetCargoName/{id}")]
        public async Task<IActionResult> GetCargoName(int id)
        {
            try
            {
                var cargo = await _cargoService.GetByIdAsync(id);
                if (cargo != null)
                {
                    return Ok(cargo.Nome);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno do servidor: {ex.Message}");
            }
        }

        public async Task<IActionResult> Index()
        {
            var cargos = await _cargoService.GetAllAsync();
            return View(cargos);
        }

        public IActionResult Create()
        {
            return PartialView();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Cargo cargo)
        {
            if (ModelState.IsValid)
            {
                await _cargoService.AddAsync(cargo);
                return Json(new { success = true });
            }
            return PartialView(cargo);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var cargo = await _cargoService.GetByIdAsync(id);
            if (cargo == null)
            {
                return NotFound();
            }
            return PartialView("Edit", cargo);
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
                return Json(new { success = true });
            }
            return PartialView(cargo);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var cargo = await _cargoService.GetByIdAsync(id);
            if (cargo == null)
            {
                return NotFound();
            }
            return PartialView(cargo);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var colaboradores = await _colaboradorService.GetByCargoIdAsync(id);
                foreach (var colaborador in colaboradores)
                {
                    colaborador.CargoId = null;
                    await _colaboradorService.UpdateAsync(colaborador);
                }

                await _cargoService.DeleteAsync(id);
                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Erro ao deletar o cargo: {ex.Message}");
                return Json(new { success = false, message = $"Erro ao deletar o cargo: {ex.Message}" });
            }
        }
    }
}