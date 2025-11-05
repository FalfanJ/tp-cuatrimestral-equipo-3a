using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Compras
    {
        public int IDCompra { get; set; }
        public int IDProveedor { get; set; }
        public int IDUsurairo { get; set; }
        public DateTime Fecha { get; set; }
        public List<DetalleOperacion> Productos { get; set; } = new List<DetalleOperacion>();
        public int Total { get; set; }

    }
}
