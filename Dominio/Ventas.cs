using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Ventas
    {
        public int IdVenta { get; set; }
        public Productos Producto { get; set; }
        public Clientes Cliente { get; set; }
        public int CantidadVendida { get; set; }
    }
}
