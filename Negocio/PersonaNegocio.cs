using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class PersonaNegocio
    {
        public List<Persona> Listar()
        {
            List<Persona> lista = new List<Persona>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.SetearConsulta("SELECT IDPersona, Nombre, Apellido, DNI, CUIT, TipoPersona, Telefono, Email, Direccion FROM Personas");
                datos.EjecutarLectura();
                while (datos.Lector.Read())
                {
                    Persona aux = new Persona();
                    aux.IdPersona = (Int64)datos.Lector["IDPersona"];
                    aux.Nombre= (string)datos.Lector["Nombre"];
                    aux.Apellido= (string)datos.Lector["Apellido"];
                    aux.Dni= (int)datos.Lector["DNI"];
                    aux.Cuit= (int)datos.Lector["CUIT"];
                    aux.TipoPersona= (bool)datos.Lector["TipoPersona"];
                    aux.Telefono= (int)datos.Lector["Telefono"];
                    aux.Email= (string)datos.Lector["Email"];
                    aux.Direccion= (string)datos.Lector["Direccion"];

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
        public void Agregar(Persona nuevo)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.SetearConsulta("INSERT INTO Personas (Nombre, Apellido, DNI, CUIT, TipoPersona, Telefono, Email, Direccion) VALUES (@nombre, @apellido, @dni, @cuit, @tipopersona, @telefono, @email, @direccion)");
                datos.SetearParametro("@nombre", nuevo.Nombre);
                datos.SetearParametro("@apellido", nuevo.Apellido);
                datos.SetearParametro("@dni", nuevo.Dni);
                datos.SetearParametro("@cuit", nuevo.Cuit);
                datos.SetearParametro("@tipopersona", nuevo.TipoPersona);
                datos.SetearParametro("@telefono", nuevo.Telefono);
                datos.SetearParametro("@email", nuevo.Email);
                datos.SetearParametro("@direccion", nuevo.Direccion);
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
        public Int64 AgregarYObtener (Persona nuevo)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.SetearConsulta("INSERT INTO Personas (Nombre, Apellido, DNI, CUIT, TipoPersona, Telefono, Email, Direccion) VALUES (@nombre, @apellido, @dni, @cuit, @tipopersona, @telefono, @email, @direccion); SELECT SCOPE_IDENTITY()");
                datos.SetearParametro("@nombre", nuevo.Nombre);
                datos.SetearParametro("@apellido", nuevo.Apellido);
                datos.SetearParametro("@dni", nuevo.Dni);
                datos.SetearParametro("@cuit", nuevo.Cuit);
                datos.SetearParametro("@tipopersona", nuevo.TipoPersona);
                datos.SetearParametro("@telefono", nuevo.Telefono);
                datos.SetearParametro("@email", nuevo.Email);
                datos.SetearParametro("@direccion", nuevo.Direccion);

                Int64 idPersona = Convert.ToInt64(datos.EjecutarScalar());
                return idPersona;
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
        public void Modificar(Persona modificado)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.SetearConsulta("UPDATE Personas SET Nombre = @nombre, Apellido = @apellido, DNI = @dni, CUIT = @cuit, TipoPersona = @tipopersona, Telefono = @telefono, Email = @email, Direccion = @direccion WHERE IDPersona = @idpersona");
                datos.SetearParametro("@idpersona", modificado.IdPersona);
                datos.SetearParametro("@nombre", modificado.Nombre);
                datos.SetearParametro("@apellido", modificado.Apellido);
                datos.SetearParametro("@dni", modificado.Dni);
                datos.SetearParametro("@cuit", modificado.Cuit);
                datos.SetearParametro("@tipopersona", modificado.TipoPersona);
                datos.SetearParametro("@telefono", modificado.Telefono);
                datos.SetearParametro("@email", modificado.Email);
                datos.SetearParametro("@direccion", modificado.Direccion);
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
