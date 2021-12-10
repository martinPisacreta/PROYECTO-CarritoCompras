using CarritoCompras.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Carrito_Compras_Core.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioPedidoDetallesController : ControllerBase
    {
        private readonly CarritoComprasContext _context;

        public UsuarioPedidoDetallesController(CarritoComprasContext context)
        {
            _context = context;
        }



        // GET: api/UsuarioPedidoDetalles/5
        [Authorize]
        [HttpGet("{id_usuario_pedido}")]
        public async Task<ActionResult<IEnumerable<object>>> GetUsuarioPedidoDetalle(int id_usuario_pedido)
        {


            var usuarioPedidoDetalle = (from upd in _context.UsuarioPedidoDetalles
                                        where upd.IdUsuarioPedido == id_usuario_pedido
                                        orderby upd.IdUsuarioPedidoDetalle ascending
                                        select upd
                                       );

            if (usuarioPedidoDetalle == null)
            {
                return NotFound();
            }

            return await usuarioPedidoDetalle.ToListAsync();
        }

        // PUT: api/UsuarioPedidoDetalles/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUsuarioPedidoDetalle(int id, UsuarioPedidoDetalle usuarioPedidoDetalle)
        {
            if (id != usuarioPedidoDetalle.IdUsuarioPedidoDetalle)
            {
                return BadRequest();
            }

            _context.Entry(usuarioPedidoDetalle).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UsuarioPedidoDetalleExists(id))
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

        // POST: api/UsuarioPedidoDetalles
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [Authorize]
        [HttpPost]
        public async Task<ActionResult<UsuarioPedidoDetalle>> PostUsuarioPedidoDetalle(UsuarioPedidoDetalle usuarioPedidoDetalle)
        {
            _context.UsuarioPedidoDetalles.Add(usuarioPedidoDetalle);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUsuarioPedidoDetalle", new { id = usuarioPedidoDetalle.IdUsuarioPedidoDetalle }, usuarioPedidoDetalle);
        }

        // DELETE: api/UsuarioPedidoDetalles/5
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<ActionResult<UsuarioPedidoDetalle>> DeleteUsuarioPedidoDetalle(int id)
        {
            var usuarioPedidoDetalle = await _context.UsuarioPedidoDetalles.FindAsync(id);
            if (usuarioPedidoDetalle == null)
            {
                return NotFound();
            }

            _context.UsuarioPedidoDetalles.Remove(usuarioPedidoDetalle);
            await _context.SaveChangesAsync();

            return usuarioPedidoDetalle;
        }

        private bool UsuarioPedidoDetalleExists(int id)
        {
            return _context.UsuarioPedidoDetalles.Any(e => e.IdUsuarioPedidoDetalle == id);
        }


    }
}
