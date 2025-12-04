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
                CargarProveedoresFiltro();
                CargarUsuariosFiltro();
                CargarCompras();
                CalcularResumenUltimoMes();
            }
        }

        private void CargarProveedoresFiltro()
        {
            try
            {
                ProveedorNegocio provNeg = new ProveedorNegocio();
                var lista = provNeg.Listar();

                ddlProveedorFiltro.DataSource = lista;
                ddlProveedorFiltro.DataTextField = "Nombre";
                ddlProveedorFiltro.DataValueField = "IdProveedor";
                ddlProveedorFiltro.DataBind();

                ddlProveedorFiltro.Items.Insert(0, new ListItem("Todos", "0"));
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "Error al cargar proveedores: " + ex.Message;
                lblMensaje.Visible = true;
            }
        }

        private void CargarCompras()
        {
            try
            {
                CompraNegocio negocio = new CompraNegocio();
                var lista = negocio.Listar();

                lista = AplicarFiltros(lista);

                var listaGrid = lista.Select(c => new
                {
                    c.IdCompra,
                    ProveedorNombre = c.Proveedor?.Nombre ?? "N/D",
                    c.Fecha,
                    TotalProductos = c.Total,
                    c.Detalle,
                    Usuario = c.Usuario
                }).ToList();

                gvCompras.DataSource = listaGrid;
                gvCompras.DataBind();
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "Error al cargar compras: " + ex.Message;
                lblMensaje.Visible = true;
            }
        }

        public string FormatearDetalle(object detalleObj)
        {
            var detalle = detalleObj as List<DetalleCompra>;
            if (detalle == null || detalle.Count == 0)
                return "-";

            return string.Join("<br/>", detalle.Select(d =>
                $"{d.Producto.Nombre} – x{d.Cantidad} – {d.PrecioUnitario:C} c/u"
            ));
        }

        protected void btnNuevaCompra_Click(object sender, EventArgs e)
        {
            Response.Redirect("NuevaCompra.aspx");
        }

        protected void Filtro_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarCompras();
        }

        protected void btnLimpiarFiltros_Click(object sender, EventArgs e)
        {
            ddlProveedorFiltro.SelectedIndex = 0;
            ddlUsuarioFiltro.SelectedIndex = 0;
            txtFechaDesde.Text = "";
            txtFechaHasta.Text = "";

            CargarCompras();
        }

        private List<Compra> AplicarFiltros(List<Compra> lista)
        {
            if (ddlProveedorFiltro.SelectedValue != "0")
            {
                int idProv = Convert.ToInt32(ddlProveedorFiltro.SelectedValue);
                lista = lista.Where(c => c.Proveedor?.IdProveedor == idProv).ToList();
            }

            if (ddlUsuarioFiltro.SelectedValue != "0")
            {
                long idUsr = Convert.ToInt64(ddlUsuarioFiltro.SelectedValue);
                lista = lista.Where(c => c.Usuario?.IdUsuario == idUsr).ToList();
            }

            if (DateTime.TryParse(txtFechaDesde.Text, out DateTime desde))
                lista = lista.Where(c => c.Fecha >= desde).ToList();

            if (DateTime.TryParse(txtFechaHasta.Text, out DateTime hasta))
            {
                hasta = hasta.AddDays(1).AddTicks(-1);
                lista = lista.Where(c => c.Fecha <= hasta).ToList();
            }

            return lista;
        }

        private void CargarUsuariosFiltro()
        {
            try
            {
                UsuarioNegocio negocio = new UsuarioNegocio();
                var lista = negocio.Listar();

                ddlUsuarioFiltro.DataSource = lista;
                ddlUsuarioFiltro.DataTextField = "Email";
                ddlUsuarioFiltro.DataValueField = "IdUsuario";
                ddlUsuarioFiltro.DataBind();

                ddlUsuarioFiltro.Items.Insert(0, new ListItem("Todos", "0"));
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "Error al cargar usuarios: " + ex.Message;
                lblMensaje.Visible = true;
            }
        }

        private void CalcularResumenUltimoMes()
        {
            try
            {
                CompraNegocio compraNegocio = new CompraNegocio();
                List<Compra> todas = compraNegocio.Listar();

                DateTime desde = DateTime.Now.AddDays(-30);
                var comprasUltimoMes = todas.Where(c => c.Fecha >= desde).ToList();

                if (comprasUltimoMes.Count == 0)
                {
                    lblTopUsuario.Text = "No hay compras registradas en el último mes bajo los filtros seleccionados.";
                    lblTotalMes.Text = "";
                    return;
                }

                // ---- Cargamos el label
                decimal totalMes = comprasUltimoMes.Sum(c => c.Total);
                lblTotalMes.Text = $"Total gastado en los últimos 30 días: {totalMes:C}";

                var topUsuario = comprasUltimoMes
                    .Where(c => c.Usuario != null)
                    .GroupBy(c => c.Usuario.IdUsuario)
                    .Select(g => new
                    {
                        IdUsuario = g.Key,
                        Email = g.First().Usuario.email,
                        TotalGastado = g.Sum(x => x.Total)
                    })
                    .OrderByDescending(x => x.TotalGastado)
                    .FirstOrDefault();

                if (topUsuario != null)
                {
                    lblTopUsuario.Text =
                        $"Usuario con más compras del último mes: {topUsuario.Email} (Gastó {topUsuario.TotalGastado:C})";
                }
                else
                {
                    lblTopUsuario.Text = "No se pudo determinar el usuario top.";
                }
            }
            catch (Exception ex)
            {
                lblTopUsuario.Text = "Error al calcular el resumen: " + ex.Message;
            }
        }

        protected void gvCompras_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }

        // ---- Eliminar registro de compra
        protected void btnConfirmarEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                long id = 0;
                if (!string.IsNullOrEmpty(ocultoIdCompra.Value) && long.TryParse(ocultoIdCompra.Value, out id))
                {
                    CompraNegocio negocio = new CompraNegocio();
                    bool ok = negocio.BajaLogica(id);

                    if (ok)
                        ScriptManager.RegisterStartupScript(this, GetType(), "ok",
                            "showToast('Compra eliminada correctamente. Se ha devuelto el stock.', true);", true);
                    else
                        ScriptManager.RegisterStartupScript(this, GetType(), "fail",
                            "showToast('No se pudo eliminar la compra.', false);", true);

                    CargarCompras();
                    CalcularResumenUltimoMes();
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "noid",
                        "showToast('ID de compra inválido.', false);", true);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "err",
                    $"showToast('Error al eliminar: {ex.Message}', false);", true);
            }
        }
    }
}
