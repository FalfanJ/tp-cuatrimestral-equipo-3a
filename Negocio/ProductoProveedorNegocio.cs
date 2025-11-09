using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class ProductoProveedorNegocio
    {
        public List<ProductoProveedor> Listar()
        {
            List<ProductoProveedor> lista = new List<ProductoProveedor>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.SetearConsulta("SELECT IDPP, IDProducto, IDProveedor, FechaAlta, FechaBaja FROM Producto_Proveedor");
                datos.EjecutarLectura();
                while (datos.Lector.Read())
                {
                    ProductoProveedor aux = new ProductoProveedor();
                    aux.IDPP = (Int64)datos.Lector["IDPP"];
                    aux.Producto.IdProducto = (Int64)datos.Lector["IDProducto"];
                    aux.Proveedor.IdProveedor = (Int64)datos.Lector["IDProveedor"];
                    aux.FechaAlta = (DateTime)datos.Lector["FechaAlta"];
                    aux.FechaBaja = (DateTime)datos.Lector["FechaBaja"];
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
        public void Agregar(ProductoProveedor nuevo)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.SetearConsulta("INSERT INTO Producto_Proveedor (IDProducto, IDProveedor, FechaAlta) VALUES (@idproducto, @idproveedor, @fechaalta)");
                datos.SetearParametro("@idproducto", nuevo.Producto.IdProducto);
                datos.SetearParametro("@idproveedor", nuevo.Proveedor.IdProveedor);
                datos.SetearParametro("@fechaalta", nuevo.FechaAlta);
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
        public void Modificar(ProductoProveedor modificado)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.SetearConsulta("UPDATE Producto_Proveedor SET IDProducto = @idproducto, IDProveedor = @idproveedor, FechaAlta = @fechaalta, FechaBaja = @fechabaja WHERE IDPP = @idpp ");
                datos.SetearParametro("@idpp", modificado.IDPP);
                datos.SetearParametro("@idproducto", modificado.Producto.IdProducto);
                datos.SetearParametro("@idproveedor", modificado.Proveedor.IdProveedor);
                datos.SetearParametro("@fechaalta", modificado.FechaAlta);
                datos.SetearParametro("@fechabaja", modificado.FechaBaja);
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
