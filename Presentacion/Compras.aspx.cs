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

                // ---- Aplicamos filtros si existen
                lista = AplicarFiltros(lista);

                // ---- Preparamos lista para el GridView
                var listaParaGrid = lista.Select(c => new
                {
                    IdCompra = c.IdCompra,
                    ProveedorNombre = c.Proveedor?.Nombre ?? "N/D",
                    Fecha = c.Fecha,
                    TotalProductos = c.Total,
                    Detalle = c.Detalle,
                    Usuario = c.Usuario
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

            return string.Join("<br/>", detalle.Select(d =>
                $"{d.Producto.Nombre} – x{d.Cantidad} – {d.PrecioUnitario:C} c/u"
            ));
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
            ddlUsuarioFiltro.SelectedIndex = 0;

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

            // ---- Filtrar por usuaruo responsable
            if (ddlUsuarioFiltro.SelectedValue != "0")
            {
                long idUsuario = long.Parse(ddlUsuarioFiltro.SelectedValue);
                lista = lista.Where(c => c.Usuario != null && c.Usuario.IdUsuario == idUsuario).ToList();
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

        private void CargarUsuariosFiltro()
        {
            try
            {
                UsuarioNegocio usuarioNeg = new UsuarioNegocio();
                var listaUsuarios = usuarioNeg.Listar();
                ddlUsuarioFiltro.DataSource = listaUsuarios;
                ddlUsuarioFiltro.DataTextField = "email";
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



    }
}
