using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class VentaNegocio
    {
        public List<Venta> Listar()
        {
            List<Venta> lista = new List<Venta>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.SetearConsulta("SELECT IDVenta, IDCliente, IDUsuario, NFactura, Fecha, Total FROM Ventas WHERE Estado=1");
                datos.EjecutarLectura();
                while (datos.Lector.Read())
                {
                    Venta aux = new Venta();
                    aux.IdVenta = (Int64)datos.Lector["IDVenta"];
                    aux.Cliente.IdCliente = (Int64)datos.Lector["IDCliente"];
                    aux.Usuario.IdUsuario = (Int64)datos.Lector["IDUsuario"];
                    aux.NFactura = (string)datos.Lector["NFactura"];
                    aux.Fecha = (DateTime)datos.Lector["Fecha"];
                    aux.Total = (decimal)datos.Lector["Total"];
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
        public void Agregar(Venta nuevo)
        {
            AccesoDatos datos = new AccesoDatos();
            DetalleVentaNegocio detalleNeg = new DetalleVentaNegocio();

            try
            {
                datos.SetearConsulta("INSERT INTO Ventas(IDCliente, IDUsuario, NFactura, Fecha, Total) VALUES (@idcliente, @idusuario, @nfactura, @fecha, @total); SELECT SCOPE_IDENTITY()");
                datos.SetearParametro("@idcliente", nuevo.Cliente.IdCliente);
                datos.SetearParametro("@idusuario", nuevo.Usuario.IdUsuario);
                datos.SetearParametro("@nfactura", nuevo.NFactura);
                datos.SetearParametro("@fecha", nuevo.Fecha);
                datos.SetearParametro("@total", nuevo.Total);
                nuevo.IdVenta = Convert.ToInt64(datos.EjecutarScalar());
                foreach (DetalleVenta item in nuevo.Detalle)
                {
                    item.IdVenta = nuevo.IdVenta;
                }
                detalleNeg.Agregar(nuevo.Detalle);
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
        public void Modificar(Venta modificado)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.SetearConsulta("UPDATE Ventas SET IDCliente = @idcliente, IDUsuario = @idusuario, NFactura = @nfactura, Fecha = @fecha, Total = @total WHERE IDVenta = @idventa");
                datos.SetearParametro("@idventa", modificado.IdVenta);
                datos.SetearParametro("@idcliente", modificado.Cliente.IdCliente);
                datos.SetearParametro("@idusuario", modificado.Usuario.IdUsuario);
                datos.SetearParametro("@nfactura", modificado.NFactura);
                datos.SetearParametro("@fecha", modificado.Fecha);
                datos.SetearParametro("@total", modificado.Total);
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
            DetalleVentaNegocio detVenNeg = new DetalleVentaNegocio();
            bool Resultado = false;
            try
            {
                if (detVenNeg.BajaLogica(ID))
                {
                    datos.SetearConsulta("UPDATE Ventas SET Estado=0 WHERE IDVenta = @idventa SELECT @@ROWCOUNT");
                    datos.SetearParametro("@idventa", ID);
                    Resultado = Convert.ToBoolean(datos.EjecutarScalar());
                }
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
