using CarritoCompras.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Mail;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CarritoCompras.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {

        private readonly IConfiguration _configuration;
        private readonly CarritoComprasContext _context;

        public UsuariosController(CarritoComprasContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public class Token
        {
            public string token { get; set; }
        }

        public class Email
        {
            public string email { get; set; }
        }


        public class Password
        {
            public string password { get; set; }
        }

        public class Login
        {
            public string email { get; set; }
            public string password { get; set; }
        }


        public class ResetPassword
        {
            public string token { get; set; }
            public string password { get; set; }
            public string confirm_password { get; set; }
        }

        public class Usuario_Detalles_Basicos
        {
            public int id { get; set; }
            public string nombre { get; set; }
            public string apellido { get; set; }
            public string razonSocial { get; set; }
            public string email { get; set; }
            public string rol { get; set; }
            public string cuit { get; set; }
            public string telefono { get; set; }
            public DateTime? dateUpdated { get; set; }

            public string direccion { get; set; }

            public int utilidad { get; set; }
        }



        [Authorize]
        [HttpGet("{id}")] // GET /api/Usuarios/1
        public async Task<ActionResult<Usuario>> GetUsuario(string id)
        {
            try
            {

                //busco el usuario con ese id
                var usuario = await _context.Usuarios.Where(u => u.IdUsuario.ToString() == id).FirstAsync();

                //si no existe , retorno el error
                if (usuario == null)
                {
                    throw new Exception("Usuario no encontrado");
                }

                //retorno el usuario encontrado
                return Ok(usuario);

            }
            catch (Exception ex)
            {
                //retorno el error
                return BadRequest(new { message = ex.Message });
            }

        }


        [Authorize]
        [HttpPost("update/{id}")] // POST api/Usuarios/update/1
        public async Task<IActionResult> update(string id, [FromBody] Usuario usuario_a_updetear)
        {
            var dbContextTransaction = _context.Database.BeginTransaction();
            try
            {

                //busco el usuario con ese id
                var usuario_bd = await _context.Usuarios.Where(u => u.IdUsuario.ToString() == id).FirstAsync();


                //si no existe , retorno el error
                if (usuario_bd == null)
                {
                    throw new Exception("Usuario no encontrado");
                }


                Usuario usuario_controlo_mail = null;

                //valido de no registrar un mail ya existente
                usuario_controlo_mail = _context.Usuarios.FirstOrDefault(t => t.Email == usuario_a_updetear.Email && t.IdUsuario.ToString() != id);
                if (usuario_controlo_mail != null)
                {
                    throw new Exception("Email " + usuario_a_updetear.Email + " ya esta registrado");
                }


                //valido de no registrar una RazonSocial ya existente
                usuario_controlo_mail = _context.Usuarios.FirstOrDefault(t => t.RazonSocial == usuario_a_updetear.RazonSocial && t.IdUsuario.ToString() != id);
                if (usuario_controlo_mail != null)
                {
                    throw new Exception("Razon Social " + usuario_a_updetear.RazonSocial + " ya esta registrada");
                }

                //valido de no registrar una Cuit ya existente
                usuario_controlo_mail = _context.Usuarios.FirstOrDefault(t => t.Cuit == usuario_a_updetear.Cuit && t.IdUsuario.ToString() != id);
                if (usuario_controlo_mail != null)
                {
                    throw new Exception("Cuit " + usuario_a_updetear.Cuit + " ya esta registrado");
                }

                //valido de no registrar una Telefono ya existente
                usuario_controlo_mail = _context.Usuarios.FirstOrDefault(t => t.Telefono == usuario_a_updetear.Telefono && t.IdUsuario.ToString() != id);
                if (usuario_controlo_mail != null)
                {
                    throw new Exception("Telefono " + usuario_a_updetear.Telefono + " ya esta registrado");
                }

                //valido de no registrar una Nombre ya existente
                //usuario_controlo_mail = _context.Usuarios.FirstOrDefault(t => t.Nombre == usuario_a_updetear.Nombre && t.IdUsuario.ToString() != id);
                //if (usuario_controlo_mail != null)
                //{
                //    return BadRequest(new { message = "Nombre " + usuario_a_updetear.Nombre + " ya esta registrado" });
                //}

                ////valido de no registrar una Apellido ya existente
                //usuario_controlo_mail = _context.Usuarios.FirstOrDefault(t => t.Apellido == usuario_a_updetear.Apellido && t.IdUsuario.ToString() != id);
                //if (usuario_controlo_mail != null)
                //{
                //    return BadRequest(new { message = "Apellido " + usuario_a_updetear.Apellido + " ya esta registrado" });
                //}

                //valido de no registrar una DireccionDescripcion ya existente
                usuario_controlo_mail = _context.Usuarios.FirstOrDefault(t => t.DireccionDescripcion == usuario_a_updetear.DireccionDescripcion && t.IdUsuario.ToString() != id);
                if (usuario_controlo_mail != null)
                {
                    throw new Exception("Direccion " + usuario_a_updetear.DireccionDescripcion + " ya esta registrada");
                }


                //usuario_bd.Email = usuario_a_updetear.Email; //no se puede modificar
                if (usuario_a_updetear.Password != "")
                {
                    usuario_bd.Password = Encriptar(usuario_a_updetear.Password);
                }


                usuario_bd.Cuit = usuario_a_updetear.Cuit;
                usuario_bd.Telefono = usuario_a_updetear.Telefono;
                usuario_bd.Nombre = usuario_a_updetear.Nombre;
                usuario_bd.Apellido = usuario_a_updetear.Apellido;
                usuario_bd.RazonSocial = usuario_a_updetear.RazonSocial;
                if (usuario_a_updetear.Rol != null)
                {
                    usuario_bd.Rol = usuario_a_updetear.Rol;
                }
                usuario_bd.FechaUltimaModificacionUsuario = DateTime.Now;
                usuario_bd.DireccionValor = usuario_a_updetear.DireccionValor;
                usuario_bd.DireccionDescripcion = usuario_a_updetear.DireccionDescripcion;
                usuario_bd.Lat = usuario_a_updetear.Lat;
                usuario_bd.Lng = usuario_a_updetear.Lng;
                usuario_bd.Utilidad = usuario_a_updetear.Utilidad;
                await _context.SaveChangesAsync();

                //genero una variable usuario_retorno , que voy a retornar
                Usuario_Detalles_Basicos usuario_retorno = new Usuario_Detalles_Basicos();
                usuario_retorno.id = usuario_bd.IdUsuario;
                usuario_retorno.nombre = usuario_bd.Nombre;
                usuario_retorno.apellido = usuario_bd.Apellido;
                usuario_retorno.razonSocial = usuario_bd.RazonSocial;
                usuario_retorno.email = usuario_bd.Email;
                usuario_retorno.rol = usuario_bd.Rol;
                usuario_retorno.cuit = usuario_bd.Cuit;
                usuario_retorno.telefono = usuario_bd.Telefono;
                usuario_retorno.dateUpdated = usuario_bd.FechaUltimaModificacionUsuario;
                usuario_retorno.direccion = usuario_bd.DireccionDescripcion;
                usuario_retorno.utilidad = usuario_bd.Utilidad;

                // Commit Transaction
                dbContextTransaction.Commit();

                //retorno usuario_retorno
                return Ok(usuario_retorno);


            }
            catch (Exception ex)
            {
                //retorno el error
                dbContextTransaction.Rollback();
                return BadRequest(new { message = ex.Message });

            }
        }


        [Authorize]
        [HttpDelete("{id}")] // DELETE: api/Usuarios/5
        public async Task<ActionResult<Usuario>> DeleteUsuario(string id)
        {
            var dbContextTransaction = _context.Database.BeginTransaction();
            try
            {

                //busco el usuario con ese id
                var usuario = await _context.Usuarios.Where(u => u.IdUsuario.ToString() == id).FirstAsync();

                //si no existe , retorno el error
                if (usuario == null)
                {
                    throw new Exception("Usuario no encontrado");
                }

                //remuevo el usuario encontrado
                _context.Usuarios.Remove(usuario);
                await _context.SaveChangesAsync();

                //Commit Transaction
                dbContextTransaction.Commit();

                //retorno mensaje
                return Ok(new { message = "Cuenta eliminada correctamente" });

            }
            catch (Exception ex)
            {
                //retorno el error
                dbContextTransaction.Rollback();
                return BadRequest(new { message = ex.Message });

            }
        }




        [HttpPost("authenticate")]
        public IActionResult authenticate([FromBody] Login login)
        {
            try
            {

                //voy a buscar el usuario que tenga el email que viene por parametro y este verificado
                Usuario usuario = _context.Usuarios.FirstOrDefault(u => u.Email.Contains(login.email) && u.UsuarioVerificado == true);

                //si no existe , retorno el error
                if (usuario == null)
                {
                    throw new Exception("Usuario no encontrado");
                }
                else //si existe
                {
                    //.. y la contraseña que viene por parametro es diferente a la contraseña del usuario encontrado , retorno el error
                    if (login.password != DesEncriptar(usuario.Password))
                    {
                        throw new Exception("Email o contraseña incorrectos");
                    }
                }


                //genero una variable _usuario , que voy a retornar
                var _usuario = new
                {
                    id = usuario.IdUsuario,
                    nombre = usuario.Nombre,
                    apellido = usuario.Apellido,
                    razonSocial = usuario.RazonSocial,
                    email = usuario.Email,
                    rol = usuario.Rol,
                    cuit = usuario.Cuit,
                    telefono = usuario.Telefono,

                    //dateCreated = usuario.DateCreated,
                    //dateUpdated = usuario.DateUpdated,
                    token = usuario.TokenVerificacion, //DEVUELVO EL TOKEN DE LA BASE DE DATOS
                    direccion = usuario.DireccionDescripcion,
                    utilidad = usuario.Utilidad
                };

                //si el token , es vacio , retorno error
                if (_usuario.token == "")
                {
                    throw new Exception("Error de token");

                }

                //retorno usuario
                var user = JsonConvert.SerializeObject(_usuario);
                return Ok(_usuario);
            }
            catch (Exception ex)
            {
                //retorno el error
                return BadRequest(new { message = ex.Message });

            }
        }


        [HttpPost("register")]
        public async Task<IActionResult> register([FromBody] Usuario usuario_que_voy_a_registrar)
        {

            var dbContextTransaction = _context.Database.BeginTransaction();
            try
            {

                string _rol = "";

                Usuario u = null;
                //valido de no registrar un mail ya existente
                u = _context.Usuarios.FirstOrDefault(t => t.Email == usuario_que_voy_a_registrar.Email);
                if (u != null)
                {
                    throw new Exception("Email " + usuario_que_voy_a_registrar.Email + " ya esta registrado");
                }

                //valido de no registrar una RazonSocial ya existente
                u = _context.Usuarios.FirstOrDefault(t => t.RazonSocial == usuario_que_voy_a_registrar.RazonSocial);
                if (u != null)
                {
                    throw new Exception("Razon Social " + usuario_que_voy_a_registrar.RazonSocial + " ya esta registrada");
                }

                //valido de no registrar una Cuit ya existente
                u = _context.Usuarios.FirstOrDefault(t => t.Cuit == usuario_que_voy_a_registrar.Cuit);
                if (u != null)
                {
                    throw new Exception("Cuit " + usuario_que_voy_a_registrar.Cuit + " ya esta registrado");
                }

                //valido de no registrar una Telefono ya existente
                u = _context.Usuarios.FirstOrDefault(t => t.Telefono == usuario_que_voy_a_registrar.Telefono);
                if (u != null)
                {
                    throw new Exception("Telefono " + usuario_que_voy_a_registrar.Telefono + " ya esta registrado");
                }

                //valido de no registrar una Nombre ya existente
                //u = _context.Usuarios.FirstOrDefault(t => t.Nombre == usuario_que_voy_a_registrar.Nombre);
                //if (u != null)
                //{
                //    return BadRequest(new { message = "Nombre " + usuario_que_voy_a_registrar.Nombre + " ya esta registrado" });
                //}

                ////valido de no registrar una Apellido ya existente
                //u = _context.Usuarios.FirstOrDefault(t => t.Apellido == usuario_que_voy_a_registrar.Apellido);
                //if (u != null)
                //{
                //    return BadRequest(new { message = "Apellido " + usuario_que_voy_a_registrar.Apellido + " ya esta registrado" });
                //}

                //valido de no registrar una DireccionDescripcion ya existente
                u = _context.Usuarios.FirstOrDefault(t => t.DireccionDescripcion == usuario_que_voy_a_registrar.DireccionDescripcion);
                if (u != null)
                {
                    throw new Exception("Direccion " + usuario_que_voy_a_registrar.DireccionDescripcion + " ya esta registrada");
                }




                //busco el tipo de usuario
                if (_context.Usuarios.Count() == 0)
                {
                    _rol = "Admin";
                }
                else
                {
                    _rol = "User";
                }

                //genero un token sin vencimiento
                string TokenVerificacion = generarTokenAuthenticate(usuario_que_voy_a_registrar.Nombre, usuario_que_voy_a_registrar.Apellido, usuario_que_voy_a_registrar.Email, 0);
                if (TokenVerificacion == "" || TokenVerificacion == null)
                {
                    throw new Exception("Error al generar el Token de Registro");
                }

                //genero una variable usuario , que voy a retornar
                Usuario usuarioNuevo = new Usuario();

                usuarioNuevo.Email = usuario_que_voy_a_registrar.Email;
                usuarioNuevo.Password = Encriptar(usuario_que_voy_a_registrar.Password);
                usuarioNuevo.RazonSocial = usuario_que_voy_a_registrar.RazonSocial;
                usuarioNuevo.Cuit = usuario_que_voy_a_registrar.Cuit;
                usuarioNuevo.AceptaTerminos = usuario_que_voy_a_registrar.AceptaTerminos; //se esta guardando "True"
                usuarioNuevo.Rol = _rol;
                usuarioNuevo.TokenVerificacion = TokenVerificacion;
                usuarioNuevo.UsuarioVerificado = false;
                usuarioNuevo.TokenReseteo = null;
                usuarioNuevo.TokenReseteoFechaExpiracion = null;
                usuarioNuevo.FechaCreacionUsuario = DateTime.Now;
                usuarioNuevo.FechaUltimaModificacionUsuario = DateTime.Now;
                usuarioNuevo.Telefono = usuario_que_voy_a_registrar.Telefono;
                usuarioNuevo.Nombre = usuario_que_voy_a_registrar.Nombre;
                usuarioNuevo.Apellido = usuario_que_voy_a_registrar.Apellido;
                usuarioNuevo.DireccionValor = usuario_que_voy_a_registrar.DireccionValor;
                usuarioNuevo.DireccionDescripcion = usuario_que_voy_a_registrar.DireccionDescripcion;
                usuarioNuevo.Lat = usuario_que_voy_a_registrar.Lat;
                usuarioNuevo.Lng = usuario_que_voy_a_registrar.Lng;
                usuarioNuevo.Utilidad = 20;
                _context.Usuarios.Add(usuarioNuevo);
                await _context.SaveChangesAsync();



                //envio mail de verificacion
                bool bandera = sendVerificationEmail(usuarioNuevo, _configuration["DomainName"]);
                if (bandera == true)
                {
                    dbContextTransaction.Commit();
                    return Ok(new { message = "Registro exitoso, revise su correo electrónico para ver las instrucciones de verificación" });

                }
                else
                {

                    throw new Exception("Error al enviar email , verifique su email e intentelo nuevamente");

                }
            }
            catch (Exception ex)
            {
                dbContextTransaction.Rollback();
                return BadRequest(new { message = ex.Message });

            }
        }



        [HttpPost("verify-email")]
        public async Task<IActionResult> verifyEmail([FromBody] Token token)
        {
            var dbContextTransaction = _context.Database.BeginTransaction();
            try
            {

                Usuario usuario = _context.Usuarios.FirstOrDefault(t => t.TokenVerificacion.Contains(token.token));
                if (usuario == null)
                {
                    throw new Exception("Fallo en la verificación");
                }

                if (usuario.UsuarioVerificado == true)
                {
                    throw new Exception("El usuario ya esta verificado");
                }



                usuario.UsuarioVerificado = true;
                usuario.FechaCreacionUsuario = DateTime.Now;
                await _context.SaveChangesAsync();

                dbContextTransaction.Commit();
                return Ok(new { message = "Verificación exitosa,ahora puede iniciar sesión" });


            }
            catch (Exception ex)
            {
                dbContextTransaction.Rollback();
                return BadRequest(new { message = ex.Message });
            }

        }


        [HttpPost("forgot-password")]
        public async Task<IActionResult> forgotPassword([FromBody] Email email)
        {
            var dbContextTransaction = _context.Database.BeginTransaction();
            bool bandera = false;
            try
            {
                Usuario usuario = _context.Usuarios.FirstOrDefault(t => t.Email.Contains(email.email));
                if (usuario == null)
                {
                    throw new Exception("El email no esta registrado");
                }

                //genero un token con vencimiento
                string TokenVerificacion = generarTokenAuthenticate(usuario.Nombre, usuario.Apellido, usuario.Email, 1);
                if (TokenVerificacion == "" || TokenVerificacion == null)
                {
                    throw new Exception("Error al generar el Token Reseteo");
                }

                usuario.TokenVerificacion = usuario.TokenVerificacion;
                usuario.TokenReseteo = TokenVerificacion;
                usuario.TokenReseteoFechaExpiracion = DateTime.Now.AddDays(1);
                usuario.FechaUltimaModificacionUsuario = DateTime.Now;
                await _context.SaveChangesAsync();


                // send email
                bandera = sendPasswordResetEmail(usuario, _configuration["DomainName"]);
                if (bandera == true)
                {
                    dbContextTransaction.Commit();
                    return Ok(new { message = "Por favor revise su correo electrónico para obtener instrucciones de restablecimiento de contraseña" });

                }
                else
                {

                    throw new Exception("Error al enviar email , verifique su email e intentelo nuevamente");
                }
            }
            catch (Exception ex)
            {
                dbContextTransaction.Rollback();
                return BadRequest(new { message = ex.Message });
            }

        }


        [HttpPost("validate-reset-token")]
        public IActionResult validateResetToken([FromBody] Token token)
        {
            try
            {
                // u.resetTokenExpiry >= DateTime.Now -> que el token no haya expirado 
                Usuario usuario = _context.Usuarios.FirstOrDefault(u => u.TokenReseteo.Contains(token.token) && u.TokenReseteoFechaExpiracion >= DateTime.Now);
                if (usuario == null)
                {
                    throw new Exception("Token invalido");
                }

                return Ok(new { message = "Token es valido" });

            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }

        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> resetPassword([FromBody] ResetPassword resetPassword)
        {
            var dbContextTransaction = _context.Database.BeginTransaction();
            try
            {
                // u.ResetTokenExpiry >= DateTime.Now -> que el token no haya expirado 
                Usuario usuario = _context.Usuarios.FirstOrDefault(u => u.TokenReseteo.Contains(resetPassword.token) && u.TokenReseteoFechaExpiracion >= DateTime.Now);
                if (usuario == null)
                {
                    throw new Exception("Token invalido");
                }

                //genero un token sin vencimiento
                string TokenVerificacion = generarTokenAuthenticate(usuario.Nombre, usuario.Apellido, usuario.Email, 0);
                if (TokenVerificacion == "" || TokenVerificacion == null)
                {
                    throw new Exception("Error al generar el Token");
                }

                usuario.TokenVerificacion = TokenVerificacion;
                usuario.Password = Encriptar(resetPassword.password);
                usuario.UsuarioVerificado = true;
                usuario.TokenReseteo = null;
                usuario.TokenReseteoFechaExpiracion = null;
                usuario.FechaUltimaModificacionUsuario = DateTime.Now;
                await _context.SaveChangesAsync();

                dbContextTransaction.Commit();
                return Ok(new { message = "Restablecimiento de contraseña exitoso, ahora puede iniciar sesión" });

            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }

        }

        private bool sendVerificationEmail(Usuario usuario, string DomainName)
        {
            try
            {

                //aca va "/usuario/... y no "/usuarios/... porque es asi el path desde la parte REACT JS
                bool bandera = false;
                string verifyUrl = DomainName + "/usuario/verify-email?token=" + usuario.TokenVerificacion;
                string mensaje = "<h4>Activar cuenta</h4>" +
                                    "<p> ¡Un usuario nuevo quiere registrarse a la web! </p>" +
                                    "<p> Utilice el siguiente token para activar su cuenta: </p>" +
                                    "<p><a class='btn btn-primary' href=" + verifyUrl + "> Verificar </a></p>";
                var fromAddress = new MailAddress(_configuration["Email:cuenta"], _configuration["Email:nombre"]);
                string userName = _configuration["Email:cuenta"];
                var toAddress = new MailAddress(_configuration["Email_Destino_Pedido:cuenta"], _configuration["Email_Destino_Pedido:nombre"]); //A PEDIDO DE MAXI EL MAIL LE LLEGA A EL PARA VALIDAR TOKEN REGISTRO Y NO AL USUARIO
                string fromPassword = _configuration["Email:contrasena"];
                string host = _configuration["Email:host"];
                string subject = "Activar cuenta usuario : " + usuario.RazonSocial;
                string body = mensaje;


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


        private bool sendPasswordResetEmail(Usuario usuario, string DomainName)
        {
            try
            {

                //aca va "/usuario/... y no "/usuarios/... porque es asi el path desde la parte REACT JS
                bool bandera = false;
                string resetUrl = DomainName + "/usuario/reset-password?token=" + usuario.TokenReseteo;
                string mensaje = "<h4>Restablecer contraseña Email </h4>" +
                                 "<p>Haga clic en el siguiente enlace para restablecer su contraseña, el enlace será válido por 1 día:</p>" +
                                 "<p><a class='btn btn-primary' href=" + resetUrl + "> Restablecer </a></p>";
                var fromAddress = new MailAddress(_configuration["Email:cuenta"], _configuration["Email:nombre"]);
                string userName = _configuration["Email:cuenta"];
                var toAddress = new MailAddress(usuario.Email, usuario.RazonSocial);
                string fromPassword = _configuration["Email:contrasena"];
                string host = _configuration["Email:host"];
                string subject = "Verificar Email";
                string body = mensaje;


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




        private string generarTokenAuthenticate(string nombre, string apellido, string email, int minutos_expired)
        {
            string jwt_token = "";
            try
            {
                //create claims details based on the user information
                var claims = new[] {
                    new Claim(JwtRegisteredClaimNames.Sub, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                    new Claim("Nombre", nombre),
                    new Claim("Apellido", apellido),
                    new Claim("Email", email)
                   };

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));

                var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                JwtSecurityToken token = null;
                if (minutos_expired == 0) //si es 0 , pongo expired en infinito
                {
                    token = new JwtSecurityToken(_configuration["Jwt:Issuer"], _configuration["Jwt:Audience"], claims, signingCredentials: signIn);
                }
                else //si es 1 , pongo expired en 1 dia
                {
                    token = new JwtSecurityToken(_configuration["Jwt:Issuer"], _configuration["Jwt:Audience"], claims, expires: DateTime.UtcNow.AddDays(1), signingCredentials: signIn);
                }


                jwt_token = new JwtSecurityTokenHandler().WriteToken(token);
                return jwt_token;
            }
            catch (Exception ex)
            {
                jwt_token = "";
                return jwt_token;
            }

        }

        private bool UsuarioExists(string id)
        {
            return _context.Usuarios.Any(e => e.Email == id);
        }


        /// Encripta una cadena
        public static string Encriptar(string _cadenaAencriptar)
        {
            string result = string.Empty;
            byte[] encryted = System.Text.Encoding.Unicode.GetBytes(_cadenaAencriptar);
            result = Convert.ToBase64String(encryted);
            return result;
        }

        /// Esta función desencripta la cadena que le envíamos en el parámentro de entrada.
        public static string DesEncriptar(string _cadenaAdesencriptar)
        {
            string result = string.Empty;
            byte[] decryted = Convert.FromBase64String(_cadenaAdesencriptar);
            //result = System.Text.Encoding.Unicode.GetString(decryted, 0, decryted.ToArray().Length);
            result = System.Text.Encoding.Unicode.GetString(decryted);
            return result;
        }



    }
}