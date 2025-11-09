using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Producto
    {
        public int IdProducto { get; set; }
        public string NSerie { get; set; }
        public Marca Marca { get; set; }
        public Categoria Categoria { get; set; }
        public string Nombre { get; set; }
        public int Precio { get; set; }
        public int Stock { get; set; }
        public int StockMinimo { get; set; }
        public int PorcentajeGanancia { get; set; }
        public string Modelo { get; set; }
        public string Descripcion { get; set; }
        public List<Imagen> Imagenes { get; set; } = new List<Imagen>();
    }
}
