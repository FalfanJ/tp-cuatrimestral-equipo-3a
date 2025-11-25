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
        void limpiarModalYSalir()
        {
            lblParcial.Text = "";
            lblPrecio.Text = "";
            lblProducto.Text = "";
            txtCantidad.Text = "";
            btnAgregar.Enabled = false;
            txtCantidad.Enabled = false;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "HidePopup", "closeModal();", true);
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
            if (e.CommandName == "Eliminar")
            {
                Int64 id = Convert.ToInt64(e.CommandArgument);

                List<DetalleVenta> detalles = (List<DetalleVenta>)Session["listDetalle"];

                if (detalles != null)
                {
                    DetalleVenta aux = detalles.FirstOrDefault(x => x.ID == id);
                    detalles.Remove(aux);
                }

                Session["listDetalle"] = detalles;
                UpdateTotal();
                CargarPanelDetalle();
            }
        }
        protected void txtCantidad_TextChanged(object sender, EventArgs e)
        {
            DetalleVenta detalle = (DetalleVenta)Session["detalleActual"];
            lblErrorCantidad.Text = "";
            lblParcial.Text = "";
            btnAgregar.Enabled = false;

            //Verificacion
            if (detalle == null)
                return;

            if (txtCantidad.Text.Length == 0)
            {
                lblErrorCantidad.Text = "Ingreso un numero";
                return;
            }
            foreach (char item in txtCantidad.Text)
            {
                if (!(char.IsNumber(item)))
                {
                    lblErrorCantidad.Text = "Solo números.";
                    return;
                }
            }
            int cantidad = int.Parse(txtCantidad.Text);

            if (cantidad <= 0)
            {
                lblErrorCantidad.Text = "No se permite 0";
                return;
            }
            else if (cantidad > detalle.Producto.Stock)
            {
                lblErrorCantidad.Text = "Cantidad supera Stock";
                return;
            }

            //
            decimal parcial = (detalle.PrecioUnitario * (((decimal)detalle.PorcentajeGanancia / 100) + 1)) * cantidad;
            lblParcial.Text = parcial.ToString();
            btnAgregar.Enabled = true;
            detalle.Cantidad = (Int16)cantidad;
            detalle.PrecioParcial = parcial;
            Session["detalleActual"] = detalle;
        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            List<DetalleVenta> listDetalle = (List<DetalleVenta>)Session["listDetalle"];
            DetalleVenta detalle = (DetalleVenta)Session["detalleActual"];
            listDetalle.Add(detalle);
            Session["listDetalle"] = listDetalle;
            detalle = new DetalleVenta();
            Session["detalleActual"] = detalle;
            UpdateTotal();
            CargarPanelDetalle();
            limpiarModalYSalir();
        }

        protected void gvProdcutos_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtCantidad.Enabled = true;
            txtCantidad.Text = "";
            btnAgregar.Enabled = false;
            lblParcial.Text = "";
            lblErrorCantidad.Text = "";
            List<DetalleVenta> listDetalle = (List<DetalleVenta>)Session["listDetalle"];
            int id = int.Parse(gvProdcutos.SelectedDataKey.Value.ToString());


            if (listDetalle.Find(x => x.ID == id) != null)
            {
                txtCantidad.Enabled = false;
                lblProducto.Text = "Producto ya cargado";
                lblPrecio.Text = "";
                return;
            }

            //recuperamos de session la lista de productos
            DetalleVenta detalle = new DetalleVenta();
            List<Producto> listProductos = (List<Producto>)Session["listProducto"];

            detalle.Producto = listProductos.FirstOrDefault(x => x.IdProducto == id);
            detalle.PrecioUnitario = detalle.Producto.Precio;
            detalle.PorcentajeGanancia = detalle.Producto.PorcentajeGanancia;
            detalle.ID = detalle.Producto.IdProducto;

            //Guardamos el producto seleccionado
            Session["detalleActual"] = detalle;

            lblProducto.Text = detalle.Producto.Nombre;
            lblPrecio.Text = detalle.Producto.Precio.ToString();
        }

        protected void btnCerrarModal_Click(object sender, EventArgs e)
        {
            limpiarModalYSalir();
        }

        protected void btnFinalizar_Click(object sender, EventArgs e)
        {
            Dominio.Venta ven = new Dominio.Venta();
            VentaNegocio negVenta = new VentaNegocio();
            List<DetalleVenta> listDetalle = (List<DetalleVenta>)Session["listDetalle"];
            Usuario usuario = (Usuario)Session["UsurioVenta"];
            //Cliente cliente = (Cliente)Session["cliente"];
            
            Cliente cliente = new Cliente();
            cliente.IdCliente = 2;
            ven.NFactura = "SD454ASD";


            ven.Detalle = listDetalle;
            ven.Usuario = usuario;
            ven.Cliente = cliente;
            ven.Total = Convert.ToDecimal(lblTotal.Text);
            ven.Fecha = DateTime.Now;

            if (negVenta.Agregar(ven))
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "HidePopup", "openModalFinFin();", true);
            }
        }

        protected void btnAbrirModalFinalizar_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalFin();", true);
        }

        private void UpdateTotal()
        {
            List<DetalleVenta> listDetalle = (List<DetalleVenta>)Session["listDetalle"];
            lblTotal.Text = "";
            decimal total = 0;
            foreach (DetalleVenta item in listDetalle)
            {
                total += item.PrecioParcial;
            }
            lblTotal.Text = Convert.ToString(total);
            if (total==0)
            {
                lblTotal.Text = "";
                btnAbrirModalFinalizar.Enabled=false;
            }
            else
            {
                btnAbrirModalFinalizar.Enabled = true;
            }
        }

        protected void btnFin_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/ComprasVentas.aspx");
        }

        protected void btnCancelarVenta_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/ComprasVentas.aspx");
        }
    }
}