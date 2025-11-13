using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Negocio
{
    public class ClienteNegocio
    {
        // LISTAR: solo clientes activos (Estado = 1)
        public List<Cliente> Listar()
        {
            List<Cliente> lista = new List<Cliente>();
            List<Persona> perList = new List<Persona>();
            AccesoDatos datos = new AccesoDatos();
            PersonaNegocio perNeg = new PersonaNegocio();

            try
            {
                perList = perNeg.Listar(); // Traemos todas las personas
                datos.SetearConsulta("SELECT IDCliente, IDPersona, Estado FROM Clientes WHERE Estado = 1");
                datos.EjecutarLectura();

                while (datos.Lector.Read())
                {
                    Cliente aux = new Cliente();
                    aux.IdCliente = (Int64)datos.Lector["IDCliente"];
                    aux.IdPersona = (Int64)datos.Lector["IDPersona"];
                    aux.Estado = (bool)datos.Lector["Estado"];

                    // Mapear datos de Persona
                    Persona persona = perList.FirstOrDefault(p => p.IdPersona == aux.IdPersona);
                    if (persona != null)
                    {
                        aux.Nombre = persona.Nombre;
                        aux.Apellido = persona.Apellido;
                        aux.Dni = persona.Dni;
                        aux.Cuit = persona.Cuit;
                        aux.TipoPersona = persona.TipoPersona;
                        aux.Telefono = persona.Telefono;
                        aux.Email = persona.Email;
                        aux.Direccion = persona.Direccion;
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

        // AGREGAR: nuevo cliente con Estado activo
        public void Agregar(Cliente nuevo)
        {
            AccesoDatos datos = new AccesoDatos();
            PersonaNegocio perNeg = new PersonaNegocio();

            try
            {
                // Agregar Persona primero
                Int64 idPersona = perNeg.AgregarYObtener(nuevo);

                // Insertar cliente con Estado = 1 (activo)
                datos.SetearConsulta("INSERT INTO Clientes (IDPersona, Estado) VALUES (@idpersona, 1)");
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

        // MODIFICAR: actualizar datos de Persona
        public void Modificar(Cliente modificado)
        {
            PersonaNegocio perNeg = new PersonaNegocio();

            try
            {
                // Solo se modifica la información de la persona
                perNeg.Modificar(modificado);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // BAJA LOGICA: marca Estado = 0 para cliente y Persona
        public bool BajaLogica(Int64 IDPersona, Int64 IDCliente)
        {
            AccesoDatos datos = new AccesoDatos();
            PersonaNegocio perNeg = new PersonaNegocio();
            bool resultado = false;

            try
            {
                // Baja lógica de Persona
                if (perNeg.BajaLogica(IDPersona))
                {
                    // Baja lógica de Cliente
                    datos.SetearConsulta("UPDATE Clientes SET Estado = 0 WHERE IDCliente = @idcliente");
                    datos.SetearParametro("@idcliente", IDCliente);
                    datos.EjecutarAccion();
                }

                return resultado;
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
