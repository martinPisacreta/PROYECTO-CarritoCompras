using System;
using System.Collections.Generic;

#nullable disable

namespace CarritoCompras.Models
{
    public partial class ArticuloHistorico
    {
        public int IdLista { get; set; }
        public long IdArticulo { get; set; }
        public string CodigoArticuloMarca { get; set; }
        public string CodigoArticulo { get; set; }
        public string DescripcionArticulo { get; set; }
        public decimal? PrecioLista { get; set; }
        public int? IdTablaFamilia { get; set; }
        public int? SnOferta { get; set; }
        public string PathImg { get; set; }
        public DateTime? FechaUltModif { get; set; }
        public DateTime? FecBaja { get; set; }
        public string Accion { get; set; }
        public int? Stock { get; set; }
        public long? IdOrden { get; set; }

        public virtual Familium IdTablaFamiliaNavigation { get; set; }
    }
}
