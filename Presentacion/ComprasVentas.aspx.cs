using Dominio;
using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Presentacion
{
    public partial class WebForm3 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Aquí podrías agregar validación de sesión si es necesario
        }

        protected void btnNuevaVenta_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/SeleccionCliente.aspx");
        }

        // 🟢 1. ABRIR MODAL DE COMPRA Y CARGAR DATOS
        protected void btnNuevaCompra_Click(object sender, EventArgs e)
        {
            try
            {
                CargarProveedores();
                CargarProductosFaltantes();

                // Abrir el modal usando ScriptManager
                ScriptManager.RegisterStartupScript(this, GetType(), "abrirModalCompra", "abrirModalCompra();", true);
            }
            catch (Exception ex)
            {
                MostrarMensaje("Error al cargar datos de compra: " + ex.Message, "danger");
            }
        }

        private void CargarProveedores()
        {
            ProveedorNegocio negocio = new ProveedorNegocio();
            List<Proveedor> lista = negocio.Listar();

            ddlProveedores.DataSource = lista;
            ddlProveedores.DataTextField = "Nombre";
            ddlProveedores.DataValueField = "IdProveedor";
            ddlProveedores.DataBind();

            // Opción por defecto
            ddlProveedores.Items.Insert(0, new ListItem("Seleccione un proveedor", "0"));
        }

        private void CargarProductosFaltantes()
        {
            ProductoNegocio negocio = new ProductoNegocio();
            List<Producto> lista = negocio.Listar();

            // Filtramos productos donde el Stock actual es menor o igual al Stock Mínimo
            // Esto automatiza la sugerencia de qué comprar
            List<Producto> faltantes = lista.Where(p => p.Stock <= p.StockMinimo).ToList();

            gvProductosFaltantes.DataSource = faltantes;
            gvProductosFaltantes.DataBind();
        }

        // 🟢 2. PROCESAR LA COMPRA (ACTUALIZAR STOCK)
        protected void btnConfirmarCompra_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddlProveedores.SelectedValue == "0")
                {
                    MostrarMensaje("Por favor, seleccione un proveedor.", "warning");
                    return;
                }

                ProductoNegocio prodNegocio = new ProductoNegocio();
                int productosActualizados = 0;
                bool huboErrores = false;

                // Recorremos cada fila de la grilla para ver qué se seleccionó
                foreach (GridViewRow row in gvProductosFaltantes.Rows)
                {
                    // Buscamos los controles dentro de la fila
                    CheckBox chk = (CheckBox)row.FindControl("chkSeleccionar");
                    TextBox txtCant = (TextBox)row.FindControl("txtCantidadCompra");

                    // Si el usuario marcó el checkbox
                    if (chk != null && chk.Checked)
                    {
                        // Validamos que la cantidad sea un número positivo
                        if (int.TryParse(txtCant.Text, out int cantidadAComprar) && cantidadAComprar > 0)
                        {
                            // Obtenemos el ID del producto desde DataKeys
                            long idProducto = Convert.ToInt64(gvProductosFaltantes.DataKeys[row.RowIndex].Value);

                            // Buscamos el producto actual
                            // Nota: Lo ideal sería tener un método específico "SumarStock(id, cantidad)" en Negocio
                            // Aquí simulamos trayendo el objeto, modificándolo y guardando.
                            Producto producto = prodNegocio.Listar().Find(p => p.IdProducto == idProducto);

                            if (producto != null)
                            {
                                // Sumamos al stock existente
                                producto.Stock += (short)cantidadAComprar;

                                // Guardamos en base de datos
                                prodNegocio.Modificar(producto);

                                productosActualizados++;
                            }
                        }
                        else
                        {
                            huboErrores = true; // Marcamos error si seleccionó pero puso cantidad 0 o vacía
                        }
                    }
                }

                if (productosActualizados > 0)
                {
                    MostrarMensaje($"Compra exitosa. Se actualizó el stock de {productosActualizados} productos.", "success");

                    // Recargamos la grilla (los productos actualizados desaparecerán si su stock > mínimo)
                    CargarProductosFaltantes();

                    // Cerramos el modal
                    ScriptManager.RegisterStartupScript(this, GetType(), "cerrarModal", "cerrarModalCompra();", true);
                }
                else if (huboErrores)
                {
                    MostrarMensaje("Error: Verifique las cantidades ingresadas en los productos seleccionados.", "danger");
                }
                else
                {
                    MostrarMensaje("No se seleccionó ningún producto para comprar.", "warning");
                }
            }
            catch (Exception ex)
            {
                MostrarMensaje("Error al procesar la compra: " + ex.Message, "danger");
            }
        }

        private void MostrarMensaje(string mensaje, string tipo)
        {
            // Reutilizamos el toast de tu MasterPage o el script incluido en el ASPX
            ScriptManager.RegisterStartupScript(this, GetType(), "toast",
                $"mostrarToast('{mensaje.Replace("'", "")}','{tipo}');", true);
        }
    }
}