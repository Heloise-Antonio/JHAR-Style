namespace ProjetoFinal_API.Model.InputModels
{
    public class ProdutoInputModel
    {
        public string nome { get; set; }
        public string descricao { get; set; }
        public decimal preco { get; set; }
        public int estoque { get; set; }
        public Guid CategoriaId { get; set; }

    }
}
