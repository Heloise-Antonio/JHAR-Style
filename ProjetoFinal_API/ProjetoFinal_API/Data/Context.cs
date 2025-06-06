using Microsoft.EntityFrameworkCore;
using ProjetoFinal_API.Model;

namespace ProjetoFinal_API.Data
   
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options) : base(options) { }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Produto> Produtos { get; set; }
        public DbSet<Carrinho> Carrinhos { get; set; }
        public DbSet<ItemCarrinho> ItensCarrinho { get; set; }
        public DbSet<Pagamento> Pagamentos { get; set; }
        public DbSet<MetodoPagamento> MetodosPagamento { get; set; }
        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cliente>().ToTable("tbClientes");

            modelBuilder.Entity<Categoria>().ToTable("tbCategoria");

            modelBuilder.Entity<Produto>().ToTable("tbProduto");

            modelBuilder.Entity<Carrinho>().ToTable("tbCarrinho");

            modelBuilder.Entity<ItemCarrinho>().ToTable("tbItemCarrinho");

            modelBuilder.Entity<Pagamento>().ToTable("tbPagamento");

            modelBuilder.Entity<MetodoPagamento>().ToTable("tbMetodoPagamento");
        }
        

 
    }
    
}
