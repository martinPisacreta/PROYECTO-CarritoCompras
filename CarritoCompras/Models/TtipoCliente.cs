using System;
using System.Collections.Generic;

#nullable disable

namespace CarritoCompras.Models
{
    public partial class TtipoCliente
    {
        public TtipoCliente()
        {
            Clientes = new HashSet<Cliente>();
        }

        public int IdTipoCliente { get; set; }
        public string TxtDesc { get; set; }
        public int SnActivo { get; set; }

        public virtual ICollection<Cliente> Clientes { get; set; }
    }
}
