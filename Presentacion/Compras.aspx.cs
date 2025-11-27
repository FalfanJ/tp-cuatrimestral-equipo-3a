using Dominio;
using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Presentacion
{
    public partial class Compras : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarProveedores();
                CargarProductosFaltantes();
                CargarCompras();
            }
        }

        protected void btnNuevaCompra_Click(object sender, EventArgs e)
        {
            try
            {
                //  Resetear DropDownList
                ddlProveedores.ClearSelection();

                //  Resetear CheckBox y TextBox dentro del GridView
                foreach (GridViewRow row in gvProductosFaltantes.Rows)
                {
                    // Resetear CheckBox
                    CheckBox chk = row.FindControl("chkSeleccionar") as CheckBox;
                    if (chk != null)
                        chk.Checked = false;

                    // Resetear TextBox
                    TextBox txtCantidad = row.FindControl("txtCantidadCompra") as TextBox;
                    if (txtCantidad != null)
                        txtCantidad.Text = string.Empty;
                }

                //  Abrir el modal
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

        private void CargarCompras()
        {
            try
            {
                CompraNegocio compraNegocio = new CompraNegocio();
                List<Compra> lista = compraNegocio.Listar();

                var listaParaGrid = lista.Select(c => new
                {
                    IdCompra = c.IdCompra,
                    ProveedorNombre = c.Proveedor?.Nombre ?? "N/D",
                    Fecha = c.Fecha,
                    TotalProductos = c.Total,
                    Detalle = c.Detalle
                }).ToList();

                foreach (var compra in listaParaGrid)
                {
                    string json = Newtonsoft.Json.JsonConvert.SerializeObject(compra.Detalle);
                    ScriptManager.RegisterStartupScript(this, this.GetType(),
                        "logDetalle" + compra.IdCompra,
                        $"console.log('Detalle compra {compra.IdCompra}: ', {json});",
                        true);
                }

                gvCompras.DataSource = listaParaGrid;
                gvCompras.DataBind();
            }
            catch (Exception ex)
            {
                MostrarMensaje("Error al cargar compras: " + ex.Message, "danger");
            }
        }

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
                CompraNegocio compraNegocio = new CompraNegocio();

                Compra nuevaCompra = new Compra
                {
                    Proveedor = new Proveedor { IdProveedor = Convert.ToInt64(ddlProveedores.SelectedValue) },
                    Usuario = new Usuario { IdUsuario = 1 },
                    Fecha = DateTime.Now,
                    Total = 0,
                    Detalle = new List<DetalleCompra>()
                };

                int productosActualizados = 0;
                bool huboErrores = false;

                foreach (GridViewRow row in gvProductosFaltantes.Rows)
                {
                    CheckBox chk = (CheckBox)row.FindControl("chkSeleccionar");
                    TextBox txtCant = (TextBox)row.FindControl("txtCantidadCompra");

                    if (chk != null && chk.Checked)
                    {
                        if (int.TryParse(txtCant.Text, out int cantidadAComprar) && cantidadAComprar > 0)
                        {
                            long idProducto = Convert.ToInt64(gvProductosFaltantes.DataKeys[row.RowIndex].Value);
                            Producto producto = prodNegocio.Listar().Find(p => p.IdProducto == idProducto);

                            if (producto != null)
                            {
                                DetalleCompra detalle = new DetalleCompra
                                {
                                    Producto = producto,
                                    Cantidad = (short)cantidadAComprar,
                                    PrecioUnitario = producto.Precio
                                };
                                nuevaCompra.Detalle.Add(detalle);

                                producto.Stock += (short)cantidadAComprar;
                                prodNegocio.Modificar(producto);

                                productosActualizados++;
                            }
                        }
                        else
                        {
                            MostrarMensaje("Compra registrada exitosamente.", "success");
                            huboErrores = true;
                        }
                    }
                }

                    MostrarMensaje("Compra registrada exitosamente.", "success");
                    compraNegocio.Agregar(nuevaCompra);
                    CargarCompras();
                    CargarProductosFaltantes();

                
                 if (huboErrores)
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
            ScriptManager.RegisterStartupScript(this, GetType(), "toast",
                $"mostrarToast('{mensaje.Replace("'", "")}','{tipo}');", true);
        }

        public string FormatearDetalle(object detalleObj)
        {
            var detalle = detalleObj as List<Dominio.DetalleCompra>;
            if (detalle == null || detalle.Count == 0)
                return "-";

            return string.Join("<br/>",
                detalle.Select(d => $"{d.Producto.Nombre} (x{d.Cantidad})")
            );
        }
    }
}
