using System;
using System.Collections.Generic;
using Dominio;

namespace Negocio
{
    public class ProveedorNegocio
    {
        public List<Proveedor> Listar()
        {
            List<Proveedor> lista = new List<Proveedor>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.SetearConsulta("SELECT IDProveedor, Nombre, CUIT, Direccion, Telefono, Email, Estado FROM Proveedor WHERE Estado = 1");
                datos.EjecutarLectura();

                while (datos.Lector.Read())
                {
                    Proveedor aux = new Proveedor
                    {
                        IdProveedor = (long)datos.Lector["IDProveedor"],
                        Nombre = datos.Lector["Nombre"]?.ToString(),
                        CUIT = datos.Lector["CUIT"]?.ToString(),
                        Direccion = datos.Lector["Direccion"]?.ToString(),
                        Telefono = datos.Lector["Telefono"]?.ToString(),
                        Email = datos.Lector["Email"]?.ToString(),
                        Estado = (bool)datos.Lector["Estado"]
                    };
                    lista.Add(aux);
                }

                return lista;
            }
            finally
            {
                datos.CerrarConexion();
            }
        }

        public void Agregar(Proveedor nuevo)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.SetearConsulta("INSERT INTO Proveedor (Nombre, CUIT, Direccion, Telefono, Email, Estado) VALUES (@nombre, @cuit, @direccion, @tel, @mail, 1)");
                datos.SetearParametro("@nombre", nuevo.Nombre);
                datos.SetearParametro("@cuit", nuevo.CUIT);
                datos.SetearParametro("@direccion", nuevo.Direccion);
                datos.SetearParametro("@tel", nuevo.Telefono);
                datos.SetearParametro("@mail", nuevo.Email);
                datos.EjecutarAccion();
            }
            finally
            {
                datos.CerrarConexion();
            }
        }

        public void Editar(Proveedor proveedor)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.SetearConsulta("UPDATE Proveedor SET Nombre=@nombre, CUIT=@cuit, Direccion=@direccion, Telefono=@tel, Email=@mail WHERE IDProveedor=@id");
                datos.SetearParametro("@nombre", proveedor.Nombre);
                datos.SetearParametro("@cuit", proveedor.CUIT);
                datos.SetearParametro("@direccion", proveedor.Direccion);
                datos.SetearParametro("@tel", proveedor.Telefono);
                datos.SetearParametro("@mail", proveedor.Email);
                datos.SetearParametro("@id", proveedor.IdProveedor);
                datos.EjecutarAccion();
            }
            finally
            {
                datos.CerrarConexion();
            }
        }

        public void BajaLogica(long id)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.SetearConsulta("UPDATE Proveedor SET Estado = 0 WHERE IDProveedor = @id");
                datos.SetearParametro("@id", id);
                datos.EjecutarAccion();
            }
            finally
            {
                datos.CerrarConexion();
            }
        }
    }
}
