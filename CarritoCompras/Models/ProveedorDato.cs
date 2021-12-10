using System;
using System.Collections.Generic;

#nullable disable

namespace CarritoCompras.Models
{
    public partial class ProveedorDato
    {
        public int IdProveedor { get; set; }
        public decimal CodTipoDato { get; set; }
        public string TxtDatoProveedor { get; set; }
        public int SnActivo { get; set; }
        public DateTime FecUltModif { get; set; }
        public string Accion { get; set; }

        public virtual TtipoDato CodTipoDatoNavigation { get; set; }
        public virtual Proveedor IdProveedorNavigation { get; set; }
    }
}
