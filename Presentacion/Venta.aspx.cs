using Dominio;
using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Presentacion
{
    public partial class Venta : System.Web.UI.Page
    {
        void CargarPanelProductos()
        {
            try
            {
                //gvProdcutos.DataSource = Session["listProducto"] as List<Producto>;
                gvProdcutos.DataSource = (List<Producto>)Session["listProducto"];
                gvProdcutos.DataBind();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        void CargarPanelDetalle()
        {
            try
            {
                //gvDetalle.DataSource = Session["listDetalle"] as List<DetalleVenta>;
                gvDetalle.DataSource = (List<DetalleVenta>)Session["listDetalle"];
                gvDetalle.DataBind();
                upDetalleGrid.Update();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    List<DetalleVenta> listDetalle = new List<DetalleVenta>();
                    Session["listDetalle"] = listDetalle;

                    ProductoNegocio negProducto = new ProductoNegocio();
                    List<Producto> listProducto = new List<Producto>();
                    listProducto = negProducto.Listar();
                    Session["listProducto"] = listProducto;

                    CargarPanelDetalle();
                    CargarPanelProductos();
                }
            }
            catch (Exception)
            {

                throw;
            }

        }

        protected void gvDetalle_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName=="Eliminar")
            {
                Int64 id = Convert.ToInt64(e.CommandArgument);

                List<DetalleVenta> detalles= (List<DetalleVenta>)Session["listDetalle"];

                if (detalles!=null)
                {
                    DetalleVenta aux = detalles.FirstOrDefault(x => x.ID == id);
                    detalles.Remove(aux);
                }

                Session["listDetalle"] = detalles;
                CargarPanelDetalle();
            }
        }
        protected void txtCantidad_TextChanged(object sender, EventArgs e)
        {
            DetalleVenta detalle = (DetalleVenta)Session["detalleActual"];

            if (detalle == null)
                return;

            if (txtCantidad.Text.Length == 0)
            {
                lblParcial.Text = "";
                btnAgregar.Enabled = false;
                return;
            }
            int cantidad = int.Parse(txtCantidad.Text);

            if (cantidad <= 0 || cantidad > detalle.Producto.Stock)
            {
                lblParcial.Text = "";
                btnAgregar.Enabled = false;
                return;
            }
            else
            {
                decimal parcial = (detalle.PrecioUnitario * (((decimal)detalle.PorcentajeGanancia / 100) + 1)) * cantidad;
                lblParcial.Text = parcial.ToString();
                btnAgregar.Enabled = true;
                detalle.Cantidad = (Int16)cantidad;
                detalle.PrecioParcial = parcial;
                Session["detalleActual"] = detalle;
            }
        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            var listDetalle = (List<DetalleVenta>)Session["listDetalle"];
            DetalleVenta detalle = (DetalleVenta)Session["detalleActual"];
            listDetalle.Add(detalle);
            Session["listDetalle"] = listDetalle;
            CargarPanelDetalle();

            lblParcial.Text = "";
            lblPrecio.Text = "";
            lblProducto.Text = "";
            lblTotal.Text = "";
            txtCantidad.Text = "";
            btnAgregar.Enabled = false;
        }

        protected void gvProdcutos_SelectedIndexChanged(object sender, EventArgs e)
        {
            int id = int.Parse(gvProdcutos.SelectedDataKey.Value.ToString());

            //recuperamos de session la lista de productos
            DetalleVenta detalle = new DetalleVenta();
            var listProductos = (List<Producto>)Session["listProducto"];
            detalle.Producto = listProductos.FirstOrDefault(x => x.IdProducto == id);
            detalle.PrecioUnitario = detalle.Producto.Precio;
            detalle.PorcentajeGanancia = detalle.Producto.PorcentajeGanancia;
            detalle.ID = detalle.Producto.IdProducto;

            //Guardamos el producto seleccionado
            Session["detalleActual"] = detalle;

            lblProducto.Text = detalle.Producto.Nombre;
            lblPrecio.Text = detalle.Producto.Precio.ToString();
            lblParcial.Text = "";
            btnAgregar.Enabled = false;

        }

        protected void gvProdcutos_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }
    }
}