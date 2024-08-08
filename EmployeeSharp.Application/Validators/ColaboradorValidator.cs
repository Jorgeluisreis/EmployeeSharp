using EmployeeSharp.Domain.Entities;
using FluentValidation;

namespace EmployeeSharp.Application.Validators
{
    public class ColaboradorValidator : AbstractValidator<Colaborador>
    {
        public ColaboradorValidator()
        {
            RuleFor(colaborador => colaborador.Nome)
                .NotEmpty().WithMessage("O nome do colaborador é obrigatório.")
                .Length(5, 20).WithMessage("O nome do colaborador deve ter entre 5 e 20 caracteres.");

            RuleFor(colaborador => colaborador.Email)
                .NotEmpty().WithMessage("O email do colaborador é obrigatório.")
                .EmailAddress().WithMessage("O email do colaborador não está em um formato válido.");

            RuleFor(colaborador => colaborador.Telefone)
                .NotEmpty().WithMessage("O telefone do colaborador é obrigatório.")
                .Matches(@"^\d{11}$").WithMessage("O telefone do colaborador deve conter exatamente 11 números.");

            RuleFor(colaborador => colaborador.CargoId)
                .NotNull().WithMessage("Por favor, selecione um cargo válido.");
        }

    }
}
