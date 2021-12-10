using CarritoCompras.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Carrito_Compras_Core.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioPedidosController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly CarritoComprasContext _context;

        public UsuarioPedidosController(CarritoComprasContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }





        // GET: api/UsuarioPedidos/5
        [Authorize]
        [HttpGet("{id_usuario}")]
        public async Task<ActionResult<IEnumerable<object>>> GetUsuarioPedido(int id_usuario)
        {

            try
            {

                var usuarioPedido = (from up in _context.UsuarioPedidos
                                     where up.IdUsuario == id_usuario
                                     orderby up.IdUsuarioPedido descending
                                     select new
                                     {
                                         id_usuario_pedido = up.IdUsuarioPedido,
                                         fecha_pedido = up.FechaPedido.ToShortDateString(),
                                         total = up.Total
                                     }

                          );

                if (usuarioPedido == null)
                {
                    return NotFound();
                }

                return await usuarioPedido.ToListAsync();

            }
            catch (Exception ex)
            {
                //retorno el error
                return BadRequest(new { message = ex.Message });
            }

        }

        // PUT: api/UsuarioPedidos/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUsuarioPedido(int id, UsuarioPedido usuarioPedido)
        {
            if (id != usuarioPedido.IdUsuarioPedido)
            {
                return BadRequest();
            }

            _context.Entry(usuarioPedido).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UsuarioPedidoExists(id))
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

        // POST: api/UsuarioPedidos
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [Authorize]
        [HttpPost]
        public async Task<ActionResult<UsuarioPedido>> PostUsuarioPedido([FromBody] UsuarioPedido usuarioPedido)
        {

            var dbContextTransaction = _context.Database.BeginTransaction();
            try
            {



                //genero una variable UsuarioPedido , que voy a retornar
                UsuarioPedido usuarioPedidoNuevo = new UsuarioPedido();

                usuarioPedidoNuevo.FechaPedido = DateTime.Now;
                usuarioPedidoNuevo.IdUsuario = usuarioPedido.IdUsuario;
                usuarioPedidoNuevo.IdEmpresa = usuarioPedido.IdEmpresaNavigation.IdEmpresa;
                usuarioPedidoNuevo.Total = usuarioPedido.Total;
                _context.UsuarioPedidos.Add(usuarioPedidoNuevo);
                await _context.SaveChangesAsync();



                foreach (UsuarioPedidoDetalle upd in usuarioPedido.UsuarioPedidoDetalles)
                {

                    //genero una variable UsuarioPedidoDetalle , que voy a retornar
                    UsuarioPedidoDetalle usuarioPedidoDetalleNuevo = new UsuarioPedidoDetalle();


                    //usuarioPedidoDetalleNuevo.IdUsuarioPedidoDetalle = upd.IdUsuarioPedidoDetalle;
                    usuarioPedidoDetalleNuevo.IdUsuarioPedido = usuarioPedidoNuevo.IdUsuarioPedido;
                    usuarioPedidoDetalleNuevo.CodigoArticulo = upd.CodigoArticulo;
                    usuarioPedidoDetalleNuevo.DescripcionArticulo = upd.DescripcionArticulo;
                    usuarioPedidoDetalleNuevo.TxtDescMarca = upd.TxtDescMarca;
                    usuarioPedidoDetalleNuevo.TxtDescFamilia = upd.TxtDescFamilia;
                    usuarioPedidoDetalleNuevo.PrecioListaPorCoeficientePorMedioIva = upd.PrecioListaPorCoeficientePorMedioIva;
                    usuarioPedidoDetalleNuevo.Utilidad = upd.Utilidad;
                    usuarioPedidoDetalleNuevo.SnOferta = upd.SnOferta;
                    usuarioPedidoDetalleNuevo.PrecioLista = upd.PrecioLista;
                    usuarioPedidoDetalleNuevo.Coeficiente = upd.Coeficiente;
                    usuarioPedidoDetalleNuevo.Cantidad = upd.Cantidad;
                    //usuarioPedidoDetalleNuevo.IdUsuarioPedidoNavigation = upd.IdUsuarioPedidoNavigation;
                    _context.UsuarioPedidoDetalles.Add(usuarioPedidoDetalleNuevo);
                    await _context.SaveChangesAsync();


                }



                if (sendPedidoEmail(usuarioPedido) == false)
                {
                    throw new Exception("Error al enviar el mail");
                }

                dbContextTransaction.Commit();
                return Ok(new { message = "Pedido finalizado correctamente" });


            }
            catch (Exception ex)
            {
                dbContextTransaction.Rollback();
                return BadRequest(new { message = ex.Message });

            }
        }


        private bool sendPedidoEmail(UsuarioPedido usuarioPedido)
        {
            try
            {

                string mailBody = "";
                bool bandera = false;



                mailBody =
                    "<html> " +
                            "<head> " +
                                    "<style> " +
                                        "table, th, td " +
                                        "{ " +
                                                "border: 1px solid black; " +
                                                "border - collapse: collapse; " +
                                        "} " +
                                    "</style> " +
                            "</head> " +
                            "<body> " +
                                "<div style = 'margin: 25px 25px 25px 25px;'> " +
                                    "<div> " +
                                        "<div> " +
                                            "<strong> Usuario : </strong> " +
                                            "<span>" + usuarioPedido.IdUsuarioNavigation.RazonSocial + "</span> " +
                                        "</div> " +

                                        "<div> " +
                                            "<strong> Telefono :</strong> " +
                                            "<span>" + usuarioPedido.IdUsuarioNavigation.Telefono + "</span> " +
                                        "</div> " +

                                        "<div> " +
                                            "<strong> Cuit :</strong> " +
                                            "<span>" + usuarioPedido.IdUsuarioNavigation.Cuit + "</span> " +
                                        "</div> " +

                                        "&nbsp; " +

                                        "<div> " +
                                            "<table style = 'width:100%; border-collapse: collapse;'> " +
                                                "<tr> " +
                                                    "<th> Cantidad </th> " +
                                                    "<th> Marca </th> " +
                                                    "<th> C&oacute;digo</th> " +
                                                    "<th> Precio Compra Usuario</th> " +
                                                //"<th> Descripci&oacute;n</th> " +
                                                "</tr>";
                foreach (UsuarioPedidoDetalle upd in usuarioPedido.UsuarioPedidoDetalles)
                {
                    mailBody +=
                        "<tr> " +
                            "<td align='center'>" + upd.Cantidad + "</td> " +
                            "<td align='center'>" + upd.TxtDescMarca + "</td> " +
                            "<td align='center'>" + upd.CodigoArticulo + "</td> " +
                            "<td align='center'>" + upd.PrecioListaPorCoeficientePorMedioIva.ToString("N2") + "</td> " +
                        //"<td>" + upd.DescripcionArticulo + "</td> " +
                        "</tr> ";
                }
                mailBody +=


                            "</table> " +

                             "<br/> <div> " +
                                "<strong> Total :</strong> " +
                                "<span>" + usuarioPedido.Total.ToString("N2") + "</span> " +
                            "</div> " +

                                        "</div> " +
                                    "</div> " +
                                "</div> " +
                            "</body> " +
                        "</html>";

                var fromAddress = new MailAddress(_configuration["Email:cuenta"], _configuration["Email:nombre"]);
                string userName = _configuration["Email:cuenta"];
                var toAddress = new MailAddress(_configuration["Email_Destino_Pedido:cuenta"], _configuration["Email_Destino_Pedido:nombre"]);
                string fromPassword = _configuration["Email:contrasena"];
                string host = _configuration["Email:host"];
                string subject = "Ingreso de pedido : " + usuarioPedido.IdUsuarioNavigation.RazonSocial;
                string body = mailBody;


                SmtpClient smtp = new SmtpClient(host, 25);

                smtp.Credentials = new System.Net.NetworkCredential(userName, fromPassword);
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;


                using (var message = new MailMessage(fromAddress, toAddress)
                {
                    Subject = subject,
                    Body = body,
                    IsBodyHtml = true
                })
                {
                    smtp.Send(message);
                }



                bandera = true;
                return bandera;


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // DELETE: api/UsuarioPedidos/5
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<ActionResult<UsuarioPedido>> DeleteUsuarioPedido(int id)
        {
            var usuarioPedido = await _context.UsuarioPedidos.FindAsync(id);
            if (usuarioPedido == null)
            {
                return NotFound();
            }

            _context.UsuarioPedidos.Remove(usuarioPedido);
            await _context.SaveChangesAsync();

            return usuarioPedido;
        }

        private bool UsuarioPedidoExists(int id)
        {
            return _context.UsuarioPedidos.Any(e => e.IdUsuarioPedido == id);
        }



    }
}
