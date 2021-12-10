using System;
using System.Collections.Generic;

#nullable disable

namespace CarritoCompras.Models
{
    public partial class TtipoCondicionIva
    {
        public TtipoCondicionIva()
        {
            Clientes = new HashSet<Cliente>();
            Empresas = new HashSet<Empresa>();
            Proveedors = new HashSet<Proveedor>();
        }

        public int IdCondicionAnteIva { get; set; }
        public string TxtDesc { get; set; }
        public string Tipo { get; set; }
        public string TxtDescResumido { get; set; }

        public virtual ICollection<Cliente> Clientes { get; set; }
        public virtual ICollection<Empresa> Empresas { get; set; }
        public virtual ICollection<Proveedor> Proveedors { get; set; }
    }
}
