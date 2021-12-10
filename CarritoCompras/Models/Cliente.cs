using System;
using System.Collections.Generic;

#nullable disable

namespace CarritoCompras.Models
{
    public partial class Cliente
    {
        public Cliente()
        {
            ClienteDatos = new HashSet<ClienteDato>();
            ClienteDirs = new HashSet<ClienteDir>();
            Facturas = new HashSet<Factura>();
        }

        public int IdCliente { get; set; }
        public string NombreFantasia { get; set; }
        public string RazonSocial { get; set; }
        public int SnActivo { get; set; }
        public DateTime FecUltModif { get; set; }
        public string Accion { get; set; }
        public int? IdCondicionAnteIva { get; set; }
        public int? IdCondicionPago { get; set; }
        public int IdCondicionFactura { get; set; }
        public int IdTipoCliente { get; set; }
        public int? IdVendedor { get; set; }

        public virtual TtipoCondicionIva IdCondicionAnteIvaNavigation { get; set; }
        public virtual TtipoCondicionFactura IdCondicionFacturaNavigation { get; set; }
        public virtual TtipoCondicionPago IdCondicionPagoNavigation { get; set; }
        public virtual TtipoCliente IdTipoClienteNavigation { get; set; }
        public virtual Vendedor IdVendedorNavigation { get; set; }
        public virtual ICollection<ClienteDato> ClienteDatos { get; set; }
        public virtual ICollection<ClienteDir> ClienteDirs { get; set; }
        public virtual ICollection<Factura> Facturas { get; set; }
    }
}
