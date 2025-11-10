using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class DetalleCompra
    {
        public Int64 IdCompra { get; set; }
        public Producto Producto { get; set; }
        public int Cantidad { get; set; }
        public int PrecioUnitario { get; set; }
        public int PrecioParcial { get; set; }
    }
}
