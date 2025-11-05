using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Productos
    {
        public int IdProducto { get; set; }
        public int NSerie { get; set; }
        public int Precio { get; set; }
        public int Stock { get; set; }
        public int StockMinimo { get; set; }
        public string Descripcion { get; set; }
        public Marcas Marca { get; set; }
        public Categorias Categoria { get; set; }
        public List<Proveedores> Proveedor { get; set; } = new List<Proveedores>();
        public List<Imagenes> Imagenes { get; set; } = new List<Imagenes>();
    }
}
