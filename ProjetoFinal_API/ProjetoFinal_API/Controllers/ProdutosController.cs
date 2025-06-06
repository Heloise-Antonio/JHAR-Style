using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjetoFinal_API.Data;
using ProjetoFinal_API.Model;
using ProjetoFinal_API.Model.InputModels;
using ProjetoFinal_API.Model.ViewModels;

namespace ProjetoFinal_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProdutosController : ControllerBase
    {
        private readonly Context _context;

        public ProdutosController(Context context)
        {
            _context = context;
        }

        // GET: api/Produtoes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProdutoViewModel>>> GetProduto()
        {
            var prod = await _context.Produtos.Include(p => p.Categoria).ToListAsync();
            List<ProdutoViewModel> lista = new List<ProdutoViewModel>();

            foreach (var item in prod)
            {
                ProdutoViewModel p = new ProdutoViewModel();
                p.ProdutoId = item.ProdutoId;
                p.CategoriaId = item.CategoriaId;
                p.descricao = item.descricao;
                p.preco = item.preco;
                p.estoque = item.estoque;
                p.nome = item.nome;
                p.Categoria = item.Categoria.Nome;
                lista.Add(p);
            }
            return lista;
        }

        // GET: api/Produtoes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Produto>> GetProduto(Guid id)
        {
            var produto = await _context.Produtos.FindAsync(id);

            if (produto == null)
            {
                return NotFound();
            }

            return produto;
        }

        // PUT: api/Produtoes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduto(Guid id, Produto produto)
        {
            if (id != produto.ProdutoId)
            {
                return BadRequest();
            }

            _context.Entry(produto).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProdutoExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Produtoes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ProdutoViewModel>> PostProduto(ProdutoInputModel input)
        {
            var produto = new Produto
            {
                ProdutoId = Guid.NewGuid(),
                nome = input.nome,
                descricao = input.descricao,
                preco = input.preco,
                estoque = input.estoque,
                CategoriaId = input.CategoriaId,

            };
            _context.Produtos.Add(produto);
            await _context.SaveChangesAsync();

            var categ = await _context.Categorias.FirstOrDefaultAsync(c => c.CategoriaId == produto.CategoriaId);

            ProdutoViewModel p = new ProdutoViewModel
            {
               ProdutoId = produto.ProdutoId,
               CategoriaId = produto.CategoriaId,
               Categoria = categ.Nome,
               descricao = produto.descricao,
               nome = produto.nome,
               estoque = produto.estoque,
               preco = produto.preco
            };

            return Ok(p);
        }

        // DELETE: api/Produtoes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduto(Guid id)
        {
            var produto = await _context.Produtos.FindAsync(id);
            if (produto == null)
            {
                return NotFound();
            }

            _context.Produtos.Remove(produto);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProdutoExists(Guid id)
        {
            return _context.Produtos.Any(e => e.ProdutoId == id);
        }
    }
}
