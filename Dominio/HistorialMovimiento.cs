using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class HistorialMovimiento
    {
        public Int64 IdHistorial { get; set; }
        public Producto Producto { get; set; }
        public Venta Venta { get; set; }
        public Compra Compra { get; set; }
        public Usuario Usuario { get; set; }
        public Int16 StockAnterior { get; set; }
        public Int16 StockPosterior { get; set; }
        public DateTime Fecha { get; set; }
    }
}
