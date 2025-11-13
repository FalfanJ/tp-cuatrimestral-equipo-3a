using Dominio;
using Negocio;
using System;
using System.Web.UI;

namespace Presentacion
{
    public partial class Login : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // ---- Si la sesion ya esta iniciada, redirigimos al home (consulta a la db)
    
            if (Session["usuario"] != null)
            {
                Response.Redirect("Home.aspx");
            }
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            string email = txtEmail.Text.Trim();
            string password = txtPassword.Text.Trim();

            // ---- Validaciones básicas
            if (string.IsNullOrWhiteSpace(email))
            {
                lblMensaje.Text = "Debe ingresar el email.";
                return;
            }
            if (string.IsNullOrWhiteSpace(password))
            {
                lblMensaje.Text = "Debe ingresar la contraseña.";
                return;
            }

            try
            {
                UsuarioNegocio negocio = new UsuarioNegocio();
                Usuario usuario = negocio.Login(email, password);

                if (usuario != null)
                {
                    // Guardamos el usuario completo en sesión
                    Session["usuario"] = usuario;
                    Response.Redirect("~/Default.aspx");
                }
                else
                {
                    lblMensaje.Text = "Correo o contraseña incorrectos.";
                }
            }
            catch (Exception ex)
            {
                lblMensaje.Text = $"Error al iniciar sesión: {ex.Message}";
            }
        }



    }
}
