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
        public Int16 Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
        public decimal PrecioParcial { get; set; }
    }
}
