using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class DetalleVenta
    {
        public Int64? IdVenta { get; set; }
        public Producto Producto { get; set; }
        public Int16 Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
        public decimal PrecioParcial { get; set; }
        public Int16 PorcentajeGanancia { get; set; }
        public Int64? ID { get; set; }
    }
}
