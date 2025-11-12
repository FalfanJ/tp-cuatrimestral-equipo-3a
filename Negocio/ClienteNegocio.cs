using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class ClienteNegocio
    {
        public List<Cliente> Listar()
        {
            List<Cliente> lista = new List<Cliente>();
            List<Persona> perList = new List<Persona>();
            AccesoDatos datos = new AccesoDatos();
            PersonaNegocio perNeg = new PersonaNegocio();

            try
            {
                perList = perNeg.Listar();
                datos.SetearConsulta("SELECT IDCliente, IDPersona FROM Clientes WHERE Estado=1");
                datos.EjecutarLectura();
                while (datos.Lector.Read())
                {
                    Cliente aux = new Cliente();
                    aux.IdCliente = (Int64)datos.Lector["IDCliente"];
                    aux.IdPersona = (Int64)datos.Lector["IDPersona"];
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
        public void Agregar(Cliente nuevo)
        {
            AccesoDatos datos = new AccesoDatos();
            PersonaNegocio perNeg = new PersonaNegocio();
            try
            {
                Int64 idPersona = perNeg.AgregarYObtener(nuevo);
                datos.SetearConsulta("INSERT INTO Clientes (IDPersona) VALUES (@idpersona)");
                datos.SetearParametro("@idpersona", idPersona);
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
        public void Modificar(Cliente modificado)
        {
            AccesoDatos datos = new AccesoDatos();
            PersonaNegocio perNeg = new PersonaNegocio();

            try
            {
                perNeg.Modificar(modificado);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
            }
        }
        public bool BajaLogica(Int64 IDPersona, Int64 IDCliente)
        {
            AccesoDatos datos = new AccesoDatos();
            PersonaNegocio perNeg = new PersonaNegocio();
            bool Resultado = false;

            try
            {
                if (perNeg.BajaLogica(IDPersona))
                {
                    datos.SetearConsulta("UPDATE Clientes SET Estado=0 WHERE IDCliente = @idcliente SELECT @@ROWCOUNT");
                    datos.SetearParametro("@idcliente", IDCliente);
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
