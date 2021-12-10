using System;
using System.Collections.Generic;

#nullable disable

namespace CarritoCompras.Models
{
    public partial class TtipoDato
    {
        public TtipoDato()
        {
            ClienteDatos = new HashSet<ClienteDato>();
            ProveedorDatos = new HashSet<ProveedorDato>();
        }

        public decimal CodTipoDato { get; set; }
        public string TxtDesc { get; set; }

        public virtual ICollection<ClienteDato> ClienteDatos { get; set; }
        public virtual ICollection<ProveedorDato> ProveedorDatos { get; set; }
    }
}
