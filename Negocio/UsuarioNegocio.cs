using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class UsuarioNegocio
    {
        public List<Usuario> Listar()
        {
            List<Usuario> lista = new List<Usuario>();
            List<Persona> perList = new List<Persona>();
            AccesoDatos datos = new AccesoDatos();
            PersonaNegocio perNeg = new PersonaNegocio();

            try
            {
                perList = perNeg.Listar();
                datos.SetearConsulta("SELECT IDUsuario, IDPersona, TipoUsuario, NombreUsuario, Contraseña FROM Usuarios");
                datos.EjecutarLectura();
                while (datos.Lector.Read())
                {
                    Usuario aux = new Usuario();
                    aux.IdUsuario = (Int64)datos.Lector["IDUsuario"];
                    aux.IdUsuario = (Int64)datos.Lector["IDPersona"];
                    aux.TipoUsuario = (string)datos.Lector["TipoUsuario"];
                    aux.NombreUsuario = (string)datos.Lector["NombreUsuario"];
                    aux.Contraseña = (string)datos.Lector["Contraseña"];
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
        public void Agregar(Usuario nuevo)
        {
            AccesoDatos datos = new AccesoDatos();
            PersonaNegocio perNeg = new PersonaNegocio();

            try
            {
                Int64 idPersona = perNeg.AgregarYObtener(nuevo);
                datos.SetearConsulta("INSERT INTO Usuarios (IDPersona, TipoUsuario, NombreUsuario, Contraseña) VALUES (@idpersona, @tipousuario, @nombreusuario, @contraseña)");
                datos.SetearParametro("@idpersona", idPersona);
                datos.SetearParametro("@tipousuario", nuevo.TipoUsuario);
                datos.SetearParametro("@nombreusuario", nuevo.NombreUsuario);
                datos.SetearParametro("@contraseña", nuevo.Contraseña);
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
        public void Modificar(Usuario modificado)
        {
            AccesoDatos datos = new AccesoDatos();
            PersonaNegocio perNeg = new PersonaNegocio();

            try
            {
                perNeg.Modificar(modificado);
                datos.SetearConsulta("UPDATE Usuarios SET TipoUsuario = @tipousuario, NombreUsuario = @nombreusuario, Contraseña = @contraseña WHERE IDUsuario = @idusuario");
                datos.SetearParametro("@idusuario", modificado.IdUsuario);
                datos.SetearParametro("@tipousuario", modificado.TipoUsuario);
                datos.SetearParametro("@nombreusuario", modificado.NombreUsuario);
                datos.SetearParametro("@contraseña", modificado.Contraseña);
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
