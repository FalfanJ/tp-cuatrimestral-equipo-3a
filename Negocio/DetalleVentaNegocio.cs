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
                datos.SetearConsulta("SELECT IDVenta, IDProducto, Cantidad, PrecioUnitario, PrecioParcial, ProcentajeGanancia FROM Detalle_Venta WHERE Estado=1");
                datos.EjecutarLectura();
                while (datos.Lector.Read())
                {
                    DetalleVenta aux = new DetalleVenta();
                    aux.IdVenta = (Int64)datos.Lector["IDVenta"];
                    aux.Producto.IdProducto = (Int64)datos.Lector["IDProducto"];
                    aux.Cantidad = (Int16)datos.Lector["Cantidad"];
                    aux.PrecioUnitario = (decimal)datos.Lector["PrecioUnitario"];
                    aux.PrecioParcial = (decimal)datos.Lector["PrecioParcial"];
                    aux.PorcentajeGanancia = (Int16)datos.Lector["ProcentajeGanancia"];
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
        public void Agregar(List<DetalleVenta> nuevo)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.SetearConsulta("INSERT INTO Detalle_Venta (IDVenta, IDProducto, Cantidad, PrecioUnitario, PrecioParcial, ProcentajeGanancia) VALUES (@idventa, @idproducto, @cantidad, @preciounitario, @precioparcial, @procentajeganancia)");
                foreach (DetalleVenta item in nuevo)
                {
                    datos.SetearParametro("@idventa", item.IdVenta);
                    datos.SetearParametro("@idproducto", item.Producto.IdProducto);
                    datos.SetearParametro("@cantidad", item.Cantidad);
                    datos.SetearParametro("@preciounitario", item.PrecioUnitario);
                    datos.SetearParametro("@precioparcial", item.PrecioParcial);
                    datos.SetearParametro("@procentajeganancia", item.PorcentajeGanancia);
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
        public bool BajaLogica(Int64 ID)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.SetearConsulta("UPDATE Detalle_Venta SET Estado=0 WHERE IDVenta = @idventa SELECT @@ROWCOUNT");
                datos.SetearParametro("@idventa", ID);
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
