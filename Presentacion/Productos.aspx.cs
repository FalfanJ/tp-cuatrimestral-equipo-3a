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
                txtPrecio.Text = seleccionado.Precio.ToString("0.00").Replace(",", "."); // Formato seguro
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

        // BOTÓN GUARDAR (CREAR O EDITAR)
        protected void btnGuardarProducto_Click(object sender, EventArgs e)
        {
            try
            {
                Producto producto = new Producto();
                ProductoNegocio negocio = new ProductoNegocio();

                // 1. Mapear datos del formulario al objeto
                producto.Nombre = txtNombreProducto.Text.Trim();
                producto.NSerie = txtNumeroSerie.Text.Trim();

                
                producto.Marca = new Marca();
                producto.Marca.IdMarca = int.Parse(ddlMarcaNuevo.SelectedValue);

                producto.Categoria = new Categoria();
                producto.Categoria.IdCategoria = int.Parse(ddlCategoriaNuevo.SelectedValue);

                producto.Descripcion = txtDescripcion.Text.Trim();
                producto.Modelo = "ModeloX"; 

                if (decimal.TryParse(txtPrecio.Text, out decimal precio)) producto.Precio = precio;
                else producto.Precio = 0;

                if (short.TryParse(txtStock.Text, out short stock)) producto.Stock = stock;
                else producto.Stock = 0;

                if (short.TryParse(txtStockMinimo.Text, out short stMin)) producto.StockMinimo = stMin;
                else producto.StockMinimo = 0;

               
                if (short.TryParse(txtGanancia.Text, out short ganancia)) producto.PorcentajeGanancia = ganancia;


                // 2.LÓGICA CRÍTICA: DECIDIR SI ES INSERT O UPDATE 
                if (string.IsNullOrEmpty(hfIdProducto.Value))
                {
                    // SI NO HAY ID => ES NUEVO => AGREGAR
                    negocio.Agregar(producto);
                    ScriptManager.RegisterStartupScript(this, GetType(), "toast", "mostrarToast('Producto agregado con éxito.', 'success');", true);
                }
                else
                {
                    // SI HAY ID => ESTAMOS EDITANDO => MODIFICAR
                    producto.IdProducto = long.Parse(hfIdProducto.Value); 
                    negocio.Modificar(producto);
                    ScriptManager.RegisterStartupScript(this, GetType(), "toast", "mostrarToast('Producto modificado con éxito.', 'success');", true);
                }

               
                CargarGrilla();
                LimpiarCamposModalNuevoProducto();

                // Cerrar modal
                //ScriptManager.RegisterStartupScript(this, GetType(), "cerrarModal", "$('#modalNuevoProducto').modal('hide');", true);
                ScriptManager.RegisterStartupScript(this, GetType(), "cerrarModal", "var myModalEl = document.getElementById('modalNuevoProducto'); var modal = bootstrap.Modal.getInstance(myModalEl); if (!modal) { modal = new bootstrap.Modal(myModalEl);} modal.hide();", true);
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "toastError",
                    $"mostrarToast('Error: {ex.Message.Replace("'", "")}', 'danger');", true);
            }
        }

        //  LÓGICA DE ELIMINAR 
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