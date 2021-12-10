using System;
using System.Collections.Generic;

#nullable disable

namespace CarritoCompras.Models
{
    public partial class Factura
    {
        public Factura()
        {
            FacturaDetalles = new HashSet<FacturaDetalle>();
        }

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

        public virtual TtipoFactura CodTipoFacturaNavigation { get; set; }
        public virtual Cliente IdClienteNavigation { get; set; }
        public virtual TtipoCondicionFactura IdCondicionFacturaNavigation { get; set; }
        public virtual ICollection<FacturaDetalle> FacturaDetalles { get; set; }
    }
}
