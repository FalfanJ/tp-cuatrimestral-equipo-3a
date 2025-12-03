using Dominio;
using System;
using System.Collections.Generic;

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
                datos.SetearConsulta(
        @"SELECT c.IDCompra,
       c.Fecha,
       c.Total,
       p.IDProveedor,
       p.Nombre AS ProveedorNombre,
       u.IDUsuario,
       u.Email AS email,
       c.Estado
FROM Compras c
INNER JOIN Proveedor p ON p.IDProveedor = c.IDProveedor
INNER JOIN Usuarios u ON u.IDUsuario = c.IDUsuario
WHERE c.Estado = 1
ORDER BY c.IDCompra DESC");


                datos.EjecutarLectura();

                // Instancia de negocio para traer detalles
                DetalleCompraNegocio detalleNegocio = new DetalleCompraNegocio();

                while (datos.Lector.Read())
                {
                    Compra compra = new Compra
                    {
                        IdCompra = (long)datos.Lector["IDCompra"],
                        Fecha = (DateTime)datos.Lector["Fecha"],
                        Total = (decimal)datos.Lector["Total"],

                        Proveedor = new Proveedor
                        {
                            IdProveedor = (long)datos.Lector["IDProveedor"],
                            Nombre = datos.Lector["ProveedorNombre"].ToString()
                        },

                        Usuario = new Usuario
                        {
                            IdUsuario = (long)datos.Lector["IDUsuario"],
                            email = datos.Lector["email"].ToString()
                        }
                    };

                    // 🔥 Aca cargamos el detalle de la compra
                    compra.Detalle = detalleNegocio.ListarPorCompra(compra.IdCompra);

                    lista.Add(compra);
                }

                datos.CerrarConexion();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al cargar compras: " + ex.Message);
            }

            return lista;
        }







        public void Agregar(Compra nuevo)
        {
            if (nuevo == null) throw new ArgumentNullException(nameof(nuevo));
            if (nuevo.Proveedor == null || nuevo.Proveedor.IdProveedor <= 0) throw new ArgumentException("Proveedor inválido.");
            if (nuevo.Usuario == null || nuevo.Usuario.IdUsuario <= 0) throw new ArgumentException("Usuario inválido.");
            if (nuevo.Detalle == null || nuevo.Detalle.Count == 0) throw new ArgumentException("Debe agregar al menos un producto.");

            AccesoDatos datos = new AccesoDatos();

            try
            {
                // Calcular total
                decimal total = 0;
                foreach (var d in nuevo.Detalle)
                    total += d.Cantidad * d.PrecioUnitario;
                nuevo.Total = total;

                // Insertar compra (CABECERA)
                datos.SetearConsulta(
                    @"INSERT INTO Compras (IDUsuario, IDProveedor, Fecha, Total, Estado)
                      VALUES (@idusuario, @idproveedor, @fecha, @total, 1);
                      SELECT CAST(SCOPE_IDENTITY() AS BIGINT);"
                );

                datos.SetearParametro("@idusuario", nuevo.Usuario.IdUsuario);
                datos.SetearParametro("@idproveedor", nuevo.Proveedor.IdProveedor);
                datos.SetearParametro("@fecha", nuevo.Fecha);
                datos.SetearParametro("@total", nuevo.Total);

                nuevo.IdCompra = Convert.ToInt64(datos.EjecutarScalar());

                // ===============================
                // INSERTAR DETALLE (Detalle_Compra)
                // ===============================

                foreach (var det in nuevo.Detalle)
                {
                    datos = new AccesoDatos();

                    datos.SetearConsulta(
                        @"INSERT INTO Detalle_Compra 
                          (IDCompra, IDProducto, Cantidad, PrecioUnitario, PrecioParcial, Estado)
                          VALUES (@idcompra, @idproducto, @cantidad, @preciounitario, @precioparcial, 1);"
                    );

                    datos.SetearParametro("@idcompra", nuevo.IdCompra);
                    datos.SetearParametro("@idproducto", det.Producto.IdProducto);
                    datos.SetearParametro("@cantidad", det.Cantidad);
                    datos.SetearParametro("@preciounitario", det.PrecioUnitario);
                    datos.SetearParametro("@precioparcial", det.Cantidad * det.PrecioUnitario);

                    datos.EjecutarAccion();

                }
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
                datos.SetearConsulta(
                    @"UPDATE Compras 
                      SET IDUsuario=@idusuario, IDProveedor=@idproveedor, Fecha=@fecha, Total=@total
                      WHERE IDCompra=@idcompra"
                );
                datos.SetearParametro("@idcompra", modificado.IdCompra);
                datos.SetearParametro("@idusuario", modificado.Usuario.IdUsuario);
                datos.SetearParametro("@idproveedor", modificado.Proveedor.IdProveedor);
                datos.SetearParametro("@fecha", modificado.Fecha);
                datos.SetearParametro("@total", modificado.Total);
                datos.EjecutarAccion();
            }
            finally
            {
                datos.CerrarConexion();
            }
        }

        public bool BajaLogica(long ID)
        {
            AccesoDatos datos = new AccesoDatos();
            DetalleCompraNegocio detComNeg = new DetalleCompraNegocio();
            bool resultado = false;

            try
            {
                if (detComNeg.BajaLogica(ID))
                {
                    datos.SetearConsulta(
                        "UPDATE Compras SET Estado=0 WHERE IDCompra=@idcompra; SELECT @@ROWCOUNT;"
                    );
                    datos.SetearParametro("@idcompra", ID);
                    resultado = Convert.ToInt32(datos.EjecutarScalar()) > 0;
                }
                return resultado;
            }
            finally
            {
                datos.CerrarConexion();
            }
        }
    }
}
