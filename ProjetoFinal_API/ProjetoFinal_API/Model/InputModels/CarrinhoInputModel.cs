using System.Security.Cryptography.Pkcs;

namespace ProjetoFinal_API.Model.InputModels
{
    public class CarrinhoInputModel
    {
        public Guid ClienteId { get; set; }
        public string? Observacoes { get; set; }
    }
}
