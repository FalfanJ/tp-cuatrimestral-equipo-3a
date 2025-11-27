using System;
using System.Web.UI;

namespace Presentacion
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Validar sesión
                if (Session["usuario"] == null)
                {
                    Response.Redirect("Login.aspx");
                    return;
                }

                dynamic usuario = Session["usuario"];

                // Consola en navegador
                string script = $@"
                    console.log('---- Usuario en sesión ----');
                    console.log('ID Usuario: {usuario.IdUsuario}');
                    console.log('Nombre Usuario: {usuario.NombreUsuario}');
                    console.log('Tipo Usuario: {usuario.TipoUsuario}');
                    console.log('Email: {usuario.Email}');
                ";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "usuarioSesion", script, true);

                // CONTROL DE PERMISOS SEGÚN ROL
                if (usuario.TipoUsuario == "Vendedor")
                {
                    // Ocultar todo excepto Compras/Ventas/Clientes/Facturacion
                    cardClientes.Visible = true;
                    cardProductos.Visible = false;
                    cardProveedores.Visible = false;
                    cardMarcasCategorias.Visible = false;
                    cardReportes.Visible = true;
                    cardUsuarios.Visible = false;
                    cardCompras.Visible = true;
                    cardComprasVentas.Visible = true;
                }
            }
        }
    }
}
