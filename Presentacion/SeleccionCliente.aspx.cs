using Negocio;
using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Presentacion
{
    public partial class SeleccionCliente : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    ClienteNegocio negCliente = new ClienteNegocio();
                    List<Cliente> listClientes = new List<Cliente>();
                    listClientes = negCliente.Listar();
                    gvCliente.DataSource = listClientes;
                    gvCliente.DataBind();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        protected void btnCancelarVenta_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/ComprasVentas.aspx");
        }

        protected void gvCliente_SelectedIndexChanged(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
        }

        protected void btnConfirmacion_Click(object sender, EventArgs e)
        {

        }
    }
}