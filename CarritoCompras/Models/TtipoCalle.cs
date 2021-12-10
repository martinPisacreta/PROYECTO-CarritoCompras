﻿using System;
using System.Collections.Generic;

#nullable disable

namespace CarritoCompras.Models
{
    public partial class TtipoCalle
    {
        public TtipoCalle()
        {
            ClienteDirs = new HashSet<ClienteDir>();
            ProveedorDirs = new HashSet<ProveedorDir>();
        }

        public decimal CodTipoCalle { get; set; }
        public string TxtDesc { get; set; }

        public virtual ICollection<ClienteDir> ClienteDirs { get; set; }
        public virtual ICollection<ProveedorDir> ProveedorDirs { get; set; }
    }
}
