using Dominio;
using Negocio;
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
        private List<Proveedor> listaProveedores;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarFiltros();
                CargarGrilla();
                CargarCombosNuevoProducto(); // 🔹 Cargar dropdowns del modal
            }
        }

        private void CargarFiltros()
        {
            try
            {
                // Marcas
                MarcaNegocio marcaNeg = new MarcaNegocio();
                ddlMarca.DataSource = marcaNeg.Listar();
                ddlMarca.DataTextField = "Nombre";
                ddlMarca.DataValueField = "IdMarca";
                ddlMarca.DataBind();
                ddlMarca.Items.Insert(0, new ListItem("Todas", "0"));

                // Categorías
                CategoriaNegocio catNeg = new CategoriaNegocio();
                ddlCategoria.DataSource = catNeg.Listar();
                ddlCategoria.DataTextField = "Nombre";
                ddlCategoria.DataValueField = "IdCategoria";
                ddlCategoria.DataBind();
                ddlCategoria.Items.Insert(0, new ListItem("Todas", "0"));

                // Proveedores
                ProveedorNegocio provNeg = new ProveedorNegocio();
                listaProveedores = provNeg.Listar();
                ddlProveedor.DataSource = listaProveedores;
                ddlProveedor.DataTextField = "RazonSocial";
                ddlProveedor.DataValueField = "IdProveedor";
                ddlProveedor.DataBind();
                ddlProveedor.Items.Insert(0, new ListItem("Todos", "0"));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void CargarCombosNuevoProducto()
        {
            try
            {
                // 🔹 Marcas
                MarcaNegocio marcaNeg = new MarcaNegocio();
                ddlMarcaNuevo.DataSource = marcaNeg.Listar();
                ddlMarcaNuevo.DataTextField = "Nombre";
                ddlMarcaNuevo.DataValueField = "IdMarca";
                ddlMarcaNuevo.DataBind();

                // 🔹 Categorías
                CategoriaNegocio catNeg = new CategoriaNegocio();
                ddlCategoriaNuevo.DataSource = catNeg.Listar();
                ddlCategoriaNuevo.DataTextField = "Nombre";
                ddlCategoriaNuevo.DataValueField = "IdCategoria";
                ddlCategoriaNuevo.DataBind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void CargarGrilla()
        {
            try
            {
                ProductoNegocio prodNeg = new ProductoNegocio();
                listaProductos = prodNeg.Listar();

                // Aplicar filtros
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
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void Filtro_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarGrilla();
        }

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
                case "Nombre":
                    data = data.OrderBy(p => p.Nombre);
                    break;
                case "Marca.Nombre":
                    data = data.OrderBy(p => p.Marca?.Nombre);
                    break;
                case "Categoria.Nombre":
                    data = data.OrderBy(p => p.Categoria?.Nombre);
                    break;
                case "Precio":
                    data = data.OrderBy(p => p.Precio);
                    break;
                case "Stock":
                    data = data.OrderBy(p => p.Stock);
                    break;
            }

            gvProductos.DataSource = data.ToList();
            gvProductos.DataBind();
        }

        protected void gvProductos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Editar")
            {
                string id = e.CommandArgument.ToString();
                Response.Redirect("ProductoForm.aspx?id=" + id, false);
            }
        }

        
        protected void btnGuardarProducto_Click(object sender, EventArgs e)
        {
            try
            {
                Producto nuevo = new Producto();
                ProductoNegocio negocio = new ProductoNegocio();

                nuevo.Nombre = txtNombreProducto.Text.Trim();
                nuevo.NSerie = txtNumeroSerie.Text.Trim();
                nuevo.Marca = new Marca { IdMarca = int.Parse(ddlMarcaNuevo.SelectedValue) };
                nuevo.Categoria = new Categoria { IdCategoria = int.Parse(ddlCategoriaNuevo.SelectedValue) };
                nuevo.Precio = decimal.Parse(txtPrecio.Text);
                nuevo.Stock = (short)int.Parse(txtStock.Text);
                nuevo.StockMinimo = (short)int.Parse(txtStockMinimo.Text);
                nuevo.Descripcion = txtDescripcion.Text.Trim();
                nuevo.Imagenes = new List<Imagen>(); // por ahora vacío

                Producto producto = new Producto();

                // --- Validaciones 
                if (decimal.TryParse(txtPrecio.Text, out decimal precio))
                    producto.Precio = precio;
                else
                    producto.Precio = 0; 

                if (short.TryParse(txtStock.Text, out short stock))
                    producto.Stock = stock;
                else
                    producto.Stock = 0;

                if (short.TryParse(txtStockMinimo.Text, out short stockMinimo))
                    producto.StockMinimo = stockMinimo;
                else
                    producto.StockMinimo = 0;

            

                negocio.Agregar(nuevo);

                // Recargar grilla
                CargarGrilla();

                // Cerrar modal por JavaScript
                ScriptManager.RegisterStartupScript(this, GetType(), "cerrarModal", "$('#modalNuevoProducto').modal('hide');", true);
            }
            catch (Exception ex)
            {
                // Podrías mostrar el error en un label dentro del modal
                throw ex;
            }
        }
    }
}

