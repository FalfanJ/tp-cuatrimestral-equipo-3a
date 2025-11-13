using Dominio;
using Negocio;
using System;
using System.Text.RegularExpressions;
using System.Web.UI;

namespace Presentacion
{
    public partial class GestionUsuarios : System.Web.UI.Page
    {
        protected string tituloModal;
        protected bool idEditing;

        protected void Page_Load(object sender, EventArgs e)
        {
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
                MostrarAlerta($"Error al cargar usuarios: {ex.Message}");
            }
        }

        protected void btnNuevoUsuario_Click(object sender, EventArgs e)
        {
            tituloModal = "Crear Usuario";
            idEditing = false;
            LimpiarCampos();

            ScriptManager.RegisterStartupScript(this, this.GetType(), "abrirModal",
                "var myModal = new bootstrap.Modal(document.getElementById('modalNuevoUsuario')); myModal.show();", true);
        }

        protected void gvUsuarios_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Editar")
            {
                long id = Convert.ToInt64(e.CommandArgument);
                CargarUsuarioEnModal(id);
                tituloModal = "Editar Usuario";
                idEditing = true;

                ScriptManager.RegisterStartupScript(this, this.GetType(), "abrirModal",
                    "var myModal = new bootstrap.Modal(document.getElementById('modalNuevoUsuario')); myModal.show();", true);
            }
            else if (e.CommandName == "Eliminar")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                long id = Convert.ToInt64(gvUsuarios.DataKeys[index].Value);
                hfIdUsuarioEliminar.Value = id.ToString();

                ScriptManager.RegisterStartupScript(this, this.GetType(), "abrirEliminar",
                    "var myModal = new bootstrap.Modal(document.getElementById('modalConfirmarEliminar')); myModal.show();", true);
            }
        }

        private void CargarUsuarioEnModal(long id)
        {
            UsuarioNegocio negocio = new UsuarioNegocio();
            Usuario usuario = negocio.Listar().Find(u => u.IdUsuario == id);

            if (usuario != null)
            {
                System.Diagnostics.Debug.WriteLine($"Usuario cargado: {usuario.IdUsuario} - {usuario.NombreUsuario} - {usuario.email} - {usuario.Contrasenia}");

                hfIdUsuario.Value = usuario.IdUsuario.ToString();
                txtNombreUsuario.Text = usuario.NombreUsuario;
                txtEmail.Text = usuario.email;
                ddlTipoUsuario.SelectedValue = usuario.TipoUsuario;

                // ---- Forzamos q se muestre la contrasenia
                txtContrasenia.Attributes["value"] = usuario.Contrasenia;
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("⚠️ Usuario no encontrado");
            }
        }



        protected void btnGuardarUsuario_Click(object sender, EventArgs e)
        {
            try
            {
                // Validaciones
                if (string.IsNullOrWhiteSpace(txtNombreUsuario.Text) ||
                    string.IsNullOrWhiteSpace(txtEmail.Text) ||
                    string.IsNullOrWhiteSpace(txtContrasenia.Text) ||
                    string.IsNullOrEmpty(ddlTipoUsuario.SelectedValue))
                {
                    MostrarAlerta("Por favor, complete todos los campos.");
                    return;
                }

                if (!EsEmailValido(txtEmail.Text.Trim()))
                {
                    MostrarAlerta("Ingrese un correo electrónico válido.");
                    return;
                }

                if (!EsContraseñaSegura(txtContrasenia.Text.Trim()))
                {
                    MostrarAlerta("La contraseña debe tener al menos 8 caracteres, incluir mayúsculas, minúsculas, números y un carácter especial.");
                    return;
                }

                UsuarioNegocio negocio = new UsuarioNegocio();
                Usuario nuevo = new Usuario
                {
                    NombreUsuario = txtNombreUsuario.Text.Trim(),
                    email = txtEmail.Text.Trim(),
                    TipoUsuario = ddlTipoUsuario.SelectedValue,
                    Contrasenia = txtContrasenia.Text.Trim()
                };

                if (!string.IsNullOrEmpty(hfIdUsuario.Value))
                {
                    nuevo.IdUsuario = Convert.ToInt64(hfIdUsuario.Value);
                    negocio.Modificar(nuevo);
                    MostrarAlerta("Usuario actualizado correctamente.");
                }
                else
                {
                    negocio.Agregar(nuevo);
                    MostrarAlerta("Usuario agregado correctamente.");
                }

                CargarUsuarios();
                LimpiarCampos();
            }
            catch (Exception ex)
            {
                MostrarAlerta($"Error al guardar usuario: {ex.Message}");
            }
        }

        protected void btnEliminarUsuarioConfirmado_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(hfIdUsuarioEliminar.Value))
                    throw new Exception("No se encontró el ID del usuario a eliminar.");

                long id = Convert.ToInt64(hfIdUsuarioEliminar.Value);
                UsuarioNegocio negocio = new UsuarioNegocio();
                negocio.BajaLogica(id);

                MostrarAlerta("Usuario eliminado correctamente.");
                CargarUsuarios();
            }
            catch (Exception ex)
            {
                MostrarAlerta($"Error al eliminar usuario: {ex.Message}");
            }
        }

        private void LimpiarCampos()
        {
            hfIdUsuario.Value = string.Empty;
            txtNombreUsuario.Text = "";
            txtEmail.Text = "";
            ddlTipoUsuario.SelectedIndex = 0;
            txtContrasenia.Text = "";
        }

        // --- Validadores ---
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

        private void MostrarAlerta(string mensaje)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alerta", $"alert('{mensaje}');", true);
        }

        protected void btnFiltrar_Click(object sender, EventArgs e)
        {
            try
            {
                UsuarioNegocio negocio = new UsuarioNegocio();
                var lista = negocio.Listar();

                // Filtrar por nombre
                if (!string.IsNullOrWhiteSpace(txtFiltroNombre.Text))
                    lista = lista.FindAll(u => u.NombreUsuario.IndexOf(txtFiltroNombre.Text.Trim(), StringComparison.OrdinalIgnoreCase) >= 0);

                // Filtrar por email
                if (!string.IsNullOrWhiteSpace(txtFiltroEmail.Text))
                    lista = lista.FindAll(u => u.email.IndexOf(txtFiltroEmail.Text.Trim(), StringComparison.OrdinalIgnoreCase) >= 0);

                gvUsuarios.DataSource = lista;
                gvUsuarios.DataBind();
            }
            catch (Exception ex)
            {
                MostrarAlerta($"Error al filtrar usuarios: {ex.Message}");
            }
        }

        protected void btnResetFiltros_Click(object sender, EventArgs e)
        {
            // Limpiar cajas de filtro
            txtFiltroNombre.Text = "";
            txtFiltroEmail.Text = "";

            // Recargar todos los usuarios
            CargarUsuarios();
        }
    }
}
