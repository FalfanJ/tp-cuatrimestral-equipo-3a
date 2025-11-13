using Dominio;
using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Consola
{
    internal class Program
    {
        static void Main(string[] args)
        {
            CategoriaNegocio negCategoria = new CategoriaNegocio();
            ClienteNegocio negCliente = new ClienteNegocio();
            CompraNegocio negCompra = new CompraNegocio();
            DetalleCompraNegocio negDetalleCompra = new DetalleCompraNegocio();
            DetalleVentaNegocio negDetalleVenta = new DetalleVentaNegocio();
            HistorialMovimientoNegocio negHistorialMovimiento = new HistorialMovimientoNegocio();
            ImagenNegocio negIMG = new ImagenNegocio();
            MarcaNegocio negMarca = new MarcaNegocio();
            PersonaNegocio negPersona = new PersonaNegocio();
            ProductoNegocio negProducto = new ProductoNegocio();
            ProductoProveedorNegocio negProductoProveedor = new ProductoProveedorNegocio();
            ProveedorNegocio negProveedor = new ProveedorNegocio();
            UsuarioNegocio negUsuario = new UsuarioNegocio();
            VentaNegocio negVeta = new VentaNegocio();


            //negCliente.Agregar(new Cliente { Nombre = "Rod", Apellido = "Falta", Dni = 4544212, Cuit = 21421311, TipoPersona = true, Telefono = 1166339988, Email = "zo@gmail.com", Direccion = "AV123" });
            //negPersona.Agregar(new Persona { Nombre = "mm", Apellido = "RR", TipoPersona = false, Telefono = 445132132 });
            //negUsuario.Agregar(new Usuario { Nombre = "Usuario1", Apellido = "Admin1", Dni = 30333444, Cuit = 20303334441, TipoPersona = true, Telefono = 1122334455, Email = "ad@gmail.com", Direccion = "BF456", TipoUsuario = "Admin", NombreUsuario = "Admin1", Contraseña = "ContraÑ" }); // funciona
            //negProveedor.Agregar(new Proveedor { Nombre = "Compania1", Apellido = "SA", Cuit = 30589734123, TipoPersona = false, Telefono = 3368797421, Email = "compania@compania", Direccion = "MMM", RazonSocial = "SociedadAnonima" }); // Funciona
            //negCategoria.Agregar(new Categoria { Nombre = "Comida" }); //funciona
            //negMarca.Agregar(new Marca { Nombre = "Samsung" }); //Funciona



            //List<Imagen> listImg = new List<Imagen>();
            //listImg.Add(new Imagen { Direccion = "RAW.HTMLLLAR1" });
            //listImg.Add(new Imagen { Direccion = "RAW.HTMLLLAR2" });
            //listImg.Add(new Imagen { Direccion = "RAW.HTMLLLAR3" });
            //listImg.Add(new Imagen { Direccion = "RAW.HTMLLLAR4" });
            //listImg.Add(new Imagen { Direccion = "RAW.HTMLLLAR5" });
            //listImg.Add(new Imagen { Direccion = "RAW.HTMLLLAR6" });
            //listImg.Add(new Imagen { Direccion = "RAW.HTMLLLAR7" });
            //listImg.Add(new Imagen { Direccion = "RAW.HTMLLLAR8" });
            //listImg.Add(new Imagen { Direccion = "RAW.HTMLLLAR9" });
            //listImg.Add(new Imagen { Direccion = "RAW.HTMLLLAR0" });


            //Producto pro = new Producto();
            //pro.NSerie = "vnnv320";
            //pro.Marca = new Marca { IdMarca = 1 };
            //pro.Categoria = new Categoria { IdCategoria = 1 };
            //pro.Nombre = "CON";
            //pro.Precio = 20000;
            //pro.Stock = 10021;
            //pro.StockMinimo = 220;
            //pro.PorcentajeGanancia = 01;
            //pro.Modelo = "Cuidado2";
            //pro.Descripcion = "FeoLindo";
            //pro.Imagenes = listImg;

            //negProducto.Agregar(pro);

            //List<DetalleCompra> detalleCompra = new List<DetalleCompra>();
            //detalleCompra.Add(new DetalleCompra { Cantidad = 3, PrecioParcial = 302, PrecioUnitario = 10, Producto = new Producto { IdProducto = 1 } });

            //Compra com = new Compra();
            //com.Proveedor = new Proveedor { IdProveedor = 1 };
            //com.Usuario = new Usuario { IdUsuario = 1 };
            //com.Fecha = DateTime.Now;
            //com.Total = 1000000;
            //com.Detalle = detalleCompra;

            //negCompra.Agregar(com);


            //ProductoProveedor oo = new ProductoProveedor();
            //oo.Producto = new Producto();
            //oo.Proveedor = new Proveedor();
            //oo.Producto.IdProducto = 1;
            //oo.Proveedor.IdProveedor = 1;
            //oo.FechaAlta = DateTime.Today;
            //negProductoProveedor.Agregar(oo);

            //List<DetalleVenta> detvent = new List<DetalleVenta>();
            //detvent.Add(new DetalleVenta {Cantidad = 29, PrecioParcial = 203, PrecioUnitario = 30, PorcentajeGanancia = 40, Producto = new Producto { IdProducto = 1 } });

            //Venta ven = new Venta();
            //ven.Cliente = new Cliente { IdCliente = 1 };
            //ven.Usuario = new Usuario { IdUsuario = 1 };
            //ven.NFactura = "22657331";
            //ven.Fecha = DateTime.Now;
            //ven.Detalle = detvent;

            //negVeta.Agregar(ven);


            //HistorialMovimiento his = new HistorialMovimiento();
            //his.Producto = new Producto { IdProducto = 1 };
            //his.Venta = new Venta { IdVenta = 1 };
            //his.Usuario = new Usuario { IdUsuario = 1 };
            //his.StockAnterior = 20;
            //his.StockPosterior = 10;
            //his.Fecha = DateTime.Now;


            //negHistorialMovimiento.Agregar(his);

            //his.Producto = new Producto { IdProducto = 1 };
            //his.Compra = new Compra { IdCompra = 1 };
            //his.Usuario = new Usuario { IdUsuario = 1 };
            //his.StockAnterior = 20;
            //his.StockPosterior = 10;
            //his.Fecha = DateTime.Now;

            //negHistorialMovimiento.Agregar(his);

            List<Persona> aux = new List<Persona>();

            aux=negPersona.Listar();

            foreach (Persona item in aux)
            {
                Console.WriteLine(item.IdPersona);
            }

        }
    }
}
