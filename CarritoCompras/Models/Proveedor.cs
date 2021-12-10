using System;
using System.Collections.Generic;

#nullable disable

namespace CarritoCompras.Models
{
    public partial class Proveedor
    {
        public Proveedor()
        {
            Marcas = new HashSet<Marca>();
            ProveedorDatos = new HashSet<ProveedorDato>();
            ProveedorDirs = new HashSet<ProveedorDir>();
        }

        public int IdProveedor { get; set; }
        public string RazonSocial { get; set; }
        public int SnActivo { get; set; }
        public DateTime FecUltModif { get; set; }
        public string Accion { get; set; }
        public string PathImg { get; set; }
        public int? IdCondicionAnteIva { get; set; }
        public int? IdCondicionPago { get; set; }

        public virtual TtipoCondicionIva IdCondicionAnteIvaNavigation { get; set; }
        public virtual TtipoCondicionPago IdCondicionPagoNavigation { get; set; }
        public virtual ICollection<Marca> Marcas { get; set; }
        public virtual ICollection<ProveedorDato> ProveedorDatos { get; set; }
        public virtual ICollection<ProveedorDir> ProveedorDirs { get; set; }
    }
}
