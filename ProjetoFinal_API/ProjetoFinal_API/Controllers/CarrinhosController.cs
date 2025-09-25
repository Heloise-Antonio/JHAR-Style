using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
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
    public class CarrinhosController : ControllerBase
    {
        private readonly Context _context;

        public CarrinhosController(Context context)
        {
            _context = context;
        }

        // GET: api/Carrinhos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Carrinho>>> GetCarrinhos()
        {
            return await _context.Carrinhos.ToListAsync();
        }

        // GET: api/Carrinhos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Carrinho>> GetCarrinho(Guid id)
        {
            var carrinho = await _context.Carrinhos.FindAsync(id);

            if (carrinho == null)
            {
                return NotFound();
            }

            return carrinho;
        }

        // PUT: api/Carrinhos/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{CarrinhoId}")]
        public async Task<ActionResult<CarrinhoViewModel>> PutCarrinho([FromRoute] Guid CarrinhoId, [FromBody] CarrinhoInputModel carrinho)
        {
            try
            {
                var dados = await _context.Carrinhos.Where(c => c.CarrinhoId == CarrinhoId).FirstOrDefaultAsync();

                if (dados != null)
                {
                    dados.ClienteId = carrinho.ClienteId;
                    dados.Observacoes = carrinho.Observacoes;
                    await _context.SaveChangesAsync();
                }

                var cli = await _context.Clientes.Where(c => c.ClienteId == dados.ClienteId).FirstOrDefaultAsync();

                CarrinhoViewModel cvm = new CarrinhoViewModel
                {
                    CarrinhoId = dados.CarrinhoId,
                    ClienteId = dados.ClienteId,
                    Cliente = cli.Nome,
                    DataHora = dados.DataHora,
                    Observacoes = dados.Observacoes
                };

                return Ok(cvm);
            }
            catch (Exception)
            {
                return UnprocessableEntity("Não foi possível gravar o carrinho!");                
            }


        }

        // POST: api/Carrinhos
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CarrinhoViewModel>> PostCarrinho([FromBody] CarrinhoInputModel input)
        {
             var carrinho = new Carrinho
             {
                 CarrinhoId = Guid.NewGuid(),
                 ClienteId = input.ClienteId,
                 Observacoes = input.Observacoes,
                 DataHora = DateTime.Now
             };

             _context.Carrinhos.Add(carrinho);
             await _context.SaveChangesAsync();

            CarrinhoViewModel c = new CarrinhoViewModel
            {
                CarrinhoId= Guid.NewGuid(),
                ClienteId= carrinho.ClienteId,
                Observacoes= carrinho.Observacoes,
                DataHora = DateTime.Now
            };

             return Ok(c);
        }
        // DELETE: api/Carrinhos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCarrinho(Guid id)
        {
            var carrinho = await _context.Carrinhos.FindAsync(id);
            if (carrinho == null)
            {
                return NotFound();
            }

            _context.Carrinhos.Remove(carrinho);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CarrinhoExists(Guid id)
        {
            return _context.Carrinhos.Any(e => e.CarrinhoId == id);
        }
    }
}
