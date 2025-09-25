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
    public class CategoriasController : ControllerBase
    {
        private readonly Context _context;

        public CategoriasController(Context context)
        {
            _context = context;
        }

        // GET: api/Categorias
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoriaViewModel>>> GetCategoria()
        {
            var dados = await _context.Categorias.ToListAsync();
            List<CategoriaViewModel> lista = new List<CategoriaViewModel>();

            foreach (var d in dados)
            {
                CategoriaViewModel cat = new CategoriaViewModel();
                cat.CategoriaId = d.CategoriaId;
                cat.Nome = d.Nome;

                lista.Add(cat);
            }
            return lista;
        }

        // GET: api/Categorias/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Categoria>> GetCategoria(Guid id)
        {
            var categoria = await _context.Categorias.FindAsync(id);

            if (categoria == null)
            {
                return NotFound();
            }

            return categoria;
        }

        // PUT: api/Categorias/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCategoria(Guid id, Categoria categoria)
        {
            if (id != categoria.CategoriaId)
            {
                return BadRequest();
            }

            _context.Entry(categoria).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CategoriaExists(id))
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

        // POST: api/Categorias
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CategoriaViewModel>> PostCategoria(CategoriaInputModel input)
        {
            var categoria = new Categoria
            {
                CategoriaId = Guid.NewGuid(),
                Nome = input.nome
            };
            _context.Categorias.Add(categoria);
            await _context.SaveChangesAsync();

            CategoriaViewModel c = new CategoriaViewModel
            {
                CategoriaId = categoria.CategoriaId,
                Nome = categoria.Nome
            };

            return Ok(c);
        }

        // DELETE: api/Categorias/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategoria(Guid id)
        {
            var categoria = await _context.Categorias.FindAsync(id);
            if (categoria == null)
            {
                return NotFound();
            }

            _context.Categorias.Remove(categoria);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CategoriaExists(Guid id)
        {
            return _context.Categorias.Any(e => e.CategoriaId == id);
        }
    }
}
