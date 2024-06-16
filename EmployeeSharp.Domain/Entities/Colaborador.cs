namespace EmployeeSharp.Domain.Entities
{
    public class Colaborador
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }
        public int? CargoId { get; set; }
        public Cargo Cargo { get; set; }
    }
}