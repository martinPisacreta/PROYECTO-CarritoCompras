using System;
using System.Collections.Generic;

#nullable disable

namespace CarritoCompras.Models
{
    public partial class Tpai
    {
        public Tpai()
        {
            ClienteDirs = new HashSet<ClienteDir>();
            ProveedorDirs = new HashSet<ProveedorDir>();
            Tmunicipios = new HashSet<Tmunicipio>();
            Tprovincia = new HashSet<Tprovincium>();
        }

        public decimal CodPais { get; set; }
        public string TxtDesc { get; set; }

        public virtual ICollection<ClienteDir> ClienteDirs { get; set; }
        public virtual ICollection<ProveedorDir> ProveedorDirs { get; set; }
        public virtual ICollection<Tmunicipio> Tmunicipios { get; set; }
        public virtual ICollection<Tprovincium> Tprovincia { get; set; }
    }
}
