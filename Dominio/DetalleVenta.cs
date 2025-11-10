using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class DetalleVenta
    {
        public Int64 IdVenta { get; set; }
        public Producto Producto { get; set; }
        public int Cantidad { get; set; }
        public int PrecioUnitario { get; set; }
        public int PrecioParcial { get; set; }
        public int PorcentajeGanancia { get; set; }
    }
}
