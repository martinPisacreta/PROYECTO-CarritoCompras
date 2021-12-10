using System;
using System.Collections.Generic;

#nullable disable

namespace CarritoCompras.Models
{
    public partial class FacturaLog
    {
        public int IdFacturaLog { get; set; }
        public int IdFactura { get; set; }
        public int IdCliente { get; set; }
        public decimal CodTipoFactura { get; set; }
        public DateTime Fecha { get; set; }
        public long NroFactura { get; set; }
        public decimal PrecioFinal { get; set; }
        public int SnModificaPrecioFinal { get; set; }
        public decimal PrecioFinalConPagoMayorA30Dias { get; set; }
        public decimal PrecioFinalConPagoMenorA30Dias { get; set; }
        public decimal PrecioFinalConPagoMenorA7Dias { get; set; }
        public int SnEmitida { get; set; }
        public string Observacion { get; set; }
        public int SnMostrarPagoMayor30Dias { get; set; }
        public int SnMostrarPagoMenor7Dias { get; set; }
        public int SnMostrarPagoMenor30Dias { get; set; }
        public int IdCondicionFactura { get; set; }
        public DateTime? FechaSnEmitida { get; set; }
        public string PathFactura { get; set; }
        public DateTime FechaLog { get; set; }
        public string Accion { get; set; }
    }
}
