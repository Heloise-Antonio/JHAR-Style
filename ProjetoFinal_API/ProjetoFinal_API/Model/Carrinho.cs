namespace ProjetoFinal_API.Model
{
    public class Carrinho
    {
        public Guid CarrinhoId { get; set; }
        public Guid ClienteId {  get; set; } 
        public Cliente? Cliente { get; set; }
        public DateTime DataHora { get; set; }
        public string? Observacoes { get; set; }
        public IEnumerable<ItemCarrinho>? Itens { get; set; }
    }
}
