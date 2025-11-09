using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class ProductoNegocio
    {
        public List<Producto> Listar()
        {
            List<Producto> lista = new List<Producto>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.SetearConsulta("SELECT P.IDProducto, P.NumeroSerie, M.Marca AS 'Marca', C.Categoria AS 'Categoria', P.Nombre, P.Precio, P.StockActual, P.StockMinimo, P.PorcentajeGanancia, P.Modelo, P.Descripcion, P.IDMarca, P.IDCategoria FROM Productos P LEFT JOIN Marcas M ON P.IDMarca = M.IDMarca LEFT JOIN Categorias C ON P.IDCategoria = C.IDCategoria");
                datos.EjecutarLectura();
                while (datos.Lector.Read())
                {
                    Producto aux = new Producto();
                    aux.IdProducto = (Int64)datos.Lector["IDProducto"];
                    aux.NSerie = (string)datos.Lector["NumeroSerie"];
                    aux.Nombre = (string)datos.Lector["Nombre"];
                    aux.Precio = (int)datos.Lector["Precio"];
                    aux.Stock = (int)datos.Lector["StockActual"];
                    aux.StockMinimo = (int)datos.Lector["StockMinimo"];
                    aux.PorcentajeGanancia = (int)datos.Lector["PorcentajeGanancia"];

                    if (!(datos.Lector["Modelo"] is DBNull))
                        aux.Modelo = (string)datos.Lector["Modelo"];
                    
                    if(!(datos.Lector["Descripcion"] is DBNull))
                        aux.Descripcion = (string)datos.Lector["Descripcion"];

                    if (!(datos.Lector["Marca"] is DBNull))
                    {
                        aux.Marca = new Marca();
                        aux.Marca.IdMarca = (int)datos.Lector["IDMarca"];
                        aux.Marca.Nombre = (string)datos.Lector["Marca"];
                    }

                    if (!(datos.Lector["Categoria"] is DBNull))
                    {
                        aux.Categoria = new Categoria();
                        aux.Categoria.IdCategoria = (int)datos.Lector["IDCategoria"];
                        aux.Categoria.Nombre = (string)datos.Lector["Categoria"];
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
        public void Agregar(Producto nuevo)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.SetearConsulta("INSERT INTO Productos (NumeroSerie, IDMarca, IDCategoria, Nombre, Precio, StockActual, StockMinimo, PorcentajeGanancia, Modelo, Descripcion) VALUES (@numeroserie, @idmarca, @idcategoria, @nombre, @precio, @stockactual, @stockminimo, @porcentajeganancia, @modelo, @descripcion)");
                datos.SetearParametro("@numeroserie", nuevo.NSerie);
                datos.SetearParametro("@idmarca", nuevo.Marca.IdMarca);
                datos.SetearParametro("@idcategoria", nuevo.Categoria.IdCategoria);
                datos.SetearParametro("@nombre", nuevo.Nombre);
                datos.SetearParametro("@precio", nuevo.Precio);
                datos.SetearParametro("@stockactual", nuevo.Stock);
                datos.SetearParametro("@stockminimo", nuevo.StockMinimo);
                datos.SetearParametro("@porcentajeganancia", nuevo.PorcentajeGanancia);
                datos.SetearParametro("@modelo", nuevo.Modelo);
                datos.SetearParametro("@descripcion", nuevo.Descripcion);
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
        public void Modificar(Producto modificado)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.SetearConsulta("UPDATE Productos SET NumeroSerie = @numeroserie, IDMarca = @idmarca, IDCategoria = @idcategoria, Nombre = @nombre, Precio = @precio, StockActual = @stockactual, StockMinimo = @stockminimo, PorcentajeGanancia = @porcentajeganancia, Modelo = @modelo, Descripcion = @descripcion WHERE IDProducto = @idproducto");
                datos.SetearParametro("@idproducto", modificado.IdProducto);
                datos.SetearParametro("@numeroserie", modificado.NSerie);
                datos.SetearParametro("@idmarca", modificado.Marca.IdMarca);
                datos.SetearParametro("@idcategoria", modificado.Categoria.IdCategoria);
                datos.SetearParametro("@nombre", modificado.Nombre);
                datos.SetearParametro("@precio", modificado.Precio);
                datos.SetearParametro("@stockactual", modificado.Stock);
                datos.SetearParametro("@stockminimo", modificado.StockMinimo);
                datos.SetearParametro("@porcentajeganancia", modificado.PorcentajeGanancia);
                datos.SetearParametro("@modelo", modificado.Modelo);
                datos.SetearParametro("@descripcion", modificado.Descripcion);
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
