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
                datos.SetearConsulta("SELECT IDCompra, IDUsuario, IDProveedor, Fecha, Total FROM Compras WHERE Estado=1");
                datos.EjecutarLectura();
                while (datos.Lector.Read())
                {
                    Compra aux = new Compra();
                    aux.IdCompra = (Int64)datos.Lector["IDCompra"];
                    aux.Usuario.IdUsuario = (Int64)datos.Lector["IDUsuario"];
                    aux.Proveedor.IdProveedor = (Int64)datos.Lector["IDProveedor"];
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
            DetalleCompraNegocio detalleNeg = new DetalleCompraNegocio();

            try
            {
                datos.SetearConsulta("INSERT INTO Compras(IDUsuario, IDProveedor, Fecha, Total) VALUES (@idusuario, @idproveedor, @fecha, @total); SELECT SCOPE_IDENTITY()");
                datos.SetearParametro("@idusuario", nuevo.Usuario.IdUsuario);
                datos.SetearParametro("@idproveedor", nuevo.Proveedor.IdProveedor);
                datos.SetearParametro("@fecha", nuevo.Fecha);
                datos.SetearParametro("@total", nuevo.Total);
                nuevo.IdCompra = Convert.ToInt64(datos.EjecutarScalar());
                foreach (DetalleCompra item in nuevo.Detalle)
                {
                    item.IdCompra = nuevo.IdCompra;
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
        public bool BajaLogica(Int64 ID)
        {
            AccesoDatos datos = new AccesoDatos();
            DetalleCompraNegocio detComNeg = new DetalleCompraNegocio();
            bool Resultado = false;
            try
            {
                if (detComNeg.BajaLogica(ID))
                {
                    datos.SetearConsulta("UPDATE Compras SET Estado=0 WHERE IDCompra = @idcompra SELECT @@ROWCOUNT");
                    datos.SetearParametro("@idcompra", ID);
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
