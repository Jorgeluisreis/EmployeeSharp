using Microsoft.AspNetCore.Mvc;
using EmployeeSharp.Application.Services;
using EmployeeSharp.Domain.Entities;
using EmployeeSharp.Web.Models;
using EmployeeSharp.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using EmployeeSharp.Application.Validators;
using FluentValidation;

namespace EmployeeSharp.Web.Controllers
{
    public class ColaboradoresController : Controller
    {
        private readonly ColaboradorService _colaboradorService;
        private readonly ICargoRepository _cargoRepository;
        private readonly ColaboradorValidator _colaboradorValidator;

        public ColaboradoresController(ColaboradorService colaboradorService, ICargoRepository cargoRepository, ColaboradorValidator colaboradorValidator)
        {
            _colaboradorService = colaboradorService;
            _cargoRepository = cargoRepository;
            _colaboradorValidator = colaboradorValidator;
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
            return PartialView("Create", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ColaboradorViewModel colaboradorViewModel)
        {

            colaboradorViewModel.Cargos = await _cargoRepository.GetAllAsync();

            if (ModelState.IsValid)
            {

                var colaborador = new Colaborador
                {
                    Nome = colaboradorViewModel.Nome,
                    Email = colaboradorViewModel.Email,
                    Telefone = colaboradorViewModel.Telefone,
                    CargoId = colaboradorViewModel.CargoId
                };

                var validationResult = await _colaboradorValidator.ValidateAsync(colaborador);

                if (!validationResult.IsValid)
                {

                    foreach (var error in validationResult.Errors)
                    {
                        ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                    }

                    return PartialView("Create", colaboradorViewModel);
                }

                await _colaboradorService.AddAsync(colaborador);
                return Json(new { success = true });
            }

            return PartialView("Create", colaboradorViewModel);
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

            viewModel.Cargos = await _cargoRepository.GetAllAsync();


            var colaborador = new Colaborador
            {
                Id = viewModel.Id,
                Nome = viewModel.Nome,
                Email = viewModel.Email,
                Telefone = viewModel.Telefone,
                CargoId = viewModel.CargoId ?? 0
            };

            var validationResult = await _colaboradorValidator.ValidateAsync(colaborador);
            if (!validationResult.IsValid)
            {
                foreach (var error in validationResult.Errors)
                {
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                }
            }

            if (!ModelState.IsValid)
            {
                // Preencha a lista de cargos novamente para a View
                viewModel.Cargos = await _cargoRepository.GetAllAsync();
                return PartialView("Edit", viewModel);
            }

            try
            {
                var cargo = viewModel.CargoId.HasValue
                            ? await _cargoRepository.GetByIdAsync(viewModel.CargoId.Value)
                            : null;

                if (viewModel.CargoId.HasValue && cargo == null)
                {
                    ModelState.AddModelError("CargoId", "Cargo não encontrado.");
                    viewModel.Cargos = await _cargoRepository.GetAllAsync();
                    return PartialView("Edit", viewModel);
                }

                await _colaboradorService.UpdateAsync(colaborador);
                return Json(new { success = true });
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



        // GET: /Colaboradores/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var colaborador = await _colaboradorService.GetByIdAsync(id);
            if (colaborador == null)
            {
                return NotFound();
            }
            return PartialView("Delete", colaborador);
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