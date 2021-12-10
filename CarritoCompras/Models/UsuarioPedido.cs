using System;
using System.Collections.Generic;

#nullable disable

namespace CarritoCompras.Models
{
    public partial class UsuarioPedido
    {
        public UsuarioPedido()
        {
            UsuarioPedidoDetalles = new HashSet<UsuarioPedidoDetalle>();
        }

        public int IdUsuarioPedido { get; set; }
        public DateTime FechaPedido { get; set; }
        public int IdUsuario { get; set; }
        public int IdEmpresa { get; set; }
        public decimal Total { get; set; }

        public virtual Empresa IdEmpresaNavigation { get; set; }
        public virtual Usuario IdUsuarioNavigation { get; set; }
        public virtual ICollection<UsuarioPedidoDetalle> UsuarioPedidoDetalles { get; set; }
    }
}
