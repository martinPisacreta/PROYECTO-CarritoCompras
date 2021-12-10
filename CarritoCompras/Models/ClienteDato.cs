using System;
using System.Collections.Generic;

#nullable disable

namespace CarritoCompras.Models
{
    public partial class ClienteDato
    {
        public int IdCliente { get; set; }
        public decimal CodTipoDato { get; set; }
        public string TxtDatoCliente { get; set; }
        public int SnActivo { get; set; }
        public DateTime FecUltModif { get; set; }
        public string Accion { get; set; }

        public virtual TtipoDato CodTipoDatoNavigation { get; set; }
        public virtual Cliente IdClienteNavigation { get; set; }
    }
}
