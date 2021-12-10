using System;
using System.Collections.Generic;

#nullable disable

namespace CarritoCompras.Models
{
    public partial class ClienteCuentaCorriente
    {
        public int IdClienteCuentaCorriente { get; set; }
        public int IdCliente { get; set; }
        public int? IdFactura { get; set; }
        public DateTime? FechaFacturaVieja { get; set; }
        public long? NroFacturaVieja { get; set; }
        public decimal? CodTipoFacturaVieja { get; set; }
        public decimal? ImpFactura { get; set; }
        public decimal? Pago1 { get; set; }
        public decimal? Pago2 { get; set; }
        public decimal? Pago3 { get; set; }
        public decimal? Pago4 { get; set; }
        public DateTime? Pago1Fecha { get; set; }
        public DateTime? Pago2Fecha { get; set; }
        public DateTime? Pago3Fecha { get; set; }
        public DateTime? Pago4Fecha { get; set; }
        public string Observacion { get; set; }
    }
}
