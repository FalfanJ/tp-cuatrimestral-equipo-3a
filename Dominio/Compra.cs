using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Compra
    {
        public int IdCompra { get; set; }
        public Proveedor Proveedor { get; set; }
        public Usuario Usuario { get; set; }
        public DateTime Fecha { get; set; }
        public int Total { get; set; }
        public List<DetalleOperacion> Productos { get; set; } = new List<DetalleOperacion>();

    }
}
