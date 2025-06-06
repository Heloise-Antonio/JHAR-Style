namespace ProjetoFinal_API.Model.ViewModels
{
    public class CarrinhoViewModel
    {
        public Guid CarrinhoId { get; set; }
        public Guid ClienteId { get; set; }
        public string? Cliente { get; set; }
        public DateTime DataHora { get; set; }
        public string? Observacoes { get; set; }
    }
}
