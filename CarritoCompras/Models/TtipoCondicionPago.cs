using System;
using System.Collections.Generic;

#nullable disable

namespace CarritoCompras.Models
{
    public partial class TtipoCondicionPago
    {
        public TtipoCondicionPago()
        {
            Clientes = new HashSet<Cliente>();
            Proveedors = new HashSet<Proveedor>();
        }

        public int IdCondicionPago { get; set; }
        public string TxtDesc { get; set; }

        public virtual ICollection<Cliente> Clientes { get; set; }
        public virtual ICollection<Proveedor> Proveedors { get; set; }
    }
}
