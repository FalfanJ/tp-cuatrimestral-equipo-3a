using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class HistorialMovimientoNegocio
    {
        public List<HistorialMovimiento> Listar()
        {
            List<HistorialMovimiento> lista = new List<HistorialMovimiento>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.SetearConsulta("SELECT IDHistorial, IDProducto, IDVenta, IDCompra, IDUsuario, StockAnterior, StockPosterior, Fecha FROM HistorialMovimiento WHERE Estado=1");
                datos.EjecutarLectura();
                while (datos.Lector.Read())
                {
                    HistorialMovimiento aux = new HistorialMovimiento();
                    aux.IdHistorial = (Int64)datos.Lector["IDHistorial"];
                    aux.Producto.IdProducto = (Int64)datos.Lector["IDProducto"];

                    if (!(datos.Lector["IDVenta"] is DBNull))
                        aux.Venta.IdVenta = (Int64)datos.Lector["IDVenta"];

                    if (!(datos.Lector["IDCompra"] is DBNull))
                        aux.Compra.IdCompra = (Int64)datos.Lector["IDCompra"];

                    aux.Usuario.IdUsuario= (Int64)datos.Lector["IDUsuario"];
                    aux.StockAnterior = (Int16)datos.Lector["StockAnterior"];
                    aux.StockPosterior = (Int16)datos.Lector["StockPosterior"];
                    aux.Fecha = (DateTime)datos.Lector["Fecha"];
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
        public void Agregar(HistorialMovimiento nuevo)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.SetearConsulta("INSERT INTO HistorialMovimiento (IDProducto, IDVenta, IDCompra, StockAnterior, StockPosterior, Fecha, IDUsuario) VALUES (@idproducto, @idventa, @idcompra, @stockanterior, @stockposterior, @fecha, @idusuario)");
                datos.SetearParametro("@idproducto", nuevo.Producto.IdProducto);
                datos.SetearParametro("@idventa", nuevo.Venta.IdVenta);
                datos.SetearParametro("@idcompra", nuevo.Compra.IdCompra);
                datos.SetearParametro("@stockanterior", nuevo.StockAnterior);
                datos.SetearParametro("@stockposterior", nuevo.StockPosterior);
                datos.SetearParametro("@fecha", nuevo.Fecha);
                datos.SetearParametro("@idusuario", nuevo.Usuario.IdUsuario);
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
        public void Modificar(HistorialMovimiento modificado)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.SetearConsulta("UPDATE HistorialMovimiento SET IDProducto = @idproducto, IDVenta = @idventa, IDCompra = @idcompra, StockAnterior = @stockanterior, StockPosterior = @stockposterior, Fecha = @fecha, IDUsuario = @idusuario WHERE IDHistorial = @idhistorial");
                datos.SetearParametro("@idhistorial", modificado.IdHistorial);
                datos.SetearParametro("@idproducto", modificado.Producto.IdProducto);
                datos.SetearParametro("@idventa", modificado.Venta.IdVenta);
                datos.SetearParametro("@idcompra", modificado.Compra.IdCompra);
                datos.SetearParametro("@stockanterior", modificado.StockAnterior);
                datos.SetearParametro("@stockposterior", modificado.StockPosterior);
                datos.SetearParametro("@fecha", modificado.Fecha);
                datos.SetearParametro("@idusuario", modificado.Usuario.IdUsuario);
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
                datos.SetearConsulta("UPDATE HistorialMovimiento SET Estado=0 WHERE IDHistorial = @idhistorial SELECT @@ROWCOUNT\r\n");
                datos.SetearParametro("@idhistorial", ID);
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
