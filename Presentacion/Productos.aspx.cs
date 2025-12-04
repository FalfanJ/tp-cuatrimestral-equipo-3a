using Dominio;
using Negocio;
using Presentacion.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Presentacion
{
    public partial class ProductoLista : System.Web.UI.Page
    {
        private List<Producto> listaProductos;

        // Propiedad para cambiar el título del modal dinámicamente
        protected string TituloModal = "➕ Nuevo Producto";

        protected void Page_Load(object sender, EventArgs e)
        {
            // Validar sesión
            if (Session["usuario"] == null)
            {
                Response.Redirect("Login.aspx");
                return;
            }

            dynamic usuario = Session["usuario"];

            // Bloquear si es vendedor
            if (Seguridad.EsVendedor(usuario))
            {
                Response.Redirect("Default.aspx");
                return;
            }

            if (!IsPostBack)
            {
                CargarFiltros();
                CargarGrilla();
                CargarCombosNuevoProducto();
            }
        }

        // --- MÉTODOS DE CARGA (Sin cambios mayores) ---
        private void CargarFiltros()
        {
            try
            {
                MarcaNegocio marcaNeg = new MarcaNegocio();
                ddlMarca.DataSource = marcaNeg.Listar();
                ddlMarca.DataTextField = "Nombre";
                ddlMarca.DataValueField = "IdMarca";
                ddlMarca.DataBind();
                ddlMarca.Items.Insert(0, new ListItem("Todas", "0"));

                CategoriaNegocio catNeg = new CategoriaNegocio();
                ddlCategoria.DataSource = catNeg.Listar();
                ddlCategoria.DataTextField = "Nombre";
                ddlCategoria.DataValueField = "IdCategoria";
                ddlCategoria.DataBind();
                ddlCategoria.Items.Insert(0, new ListItem("Todas", "0"));

                ProveedorNegocio provNeg = new ProveedorNegocio();
                ddlProveedor.DataSource = provNeg.Listar();
                ddlProveedor.DataTextField = "Nombre";
                ddlProveedor.DataValueField = "IdProveedor";
                ddlProveedor.DataBind();
                ddlProveedor.Items.Insert(0, new ListItem("Todos", "0"));
            }
            catch (Exception ex)
            {
                MostrarMensaje("Error al cargar filtros: " + ex.Message, "danger");
            }
        }

        private void CargarCombosNuevoProducto()
        {
            try
            {
                MarcaNegocio marcaNeg = new MarcaNegocio();
                ddlMarcaNuevo.DataSource = marcaNeg.Listar();
                ddlMarcaNuevo.DataTextField = "Nombre";
                ddlMarcaNuevo.DataValueField = "IdMarca";
                ddlMarcaNuevo.DataBind();

                CategoriaNegocio catNeg = new CategoriaNegocio();
                ddlCategoriaNuevo.DataSource = catNeg.Listar();
                ddlCategoriaNuevo.DataTextField = "Nombre";
                ddlCategoriaNuevo.DataValueField = "IdCategoria";
                ddlCategoriaNuevo.DataBind();
            }
            catch (Exception ex)
            {
                MostrarMensaje("Error al cargar combos: " + ex.Message, "danger");
            }
        }

        private void CargarGrilla()
        {
            try
            {
                ProductoNegocio prodNeg = new ProductoNegocio();
                listaProductos = prodNeg.Listar();

                IEnumerable<Producto> filtrados = listaProductos;

                if (!string.IsNullOrEmpty(txtBuscarNombre.Text))
                    filtrados = filtrados.Where(p => p.Nombre.ToUpper().Contains(txtBuscarNombre.Text.ToUpper()));

                if (ddlMarca.SelectedValue != "0")
                    filtrados = filtrados.Where(p => p.Marca != null && p.Marca.IdMarca.ToString() == ddlMarca.SelectedValue);

                if (ddlCategoria.SelectedValue != "0")
                    filtrados = filtrados.Where(p => p.Categoria != null && p.Categoria.IdCategoria.ToString() == ddlCategoria.SelectedValue);

                if (ddlStock.SelectedValue == "1")
                    filtrados = filtrados.Where(p => p.Stock > 0);
                else if (ddlStock.SelectedValue == "2")
                    filtrados = filtrados.Where(p => p.Stock <= 0);

                gvProductos.DataSource = filtrados.ToList();
                gvProductos.DataBind();
                UpdatePanelProductos.Update();
            }
            catch (Exception ex)
            {
                MostrarMensaje("Error al cargar grilla: " + ex.Message, "danger");
            }
        }

        // --- EVENTOS GRILLA Y FILTROS ---
        protected void Filtro_SelectedIndexChanged(object sender, EventArgs e) => CargarGrilla();

        protected void btnLimpiarFiltros_Click(object sender, EventArgs e)
        {
            txtBuscarNombre.Text = string.Empty;
            ddlMarca.SelectedIndex = 0;
            ddlCategoria.SelectedIndex = 0;
            ddlProveedor.SelectedIndex = 0;
            ddlStock.SelectedIndex = 0;
            CargarGrilla();
        }

        protected void gvProductos_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvProductos.PageIndex = e.NewPageIndex;
            CargarGrilla();
        }

        protected void gvProductos_Sorting(object sender, GridViewSortEventArgs e)
        {
            
            ProductoNegocio prodNeg = new ProductoNegocio();
            listaProductos = prodNeg.Listar();
            IEnumerable<Producto> data = listaProductos;

            switch (e.SortExpression)
            {
                case "Nombre": data = data.OrderBy(p => p.Nombre); break;
                case "Precio": data = data.OrderBy(p => p.Precio); break;
                case "Stock": data = data.OrderBy(p => p.Stock); break;
            }

            gvProductos.DataSource = data.ToList();
            gvProductos.DataBind();
        }

        //  LÓGICA DE EDICIÓN (MODIFICADO)
        protected void gvProductos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Editar")
            {
                try
                {
                    
                    long id = Convert.ToInt64(e.CommandArgument);

                    // 2. Cargar los datos en los TextBoxes
                    CargarDatosEnModal(id);

                   
                    TituloModal = "✏️ Editar Producto";

                    
                    UpdatePanelNuevoProducto.Update();

                    // 5. Abrir el modal con JS
                    ScriptManager.RegisterStartupScript(this, GetType(), "abrirModal",
                        "var myModal = new bootstrap.Modal(document.getElementById('modalNuevoProducto')); myModal.show();", true);
                }
                catch (Exception ex)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "error",
                        $"mostrarToast('Error al cargar editar: {ex.Message}', 'danger');", true);
                }
            }
        }

        private void CargarDatosEnModal(long id)
        {
            ProductoNegocio negocio = new ProductoNegocio();
         
            Producto seleccionado = negocio.Listar().Find(x => x.IdProducto == id);

            if (seleccionado != null)
            {
              
                hfIdProducto.Value = seleccionado.IdProducto.ToString();

                txtNombreProducto.Text = seleccionado.Nombre;
                txtNumeroSerie.Text = seleccionado.NSerie;
                txtPrecio.Text = seleccionado.Precio.ToString("0.00").Replace(",", "."); 
                txtStock.Text = seleccionado.Stock.ToString();
                txtStockMinimo.Text = seleccionado.StockMinimo.ToString();
                txtDescripcion.Text = seleccionado.Descripcion;

                
                txtGanancia.Text = seleccionado.PorcentajeGanancia.ToString();

                if (seleccionado.Marca != null)
                    ddlMarcaNuevo.SelectedValue = seleccionado.Marca.IdMarca.ToString();

                if (seleccionado.Categoria != null)
                    ddlCategoriaNuevo.SelectedValue = seleccionado.Categoria.IdCategoria.ToString();
            }
        }

        // BOTON GUARDAR
        protected void btnGuardarProducto_Click(object sender, EventArgs e)
        {
            try
            {
                // --- VALIDACION DE CAMPOS VACIOS ---
                if (string.IsNullOrWhiteSpace(txtNombreProducto.Text) ||
                    string.IsNullOrWhiteSpace(txtNumeroSerie.Text) ||
                    string.IsNullOrWhiteSpace(txtPrecio.Text) ||
                    string.IsNullOrWhiteSpace(txtStock.Text) ||
                    string.IsNullOrWhiteSpace(txtStockMinimo.Text) ||
                    string.IsNullOrWhiteSpace(txtGanancia.Text) ||
                    string.IsNullOrWhiteSpace(txtDescripcion.Text) ||
                    ddlMarcaNuevo.SelectedIndex <= 0 ||
                    ddlCategoriaNuevo.SelectedIndex <= 0)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "toastError",
                        "mostrarToast('⚠️ Todos los campos son obligatorios.', 'danger');", true);
                    return;
                }

                ProductoNegocio negocio = new ProductoNegocio();
                long idActual = string.IsNullOrEmpty(hfIdProducto.Value) ? 0 : long.Parse(hfIdProducto.Value);

                Producto producto = new Producto
                {
                    IdProducto = idActual,
                    Nombre = txtNombreProducto.Text.Trim(),
                    NSerie = txtNumeroSerie.Text.Trim(),
                    Marca = new Marca { IdMarca = int.Parse(ddlMarcaNuevo.SelectedValue) },
                    Categoria = new Categoria { IdCategoria = int.Parse(ddlCategoriaNuevo.SelectedValue) },
                    Descripcion = txtDescripcion.Text.Trim(),
                    Modelo = "ModeloX",
                    Precio = decimal.TryParse(txtPrecio.Text.Trim(), out decimal precio) ? precio : 0,
                    Stock = short.TryParse(txtStock.Text.Trim(), out short stock) ? stock : (short)0,
                    StockMinimo = short.TryParse(txtStockMinimo.Text.Trim(), out short stMin) ? stMin : (short)0,
                    PorcentajeGanancia = short.TryParse(txtGanancia.Text.Trim(), out short ganancia) ? ganancia : (short)0
                };

                if (idActual == 0)
                {
                    negocio.Agregar(producto);
                    ScriptManager.RegisterStartupScript(this, GetType(), "toast",
                        "mostrarToast('Producto agregado con éxito.', 'success');", true);
                }
                else
                {
                    negocio.Modificar(producto);
                    ScriptManager.RegisterStartupScript(this, GetType(), "toast",
                        "mostrarToast('Producto modificado con éxito.', 'success');", true);
                }

                CargarGrilla();
                LimpiarCamposModalNuevoProducto();

                // Cerramos el modal
                ScriptManager.RegisterStartupScript(this, GetType(), "cerrarModal",
                    "var myModalEl = document.getElementById('modalNuevoProducto'); var modal = bootstrap.Modal.getInstance(myModalEl); if (!modal) { modal = new bootstrap.Modal(myModalEl);} modal.hide();", true);
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "toastError",
                    $"mostrarToast('Error: {ex.Message.Replace("'", "")}', 'danger');", true);
            }
        }



        //  Eliminar producto
        protected void btnEliminarProductoConfirmado_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(hfIdProductoEliminar.Value)) return;

               
                long id = long.Parse(hfIdProductoEliminar.Value);

                ProductoNegocio negocio = new ProductoNegocio();

                
                negocio.BajaLogica(id);

                CargarGrilla();

                
                ScriptManager.RegisterStartupScript(this, GetType(), "toastExito",
                    "mostrarToast('Producto eliminado correctamente.', 'success');", true);

                hfIdProductoEliminar.Value = ""; 

                // Cerrar modal
                ScriptManager.RegisterStartupScript(this, GetType(), "cerrarModalEliminar",
                    "$('#modalEliminarProducto').modal('hide');", true);
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "toastError",
                    $"mostrarToast('Error al eliminar: {ex.Message}','danger');", true);
            }
        }

        
        private void LimpiarCamposModalNuevoProducto()
        {
            hfIdProducto.Value = string.Empty; 
            txtNombreProducto.Text = string.Empty;
            txtNumeroSerie.Text = string.Empty;
            ddlMarcaNuevo.SelectedIndex = 0;
            ddlCategoriaNuevo.SelectedIndex = 0;
            txtPrecio.Text = string.Empty;
            txtStock.Text = string.Empty;
            txtStockMinimo.Text = string.Empty;
            txtDescripcion.Text = string.Empty;
            txtGanancia.Text = string.Empty;
            TituloModal = "➕ Nuevo Producto";
        }

        protected void btnAbrirModalNuevo_Click(object sender, EventArgs e)
        {
            LimpiarCamposModalNuevoProducto();
            TituloModal = "➕ Nuevo Producto";
            ScriptManager.RegisterStartupScript(this, GetType(), "abrirModalNuevo",
                   "var myModal = new bootstrap.Modal(document.getElementById('modalNuevoProducto')); myModal.show();", true);
        }

        private void MostrarMensaje(string mensaje, string tipo)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "toast",
                $"mostrarToast('{mensaje.Replace("'", "")}','{tipo}');", true);
        }
    }
}