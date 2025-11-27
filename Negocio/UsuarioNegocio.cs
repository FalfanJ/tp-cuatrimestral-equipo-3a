using Dominio;
using System;
using System.Collections.Generic;

namespace Negocio
{
    public class UsuarioNegocio
    {
        public List<Usuario> Listar()
        {
            List<Usuario> lista = new List<Usuario>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.SetearConsulta("SELECT IDUsuario, TipoUsuario, NombreUsuario, Email, Contrasenia FROM Usuarios WHERE Estado = 1");
                datos.EjecutarLectura();

                while (datos.Lector.Read())
                {
                    Usuario aux = new Usuario
                    {
                        IdUsuario = (long)datos.Lector["IDUsuario"],
                        TipoUsuario = (string)datos.Lector["TipoUsuario"],
                        NombreUsuario = (string)datos.Lector["NombreUsuario"],
                        email = (string)datos.Lector["Email"],
                        Contrasenia = (string)datos.Lector["Contrasenia"]
                    };
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

            try
            {
                datos.SetearConsulta("INSERT INTO Usuarios (TipoUsuario, NombreUsuario, Email, Contrasenia, Estado) VALUES (@tipo, @nombre, @correo, @pass, 1)");
                datos.SetearParametro("@tipo", nuevo.TipoUsuario);
                datos.SetearParametro("@nombre", nuevo.NombreUsuario);
                datos.SetearParametro("@correo", nuevo.email);
                datos.SetearParametro("@pass", nuevo.Contrasenia);
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

            try
            {
                datos.SetearConsulta("UPDATE Usuarios SET TipoUsuario = @tipo, NombreUsuario = @nombre, Email = @correo, Contrasenia = @pass WHERE IDUsuario = @id");
                datos.SetearParametro("@id", modificado.IdUsuario);
                datos.SetearParametro("@tipo", modificado.TipoUsuario);
                datos.SetearParametro("@nombre", modificado.NombreUsuario);
                datos.SetearParametro("@correo", modificado.email);
                datos.SetearParametro("@pass", modificado.Contrasenia);
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

        public bool Ingreso(string email, string contrasenia)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.SetearConsulta("SELECT COUNT(*) FROM Usuarios WHERE Email = @email AND Contrasenia = @pass AND Estado = 1");
                datos.SetearParametro("@email", email);
                datos.SetearParametro("@pass", contrasenia);

                int resultado = Convert.ToInt32(datos.EjecutarScalar());
                return resultado > 0;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                datos.CerrarConexion();
            }
        }

        public Usuario Login(string email, string contrasenia)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                // ❌ SACADO INNER JOIN y uso de p.Email
                datos.SetearConsulta("SELECT IDUsuario, TipoUsuario, NombreUsuario, Email, Contrasenia FROM Usuarios WHERE Email = @correo AND Contrasenia = @pass AND Estado = 1");
                datos.SetearParametro("@correo", email);
                datos.SetearParametro("@pass", contrasenia);
                datos.EjecutarLectura();

                if (datos.Lector.Read())
                {
                    return new Usuario
                    {
                        IdUsuario = (long)datos.Lector["IDUsuario"],
                        TipoUsuario = (string)datos.Lector["TipoUsuario"],
                        NombreUsuario = (string)datos.Lector["NombreUsuario"],
                        email = (string)datos.Lector["Email"],
                        Contrasenia = (string)datos.Lector["Contrasenia"]
                    };
                }

                return null;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                datos.CerrarConexion();
            }
        }

        public bool BajaLogica(long idUsuario)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.SetearConsulta("UPDATE Usuarios SET Estado = 0 WHERE IDUsuario = @id");
                datos.SetearParametro("@id", idUsuario);
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
