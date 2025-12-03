using Dominio;
using Negocio;
using Presentacion.Models;
using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;

namespace Presentacion
{
    public partial class GestionUsuarios : System.Web.UI.Page
    {
        protected string tituloModal
        {
            get { return ViewState["TituloModal"] as string ?? ""; }
            set { ViewState["TituloModal"] = value; }
        }

        protected bool idEditing
        {
            get { return ViewState["IdEditing"] != null ? (bool)ViewState["IdEditing"] : false; }
            set { ViewState["IdEditing"] = value; }
        }

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
                CargarUsuarios();
        }

        private void CargarUsuarios()
        {
            try
            {
                UsuarioNegocio negocio = new UsuarioNegocio();
                gvUsuarios.DataSource = negocio.Listar();
                gvUsuarios.DataBind();
            }
            catch (Exception ex)
            {
                MostrarMensaje($"Error al cargar usuarios: {ex.Message}", "danger");
            }
        }

        protected void btnNuevoUsuario_Click(object sender, EventArgs e)
        {
            tituloModal = "Crear Usuario";
            idEditing = false;
            LimpiarCampos();
            AbrirModal();
        }

        protected void gvUsuarios_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Editar")
            {
                long id = Convert.ToInt64(e.CommandArgument);
                CargarUsuarioEnModal(id);

                tituloModal = "Editar Usuario";
                idEditing = true;

                AbrirModal();
            }
        }

        private void CargarUsuarioEnModal(long id)
        {
            UsuarioNegocio negocio = new UsuarioNegocio();
            Usuario usuario = negocio.Listar().Find(u => u.IdUsuario == id);

            if (usuario != null)
            {
                hfIdUsuario.Value = usuario.IdUsuario.ToString();
                txtNombreUsuario.Text = usuario.NombreUsuario;
                txtEmail.Text = usuario.email;
                ddlTipoUsuario.SelectedValue = usuario.TipoUsuario;

                txtContrasenia.Attributes["value"] = usuario.Contrasenia;
            }
            else
            {
                MostrarMensaje("Usuario no encontrado.", "warning");
            }
        }

        protected void btnGuardarUsuario_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtNombreUsuario.Text) ||
                    string.IsNullOrWhiteSpace(txtEmail.Text) ||
                    string.IsNullOrWhiteSpace(txtContrasenia.Text) ||
                    string.IsNullOrEmpty(ddlTipoUsuario.SelectedValue))
                {
                    MostrarMensaje("Por favor, complete todos los campos.", "warning");
                    AbrirModal(); 
                    return;
                }

                if (!EsEmailValido(txtEmail.Text.Trim()))
                {
                    MostrarMensaje("Ingrese un correo electrónico válido.", "warning");
                    AbrirModal();
                    return;
                }

                if (!EsContraseñaSegura(txtContrasenia.Text.Trim()))
                {
                    MostrarMensaje("La contraseña debe tener al menos 8 caracteres, incluir mayúsculas, minúsculas, números y un carácter especial.", "warning");
                    AbrirModal();
                    return;
                }

                UsuarioNegocio negocio = new UsuarioNegocio();

                string nombreUsuarioIngresado = txtNombreUsuario.Text.Trim();
                string emailIngresado = txtEmail.Text.Trim();

                long idActual = string.IsNullOrEmpty(hfIdUsuario.Value) ? 0 : long.Parse(hfIdUsuario.Value);

                var listaValidacion = negocio.Listar();

                bool existeUsuario = listaValidacion.Any(u =>
                    u.NombreUsuario.Equals(nombreUsuarioIngresado, StringComparison.OrdinalIgnoreCase) &&
                    u.IdUsuario != idActual); 

                if (existeUsuario)
                {
                    MostrarMensaje("⚠️ El Nombre de Usuario ya está en uso. Elija otro.", "warning");
                    AbrirModal(); 
                    return;
                }

                bool existeEmail = listaValidacion.Any(u =>
                    u.email.Equals(emailIngresado, StringComparison.OrdinalIgnoreCase) &&
                    u.IdUsuario != idActual);

                if (existeEmail)
                {
                    MostrarMensaje("⚠️ Ese correo electrónico ya está registrado en el sistema.", "warning");
                    AbrirModal();
                    return;
                }

                Usuario nuevo = new Usuario
                {
                    NombreUsuario = nombreUsuarioIngresado,
                    email = emailIngresado,
                    TipoUsuario = ddlTipoUsuario.SelectedValue,
                    Contrasenia = txtContrasenia.Text.Trim()
                };

                if (idActual != 0)
                {
                    // EDITAR
                    nuevo.IdUsuario = idActual;
                    negocio.Modificar(nuevo);
                    MostrarMensaje("Usuario actualizado correctamente.", "success");
                }
                else
                {
                    // NUEVO
                    negocio.Agregar(nuevo);
                    MostrarMensaje("Usuario agregado correctamente.", "success");
                }

                CargarUsuarios();
                LimpiarCampos();

                ScriptManager.RegisterStartupScript(this, this.GetType(), "cerrarModal",
                    "var myModalEl = document.getElementById('modalNuevoUsuario'); var modal = bootstrap.Modal.getInstance(myModalEl); if (!modal) { modal = new bootstrap.Modal(myModalEl);} modal.hide();", true);

            }
            catch (Exception ex)
            {
                MostrarMensaje($"Error al guardar usuario: {ex.Message}", "danger");
            }
        }

        protected void btnEliminarUsuarioConfirmado_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(hfIdUsuarioEliminar.Value))
                {
                    MostrarMensaje("No se encontró el usuario a eliminar.", "warning");
                    return;
                }

                long id = Convert.ToInt64(hfIdUsuarioEliminar.Value);

                UsuarioNegocio negocio = new UsuarioNegocio();
                negocio.BajaLogica(id);

                MostrarMensaje("Usuario eliminado correctamente.", "success");
                CargarUsuarios();
            }
            catch (Exception ex)
            {
                MostrarMensaje($"Error al eliminar usuario: {ex.Message}", "danger");
            }
        }

        protected void btnFiltrar_Click(object sender, EventArgs e)
        {
            try
            {
                UsuarioNegocio negocio = new UsuarioNegocio();
                var lista = negocio.Listar();

                if (!string.IsNullOrWhiteSpace(txtFiltroNombre.Text))
                    lista = lista.FindAll(u => u.NombreUsuario.IndexOf(txtFiltroNombre.Text.Trim(), StringComparison.OrdinalIgnoreCase) >= 0);

                if (!string.IsNullOrWhiteSpace(txtFiltroEmail.Text))
                    lista = lista.FindAll(u => u.email.IndexOf(txtFiltroEmail.Text.Trim(), StringComparison.OrdinalIgnoreCase) >= 0);

                gvUsuarios.DataSource = lista;
                gvUsuarios.DataBind();
            }
            catch (Exception ex)
            {
                MostrarMensaje($"Error al filtrar usuarios: {ex.Message}", "danger");
            }
        }

        protected void btnResetFiltros_Click(object sender, EventArgs e)
        {
            txtFiltroNombre.Text = "";
            txtFiltroEmail.Text = "";
            CargarUsuarios();
        }

        private void LimpiarCampos()
        {
            hfIdUsuario.Value = string.Empty;
            txtNombreUsuario.Text = "";
            txtEmail.Text = "";
            ddlTipoUsuario.SelectedIndex = 0;
            txtContrasenia.Text = "";
        }

        private bool EsEmailValido(string email)
        {
            string patron = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            return Regex.IsMatch(email, patron);
        }

        private bool EsContraseñaSegura(string contrasenia)
        {
            string patron = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[\W_]).{8,}$";
            return Regex.IsMatch(contrasenia, patron);
        }

        private void MostrarMensaje(string mensaje, string tipo)
        {
            mensaje = HttpUtility.JavaScriptStringEncode(mensaje);

            ScriptManager.RegisterStartupScript(
                this,
                this.GetType(),
                "toast",
                $"mostrarToast('{mensaje}', '{tipo}');",
                true
            );
        }

        private void AbrirModal()
        {
            ScriptManager.RegisterStartupScript(
                this,
                this.GetType(),
                "abrirModal",
                "var myModal = new bootstrap.Modal(document.getElementById('modalNuevoUsuario')); myModal.show();",
                true
            );
        }
    }
}
    