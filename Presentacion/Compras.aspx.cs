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
                CargarCompras();
            }
        }

        // ---- Cargamos los proveedores para el dropdown de filtro
        private void CargarProveedoresFiltro()
        {
            try
            {
                ProveedorNegocio provNeg = new ProveedorNegocio();
                var listaProveedores = provNeg.Listar();
                ddlProveedorFiltro.DataSource = listaProveedores;
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

        // ---- Cargamos todas las compras y las mostramos en el grid
        private void CargarCompras()
        {
            try
            {
                CompraNegocio compraNegocio = new CompraNegocio();
                List<Compra> lista = compraNegocio.Listar();

                // ---- Aplicamos si hay filtros
                lista = AplicarFiltros(lista);

                var listaParaGrid = lista.Select(c => new
                {
                    IdCompra = c.IdCompra,
                    ProveedorNombre = c.Proveedor?.Nombre ?? "N/D",
                    Fecha = c.Fecha,
                    TotalProductos = c.Total,
                    Detalle = c.Detalle
                }).ToList();

                gvCompras.DataSource = listaParaGrid;
                gvCompras.DataBind();
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "Error al cargar compras: " + ex.Message;
                lblMensaje.Visible = true;
            }
        }

        // ---- Metodo para formatear el detalle de productos
        public string FormatearDetalle(object detalleObj)
        {
            var detalle = detalleObj as List<Dominio.DetalleCompra>;
            if (detalle == null || detalle.Count == 0)
                return "-";

            return string.Join("<br/>", detalle.Select(d => $"{d.Producto.Nombre} (x{d.Cantidad})"));
        }

        // ---- Boton para ir al la seccion de registro de compra
        protected void btnNuevaCompra_Click(object sender, EventArgs e)
        {
            Response.Redirect("NuevaCompra.aspx");
        }

        // ---- Evento al cambiar filtros
        protected void Filtro_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarCompras();
        }

        // ---- Boton para limpiar filtros
        protected void btnLimpiarFiltros_Click(object sender, EventArgs e)
        {
            ddlProveedorFiltro.SelectedIndex = 0;
            txtFechaDesde.Text = string.Empty;
            txtFechaHasta.Text = string.Empty;

            CargarCompras();
        }

        // ---- Aplicamos los filtros seleccionados a la lista de compras
        private List<Compra> AplicarFiltros(List<Compra> lista)
        {
            // ---- Filtrar por proveedor
            if (ddlProveedorFiltro.SelectedValue != "0")
            {
                int idProveedor = int.Parse(ddlProveedorFiltro.SelectedValue);
                lista = lista.Where(c => c.Proveedor != null && c.Proveedor.IdProveedor == idProveedor).ToList();
            }

            // ---- Filtrar por fechas
            if (DateTime.TryParse(txtFechaDesde.Text, out DateTime fechaDesde))
            {
                lista = lista.Where(c => c.Fecha >= fechaDesde).ToList();
            }

            if (DateTime.TryParse(txtFechaHasta.Text, out DateTime fechaHasta))
            {
                fechaHasta = fechaHasta.AddDays(1).AddTicks(-1); // <-- Incluye todo el día
                lista = lista.Where(c => c.Fecha <= fechaHasta).ToList();
            }

            return lista;
        }

    }
}
