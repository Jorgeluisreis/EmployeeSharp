using System.ComponentModel.DataAnnotations;
using EmployeeSharp.Domain.Entities;

namespace EmployeeSharp.Web.Models
{
    public class ColaboradorViewModel
    {
        public int Id { get; set; }

        public string Nome { get; set; }

        public string Email { get; set; }
        public string Telefone { get; set; }

        [Display(Name = "Cargo")]
        public int? CargoId { get; set; }

        public IEnumerable<Cargo> Cargos { get; set; }
    }
}