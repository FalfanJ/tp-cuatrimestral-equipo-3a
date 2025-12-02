using Dominio;
using Negocio;
using Presentacion.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Presentacion
{
    public partial class Estadisticas : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
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
            {
                CargarEstadisticasGenerales();
                FiltrarProductosVendidos(); 
            }
        }

        private void CargarEstadisticasGenerales()
        {
            ReporteNegocio negocio = new ReporteNegocio();
            try
            {
                
                gvTopClientes.DataSource = negocio.ObtenerTopClientes();
                gvTopClientes.DataBind();

                
                gvTopVendedores.DataSource = negocio.ObtenerTopVendedores();
                gvTopVendedores.DataBind();

              
                gvStockCritico.DataSource = negocio.ObtenerStockCritico();
                gvStockCritico.DataBind();

              
                gvStockExceso.DataSource = negocio.ObtenerExcesoStock();
                gvStockExceso.DataBind();

                
                gvMargenCategoria.DataSource = negocio.ObtenerMargenPorCategoria();
                gvMargenCategoria.DataBind();
            }
            catch (Exception ex)
            {
                Session.Add("error", ex.ToString());
            }
        }

        protected void ddlFiltroTiempo_SelectedIndexChanged(object sender, EventArgs e)
        {
            FiltrarProductosVendidos();
        }

        private void FiltrarProductosVendidos()
        {
            ReporteNegocio negocio = new ReporteNegocio();
            DateTime desde = DateTime.Now;
            DateTime hasta = DateTime.Now;

            string seleccion = ddlFiltroTiempo.SelectedValue;

            if (seleccion == "Semana")
            {
                desde = DateTime.Now.AddDays(-7);
            }
            else if (seleccion == "Mes")
            {
                desde = DateTime.Now.AddMonths(-1);
            }
            else 
            {
                desde = new DateTime(2000, 1, 1);
            }

            try
            {
                gvProductosVendidos.DataSource = negocio.ObtenerProductosMasVendidos(desde, hasta);
                gvProductosVendidos.DataBind();
            }
            catch (Exception ex)
            {
                
            }
        }
    }
}