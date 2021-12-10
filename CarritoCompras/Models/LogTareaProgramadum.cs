using System;
using System.Collections.Generic;

#nullable disable

namespace CarritoCompras.Models
{
    public partial class LogTareaProgramadum
    {
        public int Id { get; set; }
        public string Tabla { get; set; }
        public DateTime Fecha { get; set; }
        public string Observacion { get; set; }
    }
}
