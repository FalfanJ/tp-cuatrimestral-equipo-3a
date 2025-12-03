using Dominio;
using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Presentacion
{
    public partial class NuevaCompra : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarProveedores();
                CargarProductosFaltantes();
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
            ddlProveedores.Items.Insert(0, new ListItem("Seleccione un proveedor", "0"));
        }

        private void CargarProductosFaltantes()
        {
            ProductoNegocio negocio = new ProductoNegocio();
            List<Producto> lista = negocio.Listar();
            List<Producto> faltantes = lista.Where(p => p.Stock <= p.StockMinimo).ToList();
            gvProductosFaltantes.DataSource = faltantes;
            gvProductosFaltantes.DataBind();
        }

        protected void btnConfirmarCompra_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddlProveedores.SelectedValue == "0")
                {
                    MostrarToast("Por favor, seleccione un proveedor.", "warning");
                    return;
                }

                ProductoNegocio prodNegocio = new ProductoNegocio();
                CompraNegocio compraNegocio = new CompraNegocio();

                Compra nuevaCompra = new Compra
                {
                    Proveedor = new Proveedor { IdProveedor = Convert.ToInt64(ddlProveedores.SelectedValue) },
                    Usuario = new Usuario { IdUsuario = 1 },
                    Fecha = DateTime.Now,
                    Total = 0,
                    Detalle = new List<DetalleCompra>()
                };

                bool hayProductosSeleccionados = false;

                foreach (GridViewRow row in gvProductosFaltantes.Rows)
                {
                    CheckBox chk = (CheckBox)row.FindControl("chkSeleccionar");
                    TextBox txtCant = (TextBox)row.FindControl("txtCantidadCompra");

                    if (chk != null && chk.Checked)
                    {
                        if (!int.TryParse(txtCant.Text, out int cantidad) || cantidad <= 0)
                        {
                            MostrarToast("La cantidad debe ser mayor a 0.", "warning");
                            return;
                        }

                        long idProducto = Convert.ToInt64(gvProductosFaltantes.DataKeys[row.RowIndex].Value);
                        Producto producto = prodNegocio.Listar().Find(p => p.IdProducto == idProducto);

                        if (producto != null)
                        {
                            DetalleCompra detalle = new DetalleCompra
                            {
                                Producto = producto,
                                Cantidad = (short)cantidad,
                                PrecioUnitario = producto.Precio
                            };
                            nuevaCompra.Detalle.Add(detalle);

                            // ---- Actualizar stock
                            producto.Stock += (short)cantidad;
                            prodNegocio.Modificar(producto);

                            hayProductosSeleccionados = true;
                        }
                    }
                }

                if (hayProductosSeleccionados)
                {
                    compraNegocio.Agregar(nuevaCompra);

                    // ---- Mostrar toast de éxito y redirigir
                    string script = @"
                mostrarToast('Compra registrada exitosamente.', 'success');
                setTimeout(function(){ window.location.href = 'Compras.aspx'; }, 1500);
            ";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "toastRedirect", script, true);
                }
                else
                {
                    MostrarToast("No se seleccionó ningún producto.", "warning");
                }
            }
            catch (Exception ex)
            {
                MostrarToast("Error al registrar compra: " + ex.Message, "danger");
            }
        }

        private void MostrarToast(string mensaje, string tipo)
        {
            mensaje = HttpUtility.JavaScriptStringEncode(mensaje);

            ScriptManager.RegisterStartupScript(
                this,
                this.GetType(),
                "toast",
                $"mostrarToast('{mensaje}', '{tipo}');",
                true
            );
        }

    }
}
