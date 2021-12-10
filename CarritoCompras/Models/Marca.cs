using System;
using System.Collections.Generic;

#nullable disable

namespace CarritoCompras.Models
{
    public partial class Marca
    {
        public Marca()
        {
            Familia = new HashSet<Familium>();
        }

        public int IdTablaMarca { get; set; }
        public int IdMarca { get; set; }
        public int IdProveedor { get; set; }
        public string TxtDescMarca { get; set; }
        public int SnActivo { get; set; }
        public DateTime FecUltModif { get; set; }
        public string Accion { get; set; }
        public string PathImg { get; set; }

        public virtual Proveedor IdProveedorNavigation { get; set; }
        public virtual ICollection<Familium> Familia { get; set; }
    }
}
