using Microsoft.AspNetCore.Mvc;
using EmployeeSharp.Application.Services;
using EmployeeSharp.Domain.Entities;
using EmployeeSharp.Web.Models;
using EmployeeSharp.Domain.Interfaces;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace EmployeeSharp.Web.Controllers
{
    public class ColaboradoresController : Controller
    {
        private readonly ColaboradorService _colaboradorService;
        private readonly ICargoRepository _cargoRepository;

        public ColaboradoresController(ColaboradorService colaboradorService, ICargoRepository cargoRepository)
        {
            _colaboradorService = colaboradorService;
            _cargoRepository = cargoRepository;
        }

        // GET: /Colaboradores/Index
        public async Task<IActionResult> Index(string searchString)
        {
            var colaboradores = await _colaboradorService.GetAllAsync();

            if (!string.IsNullOrEmpty(searchString))
            {
                colaboradores = colaboradores.Where(c => c.Nome.Contains(searchString));
            }

            return View(colaboradores);
        }

        // GET: /Colaboradores/Create
        public async Task<IActionResult> Create()
        {
            var cargos = await _cargoRepository.GetAllAsync();
            var viewModel = new ColaboradorViewModel
            {
                Cargos = cargos
            };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ColaboradorViewModel colaboradorViewModel)
        {
            colaboradorViewModel.Cargos = await _cargoRepository.GetAllAsync();
            var cargo = colaboradorViewModel.Cargos.FirstOrDefault(c => c.Id == colaboradorViewModel.CargoId);

            ModelState.Remove("Cargos");
            if (ModelState.IsValid)
            {
                if (cargo == null)
                {
                    ModelState.AddModelError("CargoId", "Cargo não encontrado.");
                    colaboradorViewModel.Cargos = await _cargoRepository.GetAllAsync();
                    return View(colaboradorViewModel);
                }

                var colaborador = new Colaborador
                {
                    Nome = colaboradorViewModel.Nome,
                    Email = colaboradorViewModel.Email,
                    Telefone = colaboradorViewModel.Telefone,
                    Cargo = cargo
                };

                await _colaboradorService.AddAsync(colaborador);
                return RedirectToAction(nameof(Index));
            }
            return View(colaboradorViewModel);
        }

        // GET: /Colaboradores/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var colaborador = await _colaboradorService.GetByIdAsync(id);
            if (colaborador == null)
            {
                return NotFound();
            }

            var cargos = await _cargoRepository.GetAllAsync();
            var viewModel = new ColaboradorViewModel
            {
                Id = colaborador.Id,
                Nome = colaborador.Nome,
                Email = colaborador.Email,
                Telefone = colaborador.Telefone,
                CargoId = colaborador.CargoId,
                Cargos = cargos
            };

            return PartialView("Edit", viewModel);
        }

        // POST: /Colaboradores/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ColaboradorViewModel viewModel)
        {
            if (id != viewModel.Id)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var cargo = await _cargoRepository.GetByIdAsync(viewModel.CargoId);
                    if (cargo == null)
                    {
                        ModelState.AddModelError("CargoId", "Cargo não encontrado.");
                        return PartialView("Edit", viewModel);
                    }

                    var colaborador = new Colaborador
                    {
                        Id = viewModel.Id,
                        Nome = viewModel.Nome,
                        Email = viewModel.Email,
                        Telefone = viewModel.Telefone,
                        Cargo = cargo
                    };

                    await _colaboradorService.UpdateAsync(colaborador);
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await _colaboradorService.ColaboradorExists(viewModel.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }

            viewModel.Cargos = await _cargoRepository.GetAllAsync();
            return PartialView("Edit", viewModel);
        }

        // GET: /Colaboradores/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var colaborador = await _colaboradorService.GetByIdAsync(id);
            if (colaborador == null)
            {
                return NotFound();
            }
            return View(colaborador);
        }

        // POST: /Colaboradores/DeleteConfirmed
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var colaborador = await _colaboradorService.GetByIdAsync(id);
            if (colaborador == null)
            {
                return NotFound();
            }

            await _colaboradorService.DeleteAsync(id);
            return Json(new { success = true });
        }
    }
}