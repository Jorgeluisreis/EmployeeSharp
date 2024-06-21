using System.ComponentModel.DataAnnotations;
using EmployeeSharp.Domain.Entities;

namespace EmployeeSharp.Web.Models
{
    public class ColaboradorViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O campo Nome é obrigatório.")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O campo Email é obrigatório.")]
        [EmailAddress(ErrorMessage = "O Email não é válido.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "O campo Telefone é obrigatório.")]
        [Phone(ErrorMessage = "O Telefone não é válido.")]
        public string Telefone { get; set; }

        [Display(Name = "Cargo")]
        [Required(ErrorMessage = "O campo Cargo é obrigatório.")]
        public int? CargoId { get; set; }

        public IEnumerable<Cargo> Cargos { get; set; }
    }
}