namespace ProjetoFinal_API.Model
{
    public class Cliente
    {
        public Guid ClienteId { get; set; }
        public string Nome { get; set; }
        public string CPF { get; set; }
        public string Endereco { get; set; }
        public string Telefone { get; set; }
        public DateOnly Nasc {  get; set; }
        public string Email { get; set; }
    }
}
