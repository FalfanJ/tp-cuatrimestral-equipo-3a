using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Venta
    {
        public int IdVenta { get; set; }
        public Cliente Cliente { get; set; }
        public Usuario Usuario { get; set; }
        public string Factura { get; set; }
        public DateTime Fecha { get; set; }
        public int Total { get; set; }
        public List<DetalleOperacion> Productos { get; set; } = new List<DetalleOperacion>();

    }
}
