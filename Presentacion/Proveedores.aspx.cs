using Dominio;
using Negocio;
using Presentacion.Models;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Web.UI;

namespace Presentacion
{
    public partial class Proveedores : System.Web.UI.Page
    {
        protected string tituloModal = "Nuevo Proveedor";
        protected bool idEditing = false;

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
                bool tieneErrores = false;

                // Validaciones
                if (string.IsNullOrWhiteSpace(txtNombre.Text))
                {
                    MostrarMensaje("El nombre es obligatorio.", "danger");
                    tieneErrores = true;
                }

                if (string.IsNullOrWhiteSpace(txtCUIT.Text))
                {
                    MostrarMensaje("El CUIT es obligatorio.", "danger");
                    tieneErrores = true;
                }
                else
                {
                    Regex regexCUIT = new Regex(@"^\d{2}-\d{8}-\d{1}$");
                    if (!regexCUIT.IsMatch(txtCUIT.Text.Trim()))
                    {
                        MostrarMensaje("El CUIT debe tener el formato XX-XXXXXXXX-X.", "danger");
                        tieneErrores = true;
                    }
                }

                if (string.IsNullOrWhiteSpace(txtDireccion.Text))
                {
                    MostrarMensaje("La dirección es obligatoria.", "danger");
                    tieneErrores = true;
                }

                if (string.IsNullOrWhiteSpace(txtTelefono.Text))
                {
                    MostrarMensaje("El teléfono es obligatorio.", "danger");
                    tieneErrores = true;
                }

                if (string.IsNullOrWhiteSpace(txtEmail.Text))
                {
                    MostrarMensaje("El email es obligatorio.", "danger");
                    tieneErrores = true;
                }
                else
                {
                    try
                    {
                        var mail = new System.Net.Mail.MailAddress(txtEmail.Text.Trim());
                    }
                    catch
                    {
                        MostrarMensaje("El email no tiene un formato válido.", "danger");
                        tieneErrores = true;
                    }
                }

                if (tieneErrores)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "abrirModalError",
                        "var modalEl = document.getElementById('modalNuevoProveedor');" +
                        "var modal = bootstrap.Modal.getOrCreateInstance(modalEl);" +
                        "modal.show();",
                        true);
                    return;
                }

                // VALIDAR NOMBRE DUPLICADO
                ProveedorNegocio negocio = new ProveedorNegocio();

                long? idActual = string.IsNullOrEmpty(hfIdProveedor.Value)
                    ? (long?)null
                    : Convert.ToInt64(hfIdProveedor.Value);

                if (negocio.ExisteNombre(txtNombre.Text.Trim(), idActual))
                {
                    MostrarMensaje("Ya existe un proveedor con ese nombre.", "danger");

                    ScriptManager.RegisterStartupScript(this, GetType(), "abrirModalDuplicado",
                        "var modalEl = document.getElementById('modalNuevoProveedor');" +
                        "var modal = bootstrap.Modal.getOrCreateInstance(modalEl);" +
                        "modal.show();",
                        true);
                    return;
                }

                // GUARDAR O EDITAR
                Proveedor nuevo = new Proveedor
                {
                    Nombre = txtNombre.Text.Trim(),
                    CUIT = txtCUIT.Text.Trim(),
                    Direccion = txtDireccion.Text.Trim(),
                    Telefono = txtTelefono.Text.Trim(),
                    Email = txtEmail.Text.Trim(),
                    Estado = true
                };

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


                // Cerrar modal
                ScriptManager.RegisterStartupScript(this, GetType(), "cerrarModal",
                    "var modalEl = document.getElementById('modalNuevoProveedor');" +
                    "var modal = bootstrap.Modal.getOrCreateInstance(modalEl);" +
                    "modal.hide();",
                    true);

            }
            catch (Exception ex)
            {
                MostrarMensaje("Error: " + ex.Message, "danger");

                // Si hubo una excepción, reabrir el modal
                ScriptManager.RegisterStartupScript(this, GetType(), "abrirModalCatch",
                    "var modalEl = document.getElementById('modalNuevoProveedor');" +
                    "var modal = bootstrap.Modal.getOrCreateInstance(modalEl);" +
                    "modal.show();",
                    true);
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