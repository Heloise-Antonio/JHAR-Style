namespace ProjetoFinal_API.Model.InputModels
{
    public class PagamentoInputModel
    {
        public Guid MetodoPagamentoId { get; set; }
        public Guid CarrinhoId { get; set; }
        public decimal Valor {  get; set; }
    }
}
