using System;
using System.Collections.Generic;

#nullable disable

namespace CarritoCompras.Models
{
    public partial class Tmunicipio
    {
        public Tmunicipio()
        {
            ClienteDirs = new HashSet<ClienteDir>();
            ProveedorDirs = new HashSet<ProveedorDir>();
        }

        public decimal CodMunicipio { get; set; }
        public decimal CodProvincia { get; set; }
        public decimal CodPais { get; set; }
        public string TxtDesc { get; set; }
        public decimal? CodDivipola { get; set; }

        public virtual Tpai CodPaisNavigation { get; set; }
        public virtual Tprovincium CodProvinciaNavigation { get; set; }
        public virtual ICollection<ClienteDir> ClienteDirs { get; set; }
        public virtual ICollection<ProveedorDir> ProveedorDirs { get; set; }
    }
}
