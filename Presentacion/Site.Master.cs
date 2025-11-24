using System;
using System.Web.UI;

namespace Presentacion
{
    public partial class SiteMaster : MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Validar sesión
                if (Session["usuario"] == null)
                {
                    Response.Redirect("~/Login.aspx");
                    return;
                }

                dynamic usuario = Session["usuario"];
                string rol = usuario.TipoUsuario.ToString();

                // Mostrar/Ocultar según rol
                if (rol == "Vendedor")
                {
                    // Acceso permitido
                    menuComprasVentas.Visible = true;

                    // Acceso denegado
                    menuClientes.Visible = false;
                    menuProveedores.Visible = false;
                    menuProductos.Visible = false;
                    menuMarcas.Visible = false;
                    menuReportes.Visible = false;
                    menuUsuarios.Visible = false;
                }

                else if (rol == "Administrador")
                {
                    // Todo visible para admins
                    menuComprasVentas.Visible =
                    menuClientes.Visible =
                    menuProveedores.Visible =
                    menuProductos.Visible =
                    menuMarcas.Visible =
                    menuReportes.Visible =
                    menuUsuarios.Visible = true;
                }
            }
        }

        protected void btnCerrarSesion_Click(object sender, EventArgs e)
        {
    
            Session.Clear();
            Session.Abandon();

            Response.Redirect("~/Login.aspx");
        }
    }
}
