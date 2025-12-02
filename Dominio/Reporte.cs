using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Reporte
    {
        public string NombreProducto { get; set; }
        public int StockActual { get; set; }
        public int StockMinimo { get; set; }
        public string NombreLabel { get; set; } 
        public decimal TotalAcumulado { get; set; }
        public int CantidadVentas { get; set; }
        public string Categoria { get; set; }
        public decimal GananciaTotal { get; set; }
    }
}
