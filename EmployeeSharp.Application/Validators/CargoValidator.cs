using EmployeeSharp.Domain.Entities;
using FluentValidation;

namespace EmployeeSharp.Application.Validators
{
    public class CargoValidator : AbstractValidator<Cargo>
    {
        public CargoValidator()
        {
            RuleFor(cargo => cargo.Nome)
                .NotEmpty().WithMessage("O nome do cargo é obrigatório.")
                .Length(3, 50).WithMessage("O nome do cargo deve ter entre 3 e 50 caracteres.");
        }
    }
}
