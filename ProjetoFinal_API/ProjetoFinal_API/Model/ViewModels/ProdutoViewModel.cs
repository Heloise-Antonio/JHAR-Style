namespace ProjetoFinal_API.Model.ViewModels
{
    public class ProdutoViewModel
    {
        public Guid ProdutoId { get; set; }
        public string nome { get; set; }
        public string descricao { get; set; }
        public decimal preco { get; set; }
        public int estoque { get; set; }
        public Guid CategoriaId { get; set; }  
        public string? Categoria { get; set; }
       
    }
}
