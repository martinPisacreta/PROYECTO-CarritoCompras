using System;
using System.Collections.Generic;

#nullable disable

namespace CarritoCompras.Models
{
    public partial class FacturaDetalleLog
    {
        public int IdFacturaDetalleLog { get; set; }
        public int IdFacturaDetalle { get; set; }
        public long? IdArticulo { get; set; }
        public int IdFactura { get; set; }
        public int Cantidad { get; set; }
        public string CodigoArticuloMarca { get; set; }
        public string CodigoArticulo { get; set; }
        public string DescripcionArticulo { get; set; }
        public decimal PrecioListaXCoeficiente { get; set; }
        public decimal Iva { get; set; }
        public DateTime? FecBaja { get; set; }
        public DateTime FechaLog { get; set; }
        public string Accion { get; set; }
    }
}
