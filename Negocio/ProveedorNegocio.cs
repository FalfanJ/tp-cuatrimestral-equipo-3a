using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class ProveedorNegocio
    {
        public List<Proveedor> Listar()
        {
            List<Proveedor> lista = new List<Proveedor>();
            List<Persona> perList = new List<Persona>();
            AccesoDatos datos = new AccesoDatos();
            PersonaNegocio perNeg = new PersonaNegocio();

            try
            {
                perList = perNeg.Listar();
                datos.SetearConsulta("SELECT IDProveedor, IDPersona, RazonSocial FROM Proveedor");
                datos.EjecutarLectura();
                while (datos.Lector.Read())
                {
                    Proveedor aux = new Proveedor();
                    aux.IdProveedor = (Int64)datos.Lector["IDProveedor"];
                    aux.IdPersona = (Int64)datos.Lector["IDPersona"];
                    aux.RazonSocial = (string)datos.Lector["RazonSocial"];
                    foreach (Persona item in perList)
                    {
                        if (item.IdPersona == aux.IdPersona)
                        {
                            aux.Nombre = item.Nombre;
                            aux.Apellido = item.Apellido;
                            aux.Dni = item.Dni;
                            aux.Cuit = item.Cuit;
                            aux.TipoPersona = item.TipoPersona;
                            aux.Telefono = item.Telefono;
                            aux.Email = item.Email;
                            aux.Direccion = item.Direccion;
                        }
                    }
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
        public void Agregar(Proveedor nuevo)
        {
            AccesoDatos datos = new AccesoDatos();
            PersonaNegocio perNeg = new PersonaNegocio();
            try
            {
                Int64 idPersona = perNeg.AgregarYObtener(nuevo);
                datos.SetearConsulta("INSERT INTO Proveedor(IDPersona, RazonSocial) VALUES (@idpersona, @razonsocial)");
                datos.SetearParametro("@idpersona", idPersona);
                datos.SetearParametro("@razonsocial", nuevo.RazonSocial);
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
        public void Modificar(Proveedor modificado)
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
