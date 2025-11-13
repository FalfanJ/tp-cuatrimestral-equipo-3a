using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Venta
    {
        public Int64 IdVenta { get; set; }
        public Cliente Cliente { get; set; }
        public Usuario Usuario { get; set; }
        public string NFactura { get; set; }
        public DateTime Fecha { get; set; }
        public decimal Total { get; set; }
        public List<DetalleVenta> Detalle { get; set; } = new List<DetalleVenta>();
    }
}
