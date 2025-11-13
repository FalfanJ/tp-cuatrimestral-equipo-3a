using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class DetalleCompraNegocio
    {
        public List<DetalleCompra> Listar()
        {
            List<DetalleCompra> lista = new List<DetalleCompra>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.SetearConsulta("SELECT IDCompra, IDProducto, Cantidad, PrecioUnitario, PrecioParcial FROM Detalle_Compra WHERE Estado=1");
                datos.EjecutarLectura();
                while (datos.Lector.Read())
                {
                    DetalleCompra aux = new DetalleCompra();
                    aux.IdCompra = (Int64)datos.Lector["IDCompra"];
                    aux.Producto.IdProducto = (Int64)datos.Lector["IDProducto"];
                    aux.Cantidad = (Int16)datos.Lector["Cantidad"];
                    aux.PrecioUnitario = (decimal)datos.Lector["PrecioUnitario"];
                    aux.PrecioParcial = (decimal)datos.Lector["PrecioParcial"];
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
        public void Agregar(DetalleCompra nuevo)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.SetearConsulta("INSERT INTO Detalle_Compra (IDCompra, IDProducto, Cantidad, PrecioUnitario, PrecioParcial) VALUES (@idcompra, @idproducto, @cantidad, @preciounitario, @precioparcial)");
                datos.SetearParametro("@idcompra", nuevo.IdCompra);
                datos.SetearParametro("@idproducto", nuevo.Producto.IdProducto);
                datos.SetearParametro("@cantidad", nuevo.Cantidad);
                datos.SetearParametro("@preciounitario", nuevo.PrecioUnitario);
                datos.SetearParametro("@precioparcial", nuevo.PrecioParcial);
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
        public void Agregar(List<DetalleCompra> nuevo)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.SetearConsulta("INSERT INTO Detalle_Compra (IDCompra, IDProducto, Cantidad, PrecioUnitario, PrecioParcial) VALUES (@idcompra, @idproducto, @cantidad, @preciounitario, @precioparcial)");
                foreach (DetalleCompra item in nuevo)
                {
                    datos.SetearParametro("@idcompra", item.IdCompra);
                    datos.SetearParametro("@idproducto", item.Producto.IdProducto);
                    datos.SetearParametro("@cantidad", item.Cantidad);
                    datos.SetearParametro("@preciounitario", item.PrecioUnitario);
                    datos.SetearParametro("@precioparcial", item.PrecioParcial);
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
        public void Modificar(DetalleCompra modificado)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.SetearConsulta("UPDATE Detalle_Compra SET Cantidad = @cantidad, PrecioUnitario =  @preciounitario, PrecioParcial = @precioparcial WHERE IDCompra = @idcompra AND IDProducto = @idproducto");
                datos.SetearParametro("@idcompra", modificado.IdCompra);
                datos.SetearParametro("@idproducto", modificado.Producto.IdProducto);
                datos.SetearParametro("@cantidad", modificado.Cantidad);
                datos.SetearParametro("@preciounitario", modificado.PrecioUnitario);
                datos.SetearParametro("@precioparcial", modificado.PrecioParcial);
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
        public bool BajaLogica(Int64 ID)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.SetearConsulta("UPDATE Detalle_Compra SET Estado=0 WHERE IDCompra = @idcompra SELECT @@ROWCOUNT");
                datos.SetearParametro("@idcompra", ID);
                bool Resultado = Convert.ToBoolean(datos.EjecutarScalar());
                return Resultado;
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
