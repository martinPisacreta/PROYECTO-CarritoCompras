using System;
using System.Collections.Generic;

#nullable disable

namespace CarritoCompras.Models
{
    public partial class Empresa
    {
        public Empresa()
        {
            UsuarioPedidos = new HashSet<UsuarioPedido>();
        }

        public int IdEmpresa { get; set; }
        public string RazonSocial { get; set; }
        public string NombreFantasia { get; set; }
        public string Cuit { get; set; }
        public string Telefono { get; set; }
        public string Celular { get; set; }
        public string Email { get; set; }
        public string Direccion { get; set; }
        public int IdCondicionAnteIva { get; set; }
        public DateTime? FechaInicioActividad { get; set; }

        public virtual TtipoCondicionIva IdCondicionAnteIvaNavigation { get; set; }
        public virtual ICollection<UsuarioPedido> UsuarioPedidos { get; set; }
    }
}
