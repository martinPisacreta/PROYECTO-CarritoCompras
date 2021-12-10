using System;
using System.Collections.Generic;

#nullable disable

namespace CarritoCompras.Models
{
    public partial class Usuario
    {
        public Usuario()
        {
            UsuarioPedidos = new HashSet<UsuarioPedido>();
        }

        public int IdUsuario { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string RazonSocial { get; set; }
        public string Cuit { get; set; }
        public bool? AceptaTerminos { get; set; }
        public string Rol { get; set; }
        public string TokenVerificacion { get; set; }
        public bool? UsuarioVerificado { get; set; }
        public string TokenReseteo { get; set; }
        public DateTime? TokenReseteoFechaExpiracion { get; set; }
        public DateTime? FechaCreacionUsuario { get; set; }
        public DateTime? FechaUltimaModificacionUsuario { get; set; }
        public string Telefono { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string DireccionValor { get; set; }
        public string DireccionDescripcion { get; set; }
        public string Lat { get; set; }
        public string Lng { get; set; }
        public int Utilidad { get; set; }

        public virtual ICollection<UsuarioPedido> UsuarioPedidos { get; set; }
    }
}
