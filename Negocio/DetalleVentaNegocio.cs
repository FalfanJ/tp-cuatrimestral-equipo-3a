using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class DetalleVentaNegocio
    {
        public List<DetalleVenta> Listar()
        {
            List<DetalleVenta> lista = new List<DetalleVenta>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.SetearConsulta("SELECT IDVenta, IDProducto, Cantidad, PrecioUnitario, PrecioParcial, ProcentajeGanancia FROM Detalle_Venta");
                datos.EjecutarLectura();
                while (datos.Lector.Read())
                {
                    DetalleVenta aux = new DetalleVenta();
                    aux.IdVenta = (Int64)datos.Lector["IDVenta"];
                    aux.Producto.IdProducto = (Int64)datos.Lector["IDProducto"];
                    aux.Cantidad = (int)datos.Lector["Cantidad"];
                    aux.PrecioUnitario = (int)datos.Lector["PrecioUnitario"];
                    aux.PrecioParcial= (int)datos.Lector["PrecioParcial"];
                    aux.PorcentajeGanancia = (int)datos.Lector["ProcentajeGanancia"];
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
        public void Agregar(DetalleVenta nuevo)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.SetearConsulta("INSERT INTO Detalle_Venta (IDVenta, IDProducto, Cantidad, PrecioUnitario, PrecioParcial, ProcentajeGanancia) VALUES (@idventa, @idproducto, @cantidad, @preciounitario, @precioparcial, @procentajeganancia)");
                datos.SetearParametro("@idventa", nuevo.IdVenta);
                datos.SetearParametro("@idproducto", nuevo.Producto.IdProducto);
                datos.SetearParametro("@cantidad", nuevo.Cantidad);
                datos.SetearParametro("@preciounitario", nuevo.PrecioUnitario);
                datos.SetearParametro("@precioparcial", nuevo.PrecioParcial);
                datos.SetearParametro("@procentajeganancia", nuevo.PorcentajeGanancia);
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
        public void Modificar(DetalleVenta modificado)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.SetearConsulta("UPDATE Detalle_Venta SET Cantidad = @cantidad, PrecioUnitario = @preciounitario, PrecioParcial = @precioparcial, ProcentajeGanancia = @procentajeganancia WHERE IDVenta = @idventa AND IDProducto = @idproducto");
                datos.SetearParametro("@idventa", modificado.IdVenta);
                datos.SetearParametro("@idproducto", modificado.Producto.IdProducto);
                datos.SetearParametro("@cantidad", modificado.Cantidad);
                datos.SetearParametro("@preciounitario", modificado.PrecioUnitario);
                datos.SetearParametro("@precioparcial", modificado.PrecioParcial);
                datos.SetearParametro("@procentajeganancia", modificado.PorcentajeGanancia);
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
