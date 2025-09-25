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
    public class MetodoPagamentosController : ControllerBase
    {
        private readonly Context _context;

        public MetodoPagamentosController(Context context)
        {
            _context = context;
        }

        // GET: api/MetodoPagamentos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MetodoPagamento>>> GetMetodosPagamento()
        {
            return await _context.MetodosPagamento.ToListAsync();
        }

        // GET: api/MetodoPagamentos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MetodoPagamento>> GetMetodoPagamento(Guid id)
        {
            var metodoPagamento = await _context.MetodosPagamento.FindAsync(id);

            if (metodoPagamento == null)
            {
                return NotFound();
            }

            return metodoPagamento;
        }

        // PUT: api/MetodoPagamentos/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMetodoPagamento(Guid id, MetodoPagamento metodoPagamento)
        {
            if (id != metodoPagamento.MetodoPagamentoId)
            {
                return BadRequest();
            }

            _context.Entry(metodoPagamento).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MetodoPagamentoExists(id))
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

        // POST: api/MetodoPagamentos
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<MetodoPagamentoViewModel>> PostMetodoPagamento(MetodoPagamentoInputModel input)
        {
            var metodoPagamento = new MetodoPagamento
            {
                MetodoPagamentoId = Guid.NewGuid(),
                Nome = input.Nome
            };
            _context.MetodosPagamento.Add(metodoPagamento);
            await _context.SaveChangesAsync();

            MetodoPagamentoViewModel m = new MetodoPagamentoViewModel
            {
                MetodoPagamentoId = metodoPagamento.MetodoPagamentoId,
                Nome = metodoPagamento.Nome   

            };

            return Ok(m);
        }

        // DELETE: api/MetodoPagamentos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMetodoPagamento(Guid id)
        {
            var metodoPagamento = await _context.MetodosPagamento.FindAsync(id);
            if (metodoPagamento == null)
            {
                return NotFound();
            }

            _context.MetodosPagamento.Remove(metodoPagamento);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MetodoPagamentoExists(Guid id)
        {
            return _context.MetodosPagamento.Any(e => e.MetodoPagamentoId == id);
        }
    }
}
