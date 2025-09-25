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
    public class PagamentosController : ControllerBase
    {
        private readonly Context _context;

        public PagamentosController(Context context)
        {
            _context = context;
        }

        // GET: api/Pagamentos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Pagamento>>> GetPagamentos()
        {
            return await _context.Pagamentos.Include(m => m.MetodoPagamento)
                .ToListAsync();
        }

        // GET: api/Pagamentos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Pagamento>> GetPagamento(Guid id)
        {
            var pagamento = await _context.Pagamentos.FindAsync(id);

            if (pagamento == null)
            {
                return NotFound();
            }

            return pagamento;
        }

        // PUT: api/Pagamentos/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPagamento(Guid id, Pagamento pagamento)
        {
            if (id != pagamento.PagamentoId)
            {
                return BadRequest();
            }

            _context.Entry(pagamento).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PagamentoExists(id))
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

        // POST: api/Pagamentos
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<PagamentoViewModelcs>> PostPagamento([FromBody] PagamentoInputModel input)
        {

            var pagamento = new Pagamento
            {
                PagamentoId = Guid.NewGuid(),
                CarrinhoId = input.CarrinhoId,  
                DataHora = DateTime.Now,
                Valor = input.Valor,
                MetodoPagamentoId = input.MetodoPagamentoId
            };
            _context.Pagamentos.Add(pagamento);
            await _context.SaveChangesAsync();

            var pag = await _context.Pagamentos
                .Include(m => m.MetodoPagamento)
                .FirstOrDefaultAsync(p => p.PagamentoId == pagamento.PagamentoId);

            PagamentoViewModelcs p = new PagamentoViewModelcs
            {
                PagamentoId = pag.PagamentoId,
                MetodoPagamentoId = pag.MetodoPagamentoId,
                Valor = pag.Valor,
                CarrinhoId = pag.CarrinhoId,
                DataHora = DateTime.Now,
                MetodoPagamento = pag.MetodoPagamento
            };

            return Ok(p);
        }

        // DELETE: api/Pagamentos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePagamento(Guid id)
        {
            var pagamento = await _context.Pagamentos.FindAsync(id);
            if (pagamento == null)
            {
                return NotFound();
            }

            _context.Pagamentos.Remove(pagamento);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PagamentoExists(Guid id)
        {
            return _context.Pagamentos.Any(e => e.PagamentoId == id);
        }
    }
}
