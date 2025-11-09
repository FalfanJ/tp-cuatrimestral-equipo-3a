using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class ProductoProveedor
    {
        public Int64 IDPP { get; set; }
        public Producto Producto { get; set; }
        public Proveedor Proveedor { get; set; }
        public DateTime FechaAlta { get; set; }
        public DateTime FechaBaja { get; set; }
    }
}
