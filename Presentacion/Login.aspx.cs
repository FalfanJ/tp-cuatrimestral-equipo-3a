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

            // ---- Validamos de campos vacios
            if (string.IsNullOrWhiteSpace(email) && string.IsNullOrWhiteSpace(password))
            {
                lblMensaje.Text = "Debe email email y contraseña.";
                return;
            }
            if (string.IsNullOrWhiteSpace(email))
            {
                lblMensaje.Text = "Debe ingresar el email.";
                return;
            }
            if (string.IsNullOrWhiteSpace(password))
            {
                lblMensaje.Text = "Debe ingresard la contraseña.";
                return;
            }

            // ---- Simulamos el log del en la db
            if (email == "perfiladmin@comercio.com" && password == "12345")
            {
                Session["usuario"] = email;
                Response.Redirect("Home.aspx");
            }
            else
            {
                lblMensaje.Text = "Correo o contraseña incorrectos.";
            }
        }


    }
}
