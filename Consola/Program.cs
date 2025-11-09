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

            //negCliente.Agregar(new Cliente { Nombre = "Rod", Apellido = "Falta", Dni = 4544212, Cuit=21421311, TipoPersona=true, Telefono= 1166339988, Email="zo@gmail.com", Direccion="AV123"});
            negPersona.Agregar(new Persona { Nombre = "mm", Apellido = "RR", TipoPersona = false, Telefono = 445132132 });


            List<Persona> personaLista = new List<Persona>();
            List<Cliente> clienteLista = new List<Cliente>();
            List<Proveedor> ProveedorLista = new List<Proveedor>();
            List<Usuario> UsuarioLista = new List<Usuario>();
            personaLista = negPersona.Listar();
            clienteLista = negCliente.Listar();
            ProveedorLista = negProveedor.Listar();
            UsuarioLista = negUsuario.Listar();
            
            
            Console.WriteLine("Lista Persona\n");
            foreach (Persona item in personaLista)
            {
                Console.WriteLine(item.IdPersona);
                Console.WriteLine(item.Nombre);
                Console.WriteLine(item.Apellido);
                Console.WriteLine(item.Dni);
                Console.WriteLine(item.Cuit);
                Console.WriteLine(item.TipoPersona);
                Console.WriteLine(item.Telefono);
                Console.WriteLine(item.Email);
                Console.WriteLine(item.Direccion);
                Console.WriteLine("\n");
            }
            Console.WriteLine("Lista Cliente\n");
            foreach (Cliente item in clienteLista)
            {
                Console.WriteLine(item.IdCliente);
                Console.WriteLine(item.IdPersona);
                Console.WriteLine(item.Nombre);
                Console.WriteLine(item.Apellido);
                Console.WriteLine(item.Dni);
                Console.WriteLine(item.Cuit);
                Console.WriteLine(item.TipoPersona);
                Console.WriteLine(item.Telefono);
                Console.WriteLine(item.Email);
                Console.WriteLine(item.Direccion);
                Console.WriteLine("\n");
                negCliente.Modificar(item);
            }

            Console.WriteLine("Lista Proveedor\n");
            foreach (Proveedor item in ProveedorLista)
            {
                Console.WriteLine(item.IdProveedor);
                Console.WriteLine(item.IdPersona);
                Console.WriteLine(item.Nombre);
                Console.WriteLine(item.Apellido);
                Console.WriteLine(item.Dni);
                Console.WriteLine(item.Cuit);
                Console.WriteLine(item.TipoPersona);
                Console.WriteLine(item.Telefono);
                Console.WriteLine(item.Email);
                Console.WriteLine(item.Direccion);
                Console.WriteLine(item.RazonSocial);
                Console.WriteLine("\n");
            }
            Console.WriteLine("Lista Usuario\n");
            foreach (Usuario item in UsuarioLista)
            {
                Console.WriteLine(item.IdUsuario);
                Console.WriteLine(item.IdPersona);
                Console.WriteLine(item.Nombre);
                Console.WriteLine(item.Apellido);
                Console.WriteLine(item.Dni);
                Console.WriteLine(item.Cuit);
                Console.WriteLine(item.TipoPersona);
                Console.WriteLine(item.Telefono);
                Console.WriteLine(item.Email);
                Console.WriteLine(item.Direccion);
                Console.WriteLine(item.TipoUsuario);
                Console.WriteLine(item.NombreUsuario);
                Console.WriteLine(item.Contraseña);
                Console.WriteLine("\n");
            }
        }
    }
}
