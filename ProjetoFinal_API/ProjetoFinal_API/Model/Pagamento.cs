namespace ProjetoFinal_API.Model
{
    public class Pagamento
    {
        public Guid PagamentoId { get; set; }
        public DateTime DataHora { get; set; }
        public Guid CarrinhoId { get; set; }
        public Carrinho? Carrinho { get; set; }
        public decimal Valor {  get; set; }
        public Guid MetodoPagamentoId { get; set; }
        public MetodoPagamento? MetodoPagamento { get; set; }
   
    }
}
