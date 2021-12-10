using System;
using System.Collections.Generic;

#nullable disable

namespace CarritoCompras.Models
{
    public partial class TtipoFactura
    {
        public TtipoFactura()
        {
            Facturas = new HashSet<Factura>();
        }

        public decimal CodTipoFactura { get; set; }
        public string TxtDesc { get; set; }
        public string Letra { get; set; }

        public virtual ICollection<Factura> Facturas { get; set; }
    }
}
