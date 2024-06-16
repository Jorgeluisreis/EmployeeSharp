using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using EmployeeSharp.Domain.Entities;
using System.ComponentModel.DataAnnotations.Schema;

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
        public int? CargoId { get; set; }

        public IEnumerable<Cargo> Cargos { get; set; }
    }
}