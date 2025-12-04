using Dominio;
using Negocio;
using System;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Presentacion
{
    public partial class WebForm3 : System.Web.UI.Page
    {
        private VentaNegocio ventaNeg = new VentaNegocio();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarVentas();
            }
        }

        private void CargarVentas()
        {
            try
            {
                var lista = ventaNeg.Listar();

                // Mapear nombres para mostrar en la tabla
                var listaMostrar = lista.Select(v => new
                {
                    v.IdVenta,
                    ClienteNombre = v.Cliente?.Nombre ?? "Sin nombre",
                    UsuarioEmail = v.Usuario?.email ?? "Sin email",
                    v.NFactura,
                    v.Fecha,
                    v.Total
                }).ToList();

                gvVentas.DataSource = listaMostrar;
                gvVentas.DataBind();
            }
            catch (Exception ex)
            {
                // Mostrar toast de error
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", $"mostrarToast('{ex.Message}', 'danger');", true);
            }
        }

        protected void btnNuevaVenta_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/SeleccionCliente.aspx");
        }

        protected void lnkFactura_Command(object sender, CommandEventArgs e)
        {
            string nFactura = e.CommandArgument.ToString();
            Response.Redirect("ReporteFactura.aspx?nfactura=" + nFactura);
        }
    }
}
