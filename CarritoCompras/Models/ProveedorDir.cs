using System;
using System.Collections.Generic;

#nullable disable

namespace CarritoCompras.Models
{
    public partial class ProveedorDir
    {
        public int IdProveedor { get; set; }
        public decimal CodTipoDir { get; set; }
        public int CodClaseDir { get; set; }
        public decimal? CodTipoCalle { get; set; }
        public decimal? CodCalle { get; set; }
        public string TxtNumero { get; set; }
        public string TxtApto { get; set; }
        public string TxtPiso { get; set; }
        public string TxtDireccion { get; set; }
        public string TxtCodPostal { get; set; }
        public decimal CodPais { get; set; }
        public decimal? CodProvincia { get; set; }
        public decimal CodMunicipio { get; set; }
        public int SnActivo { get; set; }
        public DateTime? FecUltModif { get; set; }
        public string Accion { get; set; }

        public virtual Tcalle CodCalleNavigation { get; set; }
        public virtual Tmunicipio CodMunicipioNavigation { get; set; }
        public virtual Tpai CodPaisNavigation { get; set; }
        public virtual Tprovincium CodProvinciaNavigation { get; set; }
        public virtual TtipoCalle CodTipoCalleNavigation { get; set; }
        public virtual TtipoDir CodTipoDirNavigation { get; set; }
        public virtual Proveedor IdProveedorNavigation { get; set; }
    }
}
