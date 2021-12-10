using System;
using System.Collections.Generic;

#nullable disable

namespace CarritoCompras.Models
{
    public partial class Familium
    {
        public Familium()
        {
            Articulos = new HashSet<Articulo>();
        }

        public int IdTablaFamilia { get; set; }
        public int IdFamilia { get; set; }
        public int IdTablaMarca { get; set; }
        public string TxtDescFamilia { get; set; }
        public int SnActivo { get; set; }
        public DateTime FecUltModif { get; set; }
        public string Accion { get; set; }
        public string PathImg { get; set; }
        public decimal Algoritmo1 { get; set; }
        public decimal Algoritmo2 { get; set; }
        public decimal Algoritmo3 { get; set; }
        public decimal Algoritmo4 { get; set; }
        public decimal Algoritmo5 { get; set; }
        public decimal Algoritmo6 { get; set; }
        public decimal Algoritmo7 { get; set; }
        public decimal Algoritmo8 { get; set; }
        public decimal Algoritmo9 { get; set; }
        public decimal Algoritmo10 { get; set; }

        public virtual Marca IdTablaMarcaNavigation { get; set; }
        public virtual ICollection<Articulo> Articulos { get; set; }
    }
}
