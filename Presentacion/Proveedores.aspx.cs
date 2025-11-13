using Dominio;
using Negocio;
using System;
using System.Collections.Generic;
using System.Web.UI;

namespace Presentacion
{
    public partial class Proveedores : System.Web.UI.Page
    {
        protected string tituloModal = "Nuevo Proveedor";
        protected bool idEditing = false;
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
                CargarProveedores();
        }

        private void CargarProveedores()
        {
            try
            {
                ProveedorNegocio negocio = new ProveedorNegocio();
                gvProveedor.DataSource = negocio.Listar();
                gvProveedor.DataBind();
            }
            catch (Exception ex)
            {
                Response.Write($"<script>alert('Error al cargar proveedores: {ex.Message}');</script>");
            }
        }

        protected void btnGuardarProveedor_Click(object sender, EventArgs e)
        {
            try
            {
                ProveedorNegocio negocio = new ProveedorNegocio();

                Proveedor nuevo = new Proveedor
                {
                    Nombre = txtNombre.Text.Trim(),
                    CUIT = txtCUIT.Text.Trim(),
                    Direccion = txtDireccion.Text.Trim(),
                    Telefono = txtTelefono.Text.Trim(),
                    Email = txtEmail.Text.Trim(),
                    Estado = true
                };

                // Si hay un ID oculto, estamos editando
                if (!string.IsNullOrEmpty(hfIdProveedor.Value))
                {
                    nuevo.IdProveedor = Convert.ToInt64(hfIdProveedor.Value);
                    negocio.Editar(nuevo);
                    Response.Write("<script>alert('Proveedor actualizado correctamente');</script>");
                }
                else
                {
                    negocio.Agregar(nuevo);
                    Response.Write("<script>alert('Proveedor agregado correctamente');</script>");
                }

                CargarProveedores();
                LimpiarCampos();
            }
            catch (Exception ex)
            {
                Response.Write($"<script>alert('Error: {ex.Message}');</script>");
            }
        }

        private void LimpiarCampos()
        {
            hfIdProveedor.Value = string.Empty;
            txtNombre.Text = "";
            txtCUIT.Text = "";
            txtDireccion.Text = "";
            txtTelefono.Text = "";
            txtEmail.Text = "";
        }

        protected void gvProveedor_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Editar")
            {
                long id = Convert.ToInt64(e.CommandArgument);
                CargarProveedorEnModal(id);
                tituloModal = "Editar Proveedor";
                idEditing = true;

                ScriptManager.RegisterStartupScript(this, this.GetType(), "abrirModal",
                    "var myModal = new bootstrap.Modal(document.getElementById('modalNuevoProveedor')); myModal.show();", true);
            }
        }

        private void CargarProveedorEnModal(long id)
        {
            ProveedorNegocio negocio = new ProveedorNegocio();
            Proveedor proveedor = negocio.Listar().Find(p => p.IdProveedor == id);

            if (proveedor != null)
            {
                hfIdProveedor.Value = proveedor.IdProveedor.ToString();
                txtNombre.Text = proveedor.Nombre;
                txtCUIT.Text = proveedor.CUIT;
                txtDireccion.Text = proveedor.Direccion;
                txtTelefono.Text = proveedor.Telefono;
                txtEmail.Text = proveedor.Email;
            }
        }

        // Nuevo método: se ejecuta al confirmar eliminación en el modal
        protected void btnEliminarProveedorConfirmado_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(hfIdProveedorEliminar.Value))
                    throw new Exception("No se encontró el ID del proveedor a eliminar.");

                long id = Convert.ToInt64(hfIdProveedorEliminar.Value);
                ProveedorNegocio negocio = new ProveedorNegocio();
                negocio.BajaLogica(id);

                Response.Write("<script>alert('Proveedor eliminado correctamente');</script>");
                CargarProveedores();

                // Limpiar campo oculto
                hfIdProveedorEliminar.Value = "";
            }
            catch (Exception ex)
            {
                Response.Write($"<script>alert('Error al eliminar proveedor: {ex.Message}');</script>");
            }
        }

        public void openCrearModal()
        {
            tituloModal = "Crear Proveedor";
            idEditing = false;
        }
    }
}
