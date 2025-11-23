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
                MostrarMensaje($"Error al cargar proveedores: {ex.Message}", "danger");
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
                    MostrarMensaje("Proveedor actualizado correctamente", "success");
                }
                else
                {
                    negocio.Agregar(nuevo);
                    MostrarMensaje("Proveedor agregado correctamente", "success");
                }

                CargarProveedores();
                LimpiarCampos();

                // Cerrar el modal vía JS
                ScriptManager.RegisterStartupScript(this, GetType(), "cerrarModal",
                    "var myModalEl = document.getElementById('modalNuevoProveedor'); var modal = bootstrap.Modal.getInstance(myModalEl); if(modal){ modal.hide(); } else { new bootstrap.Modal(myModalEl).hide(); }", true);
            }
            catch (Exception ex)
            {
                MostrarMensaje($"Error: {ex.Message}", "danger");
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

                // Abrir modal con JS
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

        protected void btnEliminarProveedorConfirmado_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(hfIdProveedorEliminar.Value))
                    throw new Exception("No se encontró el ID del proveedor a eliminar.");

                long id = Convert.ToInt64(hfIdProveedorEliminar.Value);
                ProveedorNegocio negocio = new ProveedorNegocio();
                negocio.BajaLogica(id);

                MostrarMensaje("Proveedor eliminado correctamente", "success");
                CargarProveedores();

                // Limpiar campo oculto
                hfIdProveedorEliminar.Value = "";

                // Cerrar modal de eliminar
                ScriptManager.RegisterStartupScript(this, GetType(), "cerrarModalEliminar",
                    "var myModalEl = document.getElementById('modalEliminarProveedor'); var modal = bootstrap.Modal.getInstance(myModalEl); if(modal){ modal.hide(); } else { new bootstrap.Modal(myModalEl).hide(); }", true);
            }
            catch (Exception ex)
            {
                MostrarMensaje($"Error al eliminar proveedor: {ex.Message}", "danger");
            }
        }

        public void openCrearModal()
        {
            tituloModal = "Crear Proveedor";
            idEditing = false;
        }

        // Método auxiliar para llamar al Toast de JS
        private void MostrarMensaje(string mensaje, string tipo)
        {
            string mensajeSeguro = mensaje.Replace("'", "");
            ScriptManager.RegisterStartupScript(this, GetType(), "mostrarToast",
                $"mostrarToast('{mensajeSeguro}', '{tipo}');", true);
        }
    }
}