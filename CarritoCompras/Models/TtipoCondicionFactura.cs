using System;
using System.Collections.Generic;

#nullable disable

namespace CarritoCompras.Models
{
    public partial class TtipoCondicionFactura
    {
        public TtipoCondicionFactura()
        {
            Clientes = new HashSet<Cliente>();
            Facturas = new HashSet<Factura>();
        }

        public int IdCondicionFactura { get; set; }
        public string TxtDesc { get; set; }
        public int SnActivo { get; set; }

        public virtual ICollection<Cliente> Clientes { get; set; }
        public virtual ICollection<Factura> Facturas { get; set; }
    }
}
