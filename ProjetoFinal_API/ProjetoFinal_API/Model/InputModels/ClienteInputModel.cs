namespace ProjetoFinal_API.Model.InputModels
{
    public class ClienteInputModel
    {
        public string Nome { get; set; }
        public string CPF { get; set; }
        public string Endereco { get; set; }
        public string Telefone { get; set; }
        public DateOnly Nasc { get; set; }
        public string Email { get; set; }
    }
}
