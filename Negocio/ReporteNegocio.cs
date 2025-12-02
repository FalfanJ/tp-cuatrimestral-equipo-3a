using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class ReporteNegocio
    {
        // 1. Cliente que más compró
        public List<Reporte> ObtenerTopClientes()
        {
            List<Reporte> lista = new List<Reporte>();
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.SetearConsulta("SELECT TOP 5 P.Nombre + ' ' + P.Apellido as Cliente, SUM(V.Total) as Total, COUNT(V.IDVenta) as Cantidad FROM Ventas V INNER JOIN Clientes C ON V.IDCliente = C.IDCliente INNER JOIN Personas P ON C.IDPersona = P.IDPersona WHERE V.Estado = 1 GROUP BY P.Nombre, P.Apellido ORDER BY Total DESC");
                datos.EjecutarLectura();

                while (datos.Lector.Read())
                {
                    Reporte aux = new Reporte();
                    aux.NombreLabel = (string)datos.Lector["Cliente"];
                    aux.TotalAcumulado = (decimal)datos.Lector["Total"];
                    aux.CantidadVentas = (int)datos.Lector["Cantidad"]; 
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

        // 2. Vendedor que más vendió
        public List<Reporte> ObtenerTopVendedores()
        {
            List<Reporte> lista = new List<Reporte>();
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.SetearConsulta("SELECT TOP 5 U.NombreUsuario, SUM(V.Total) as Total, COUNT(V.IDVenta) as Cantidad FROM Ventas V INNER JOIN Usuarios U ON V.IDUsuario = U.IDUsuario WHERE V.Estado = 1 GROUP BY U.NombreUsuario ORDER BY Total DESC");
                datos.EjecutarLectura();

                while (datos.Lector.Read())
                {
                    Reporte aux = new Reporte();
                    aux.NombreLabel = (string)datos.Lector["NombreUsuario"];
                    aux.TotalAcumulado = (decimal)datos.Lector["Total"];
                    aux.CantidadVentas = (int)datos.Lector["Cantidad"];
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

        // 3. Producto más vendido 
        public List<Reporte> ObtenerProductosMasVendidos(DateTime desde, DateTime hasta)
        {
            List<Reporte> lista = new List<Reporte>();
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.SetearConsulta("SELECT TOP 5 P.Nombre, SUM(DV.Cantidad) as CantidadVendida FROM Detalle_Venta DV INNER JOIN Ventas V ON DV.IDVenta = V.IDVenta INNER JOIN Productos P ON DV.IDProducto = P.IDProducto WHERE V.Fecha BETWEEN @fechaDesde AND @fechaHasta AND V.Estado = 1 GROUP BY P.Nombre ORDER BY CantidadVendida DESC");
                datos.SetearParametro("@fechaDesde", desde);
                datos.SetearParametro("@fechaHasta", hasta);
                datos.EjecutarLectura();

                while (datos.Lector.Read())
                {
                    Reporte aux = new Reporte();
                    aux.NombreProducto = (string)datos.Lector["Nombre"];
                   
                    aux.CantidadVentas = Convert.ToInt32(datos.Lector["CantidadVendida"]);
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

        // 4. Productos Stock Crítico 
        public List<Reporte> ObtenerStockCritico()
        {
            List<Reporte> lista = new List<Reporte>();
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.SetearConsulta("SELECT Nombre, StockActual, StockMinimo FROM Productos WHERE StockActual <= StockMinimo AND Estado = 1");
                datos.EjecutarLectura();

                while (datos.Lector.Read())
                {
                    Reporte aux = new Reporte();
                    aux.NombreProducto = (string)datos.Lector["Nombre"];
                    aux.StockActual = Convert.ToInt32(datos.Lector["StockActual"]);
                    aux.StockMinimo = Convert.ToInt32(datos.Lector["StockMinimo"]);
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

        // 5. Productos con Exceso de Stock 
        public List<Reporte> ObtenerExcesoStock()
        {
            List<Reporte> lista = new List<Reporte>();
            AccesoDatos datos = new AccesoDatos();
            try
            {
                string consulta = @"
                    SELECT Nombre, StockActual, StockMinimo 
                    FROM Productos P 
                    WHERE P.StockActual > (P.StockMinimo * 3) 
                    AND P.IDProducto NOT IN (
                        SELECT DISTINCT DV.IDProducto 
                        FROM Detalle_Venta DV 
                        INNER JOIN Ventas V ON DV.IDVenta = V.IDVenta 
                        WHERE V.Fecha > DATEADD(day, -30, GETDATE())
                    ) 
                    AND P.Estado = 1 
                    ORDER BY StockActual DESC";

                datos.SetearConsulta(consulta);
                datos.EjecutarLectura();

                while (datos.Lector.Read())
                {
                    Reporte aux = new Reporte();
                    aux.NombreProducto = (string)datos.Lector["Nombre"];
                    aux.StockActual = Convert.ToInt32(datos.Lector["StockActual"]);
                    aux.StockMinimo = Convert.ToInt32(datos.Lector["StockMinimo"]);
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

        // 6. Margen de Ganancia por Categoría
        public List<Reporte> ObtenerMargenPorCategoria()
        {
            List<Reporte> lista = new List<Reporte>();
            AccesoDatos datos = new AccesoDatos();
            try
            {
                string consulta = @"
                    SELECT C.Categoria, 
                           SUM(DV.PrecioParcial - (DV.PrecioParcial / (1 + (CAST(DV.PorcentajeGanancia AS decimal(16,3))/100)))) as GananciaNeta
                    FROM Detalle_Venta DV
                    INNER JOIN Productos P ON DV.IDProducto = P.IDProducto
                    INNER JOIN Categorias C ON P.IDCategoria = C.IDCategoria
                    INNER JOIN Ventas V ON DV.IDVenta = V.IDVenta
                    WHERE V.Estado = 1
                    GROUP BY C.Categoria";

                datos.SetearConsulta(consulta);
                datos.EjecutarLectura();

                while (datos.Lector.Read())
                {
                    Reporte aux = new Reporte();
                    aux.Categoria = (string)datos.Lector["Categoria"];

                    if (datos.Lector["GananciaNeta"] != DBNull.Value)
                    {
                        aux.GananciaTotal = Math.Round((decimal)datos.Lector["GananciaNeta"], 2);
                    }
                    else
                    {
                        aux.GananciaTotal = 0;
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
    }
}