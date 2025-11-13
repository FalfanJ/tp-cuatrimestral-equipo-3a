using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Producto
    {
        public Int64 IdProducto { get; set; }
        public string NSerie { get; set; }
        public Marca Marca { get; set; }
        public Categoria Categoria { get; set; }
        public string Nombre { get; set; }
        public decimal Precio { get; set; }
        public Int16 Stock { get; set; }
        public Int16 StockMinimo { get; set; }
        public Int16 PorcentajeGanancia { get; set; }
        public string Modelo { get; set; }
        public string Descripcion { get; set; }
        public List<Imagen> Imagenes { get; set; } = new List<Imagen>();
    }
}
