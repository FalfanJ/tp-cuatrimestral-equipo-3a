using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class ImagenNegocio
    {
        public List<Imagen> Listar()
        {
            List<Imagen> lista = new List<Imagen>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.SetearConsulta("SELECT IDImagen, IDProducto, Direccion FROM Producto_Imagenes");
                datos.EjecutarLectura();
                while (datos.Lector.Read())
                {
                    Imagen aux = new Imagen();
                    aux.IdImagen = (Int64)datos.Lector["IDImagen"];
                    aux.IdProducto = (Int64)datos.Lector["IDProducto"];
                    aux.Direccion = (string)datos.Lector["Direccion"];
                    lista.Add(aux);
                }
                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.CerrarConexion();
            }
        }
        public void Agregar(Imagen nuevo)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.SetearConsulta("INSERT INTO Producto_Imagenes (IDProducto, Direccion) VALUES (@idproducto, @direccion)");
                datos.SetearParametro("@idproducto", nuevo.IdProducto);
                datos.SetearParametro("@direccion", nuevo.Direccion);
                datos.EjecutarAccion();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.CerrarConexion();
            }
        }
        public void Agregar(List<Imagen> nuevo)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.SetearConsulta("INSERT INTO Producto_Imagenes (IDProducto, Direccion) VALUES (@idproducto, @direccion)");
                foreach (Imagen item in nuevo)
                {
                    datos.SetearParametro("@idproducto", item.IdProducto);
                    datos.SetearParametro("@direccion", item.Direccion);
                    datos.EjecutarAccion();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.CerrarConexion();
            }
        }
        public void Modificar(Imagen modificado)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.SetearConsulta("UPDATE Producto_Imagenes SET IDProducto = @idproducto, Direccion = @direccion WHERE IDImagen = @idimagen");
                datos.SetearParametro("@idimagen", modificado.IdImagen);
                datos.SetearParametro("@idproducto", modificado.IdProducto);
                datos.SetearParametro("@direccion", modificado.Direccion);
                datos.EjecutarAccion();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.CerrarConexion();
            }
        }
        public void BajaLogica(int ID)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.CerrarConexion();
            }
        }
    }
}
