using Microsoft.AspNetCore.Mvc;

namespace EmployeeSharp.Web.Models
{
    public class ColaboradorViewModel
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }
        public string Cargo { get; set; }
    }
}
