using System;
using System.Collections.Generic;

#nullable disable

namespace CarritoCompras.Models
{
    public partial class Vendedor
    {
        public Vendedor()
        {
            Clientes = new HashSet<Cliente>();
        }

        public int IdVendedor { get; set; }
        public string Nombre { get; set; }
        public int SnActivo { get; set; }
        public DateTime FecUltModif { get; set; }
        public string Accion { get; set; }

        public virtual ICollection<Cliente> Clientes { get; set; }
    }
}
