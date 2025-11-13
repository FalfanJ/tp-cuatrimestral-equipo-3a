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

            //bool Estado = negCategoria.BajaLogica(0);
            //Console.WriteLine(Estado);



            //negCliente.Agregar(new Cliente { Nombre = "Rod", Apellido = "Falta", Dni = 4544212, Cuit=21421311, TipoPersona=true, Telefono= 1166339988, Email="zo@gmail.com", Direccion="AV123"});
            //negPersona.Agregar(new Persona { Nombre = "mm", Apellido = "RR", TipoPersona = false, Telefono = 445132132 });
            //negCliente.Agregar();
            //negDetalleCompra.Agregar();
            //negDetalleVenta.Agregar();
            //negHistorialMovimiento.Agregar();
            //negIMG.Agregar();
            //negPersona.Agregar();
            //negProductoProveedor.Agregar();
            //negProveedor.Agregar(new Proveedor { Nombre = "Compania1", Apellido = "SA", Cuit = 30589734123, TipoPersona = false, Telefono = 3368797421, Email = "compania@compania", Direccion = "MMM", RazonSocial="SociedadAnonima" }); // Funciona
            //negUsuario.Agregar(new Usuario { Nombre = "Usuario1", Apellido = "Admin1", Dni = 30333444, Cuit = 20303334441, TipoPersona = true, Telefono = 1122334455, Email = "ad@gmail.com", Direccion = "BF456", TipoUsuario="Admin", NombreUsuario="Admin1", Contraseña="ContraÑ" }); // funciona
            //negVeta.Agregar();

            //negCategoria.Agregar(new Categoria { Nombre="Comida"}); //funciona
            //negMarca.Agregar(new Marca { Nombre= "Samsung"}); //Funciona

            List<Imagen> listImg = new List<Imagen>();
            listImg.Add(new Imagen { Direccion = "RAW.HTMLLLAR1" });
            listImg.Add(new Imagen { Direccion = "RAW.HTMLLLAR2" });
            listImg.Add(new Imagen { Direccion = "RAW.HTMLLLAR3" });
            listImg.Add(new Imagen { Direccion = "RAW.HTMLLLAR4" });
            listImg.Add(new Imagen { Direccion = "RAW.HTMLLLAR5" });
            listImg.Add(new Imagen { Direccion = "RAW.HTMLLLAR6" });
            listImg.Add(new Imagen { Direccion = "RAW.HTMLLLAR7" });
            listImg.Add(new Imagen { Direccion = "RAW.HTMLLLAR8" });
            listImg.Add(new Imagen { Direccion = "RAW.HTMLLLAR9" });
            listImg.Add(new Imagen { Direccion = "RAW.HTMLLLAR0" });
            ////negIMG.Agregar(listImg);


            Producto pro = new Producto();
            pro.NSerie = "vnnv320";
            pro.Marca = new Marca { IdMarca = 1 };
            pro.Categoria = new Categoria { IdCategoria = 1 };
            pro.Nombre = "CON";
            pro.Precio = 20000;
            pro.Stock = 10021;
            pro.StockMinimo = 220;
            pro.PorcentajeGanancia = 01;
            pro.Modelo = "Cuidado2";
            pro.Descripcion = "FeoLindo";
            //pro.Imagenes = listImg;


            //negProducto.Agregar(pro);

            //List<DetalleCompra> detalleCompra = new List<DetalleCompra>();
            //detalleCompra.Add(new DetalleCompra { Cantidad=3, PrecioParcial= 302, PrecioUnitario= 10, Producto = new Producto { IdProducto = 1} });
            //detalleCompra.Add(new DetalleCompra { Cantidad=4, PrecioParcial= 330, PrecioUnitario= 132, Producto = new Producto { IdProducto = 2} });

            //Compra com = new Compra();
            //com.Proveedor = new Proveedor { IdProveedor = 1 };
            //com.Usuario = new Usuario { IdUsuario = 1 };
            //com.Fecha = DateTime.Now;
            //com.Total = 1000000;
            //com.Detalle = detalleCompra;

            //bool Nombre = negUsuario.Ingreso("Admin1", "ContraÑ");
            //Console.WriteLine(Nombre);

            //negCompra.Agregar(com);

            //List<Persona> personaLista = new List<Persona>();
            //List<Cliente> clienteLista = new List<Cliente>();
            //List<Proveedor> ProveedorLista = new List<Proveedor>();
            //List<Usuario> UsuarioLista = new List<Usuario>();
            //personaLista = negPersona.Listar();
            //clienteLista = negCliente.Listar();
            //ProveedorLista = negProveedor.Listar();
            //UsuarioLista = negUsuario.Listar();


            //Console.WriteLine("Lista Persona\n");
            //foreach (Persona item in personaLista)
            //{
            //    Console.WriteLine(item.IdPersona);
            //    Console.WriteLine(item.Nombre);
            //    Console.WriteLine(item.Apellido);
            //    Console.WriteLine(item.Dni);
            //    Console.WriteLine(item.Cuit);
            //    Console.WriteLine(item.TipoPersona);
            //    Console.WriteLine(item.Telefono);
            //    Console.WriteLine(item.Email);
            //    Console.WriteLine(item.Direccion);
            //    Console.WriteLine("\n");
            //}
            //Console.WriteLine("Lista Cliente\n");
            //foreach (Cliente item in clienteLista)
            //{
            //    Console.WriteLine(item.IdCliente);
            //    Console.WriteLine(item.IdPersona);
            //    Console.WriteLine(item.Nombre);
            //    Console.WriteLine(item.Apellido);
            //    Console.WriteLine(item.Dni);
            //    Console.WriteLine(item.Cuit);
            //    Console.WriteLine(item.TipoPersona);
            //    Console.WriteLine(item.Telefono);
            //    Console.WriteLine(item.Email);
            //    Console.WriteLine(item.Direccion);
            //    Console.WriteLine("\n");
            //    negCliente.Modificar(item);
            //}

            //Console.WriteLine("Lista Proveedor\n");
            //foreach (Proveedor item in ProveedorLista)
            //{
            //    Console.WriteLine(item.IdProveedor);
            //    Console.WriteLine(item.IdPersona);
            //    Console.WriteLine(item.Nombre);
            //    Console.WriteLine(item.Apellido);
            //    Console.WriteLine(item.Dni);
            //    Console.WriteLine(item.Cuit);
            //    Console.WriteLine(item.TipoPersona);
            //    Console.WriteLine(item.Telefono);
            //    Console.WriteLine(item.Email);
            //    Console.WriteLine(item.Direccion);
            //    Console.WriteLine(item.RazonSocial);
            //    Console.WriteLine("\n");
            //}
            //Console.WriteLine("Lista Usuario\n");
            //foreach (Usuario item in UsuarioLista)
            //{
            //    Console.WriteLine(item.IdUsuario);
            //    Console.WriteLine(item.IdPersona);
            //    Console.WriteLine(item.Nombre);
            //    Console.WriteLine(item.Apellido);
            //    Console.WriteLine(item.Dni);
            //    Console.WriteLine(item.Cuit);
            //    Console.WriteLine(item.TipoPersona);
            //    Console.WriteLine(item.Telefono);
            //    Console.WriteLine(item.Email);
            //    Console.WriteLine(item.Direccion);
            //    Console.WriteLine(item.TipoUsuario);
            //    Console.WriteLine(item.NombreUsuario);
            //    Console.WriteLine(item.Contraseña);
            //    Console.WriteLine("\n");
            //}

            List<Producto> listProducto = negProducto.Listar();
            foreach (Producto item in listProducto)
            {
                Console.WriteLine(item.IdProducto);
                Console.WriteLine(item.NSerie);
                Console.WriteLine(item.Marca.Nombre);
                Console.WriteLine(item.Categoria.Nombre);
                Console.WriteLine(item.Nombre);
                Console.WriteLine(item.Precio);
                Console.WriteLine(item.Stock);
                Console.WriteLine(item.StockMinimo);
                Console.WriteLine(item.PorcentajeGanancia);
                Console.WriteLine(item.Modelo);
                Console.WriteLine(item.Descripcion);
                Console.WriteLine("IMG Url\n");
                foreach (Imagen item2 in item.Imagenes)
                {
                    Console.WriteLine(item2.IdImagen);
                    Console.WriteLine(item2.IdProducto);
                    Console.WriteLine(item2.Direccion);
                    Console.WriteLine("IMG Nueva\n");

                }
            }
        }
    }
}
