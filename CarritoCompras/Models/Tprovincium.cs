using System;
using System.Collections.Generic;

#nullable disable

namespace CarritoCompras.Models
{
    public partial class Tprovincium
    {
        public Tprovincium()
        {
            ClienteDirs = new HashSet<ClienteDir>();
            ProveedorDirs = new HashSet<ProveedorDir>();
            Tmunicipios = new HashSet<Tmunicipio>();
        }

        public decimal CodProvincia { get; set; }
        public decimal CodPais { get; set; }
        public string TxtDesc { get; set; }

        public virtual Tpai CodPaisNavigation { get; set; }
        public virtual ICollection<ClienteDir> ClienteDirs { get; set; }
        public virtual ICollection<ProveedorDir> ProveedorDirs { get; set; }
        public virtual ICollection<Tmunicipio> Tmunicipios { get; set; }
    }
}
