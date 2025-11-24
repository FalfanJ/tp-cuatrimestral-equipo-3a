using Dominio;
using Negocio;
using Presentacion.Models;
using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Presentacion
{
    public partial class MarcasCategorias : Page
    {
        MarcaNegocio marcaNegocio = new MarcaNegocio();
        CategoriaNegocio categoriaNegocio = new CategoriaNegocio();

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
                CargarListas();
        }

        private void CargarListas()
        {
            var listaMarcas = marcaNegocio.Listar();
            var listaCategorias = categoriaNegocio.Listar();

            gvMarcas.DataSource = listaMarcas;
            gvMarcas.DataBind();

            gvCategorias.DataSource = listaCategorias;
            gvCategorias.DataBind();

            // ---- Mostrar u ocultar los titulos segun si hay registros
            lblTituloMarcas.Visible = listaMarcas.Count > 0;
            lblSinMarcas.Visible = listaMarcas.Count == 0;

            lblTituloCategorias.Visible = listaCategorias.Count > 0;
            lblSinCategorias.Visible = listaCategorias.Count == 0;
        }

        // === MARCAS ===
        protected void btnAgregarMarca_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtNuevaMarca.Text))
            {
                Marca nueva = new Marca { Nombre = txtNuevaMarca.Text.Trim() };
                marcaNegocio.Agregar(nueva);
                lblMensajeMarca.Text = "Marca agregada con éxito.";
                txtNuevaMarca.Text = "";
                CargarListas();
            }
        }

        protected void gvMarcas_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvMarcas.EditIndex = e.NewEditIndex;
            CargarListas();
        }

        protected void gvMarcas_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvMarcas.EditIndex = -1;
            CargarListas();
        }

        protected void gvMarcas_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            int id = Convert.ToInt32(gvMarcas.DataKeys[e.RowIndex].Value);
            GridViewRow fila = gvMarcas.Rows[e.RowIndex];
            TextBox txtNombreEdit = (TextBox)fila.FindControl("txtNombreEdit");

            Marca m = new Marca { IdMarca = id, Nombre = txtNombreEdit.Text.Trim() };
            marcaNegocio.Modificar(m);

            gvMarcas.EditIndex = -1;
            lblMensajeMarca.Text = "Marca modificada correctamente.";
            CargarListas();
        }

        protected void gvMarcas_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "ConfirmarEliminacionMarca")
            {
                string script = $"abrirModalEliminar('{e.CommandArgument}', 'marca');";
                ScriptManager.RegisterStartupScript(this, GetType(), "mostrarModalMarca", script, true);
            }
        }

        // === CATEGORÍAS ===
        protected void btnAgregarCategoria_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtNuevaCategoria.Text))
            {
                Categoria nueva = new Categoria { Nombre = txtNuevaCategoria.Text.Trim() };
                categoriaNegocio.Agregar(nueva);
                lblMensajeCategoria.Text = "Categoría agregada con éxito.";
                txtNuevaCategoria.Text = "";
                CargarListas();
            }
        }

        protected void gvCategorias_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvCategorias.EditIndex = e.NewEditIndex;
            CargarListas();
        }

        protected void gvCategorias_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvCategorias.EditIndex = -1;
            CargarListas();
        }

        protected void gvCategorias_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            int id = Convert.ToInt32(gvCategorias.DataKeys[e.RowIndex].Value);
            GridViewRow fila = gvCategorias.Rows[e.RowIndex];
            TextBox txtNombreEditCat = (TextBox)fila.FindControl("txtNombreEditCat");

            Categoria c = new Categoria { IdCategoria = id, Nombre = txtNombreEditCat.Text.Trim() };
            categoriaNegocio.Modificar(c);

            gvCategorias.EditIndex = -1;
            lblMensajeCategoria.Text = "Categoría modificada correctamente.";
            CargarListas();
        }

        protected void gvCategorias_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "ConfirmarEliminacionCategoria")
            {
                string script = $"abrirModalEliminar('{e.CommandArgument}', 'categoria');";
                ScriptManager.RegisterStartupScript(this, GetType(), "mostrarModalCategoria", script, true);
            }
        }

        protected void btnConfirmarEliminacion_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(hiddenIdEliminar.Value);
            string tipo = hiddenTipoEliminar.Value;

            if (tipo == "marca")
            {
                marcaNegocio.BajaLogica(id);
                lblMensajeMarca.Text = "Marca eliminada correctamente.";
            }
            else if (tipo == "categoria")
            {
                categoriaNegocio.BajaLogica(id);
                lblMensajeCategoria.Text = "Categoría eliminada correctamente.";
            }

            CargarListas();
        }
    }
}
