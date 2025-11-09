using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class CompraNegocio
    {
        public List<Compra> Listar()
        {
            List<Compra> lista = new List<Compra>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.SetearConsulta("SELECT IDCompra, IDUsuario, IDProveedor, Fecha, Total FROM Compras");
                datos.EjecutarLectura();
                while (datos.Lector.Read())
                {
                    Compra aux = new Compra();
                    aux.IdCompra = (int)datos.Lector["IDCompra"];
                    aux.Usuario.IdUsuario = (int)datos.Lector["IDUsuario"];
                    aux.Proveedor.IdProveedor = (int)datos.Lector["IDProveedor"];
                    aux.Fecha = (DateTime)datos.Lector["Fecha"];
                    aux.Total = (int)datos.Lector["Total"];
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
        public void Agregar(Compra nuevo)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.SetearConsulta("INSERT INTO Compras(IDUsuario, IDProveedor, Fecha, Total) VALUES (@idusuario, @idproveedor, @fecha, @total)");
                datos.SetearParametro("@idusuario", nuevo.Usuario.IdUsuario);
                datos.SetearParametro("@idproveedor", nuevo.Proveedor.IdProveedor);
                datos.SetearParametro("@fecha", nuevo.Fecha);
                datos.SetearParametro("@total", nuevo.Total);
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
        public void Modificar(Compra modificado)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.SetearConsulta("UPDATE Compras SET IDUsuario = @idusuario, IDProveedor = @idproveedor, Fecha = @fecha, Total = @total WHERE IDCompra = @idcompra");
                datos.SetearParametro("@idcompra", modificado.IdCompra);
                datos.SetearParametro("@idusuario", modificado.Usuario.IdUsuario);
                datos.SetearParametro("@idproveedor", modificado.Proveedor.IdProveedor);
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
