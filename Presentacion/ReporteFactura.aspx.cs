using Dominio;
using Negocio;
using Presentacion.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;

namespace Presentacion
{
    public partial class Factura : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["usuario"] == null)
            {
                Response.Redirect("Login.aspx");
                return;
            }

            dynamic usuario = Session["usuario"];

            if (Seguridad.EsVendedor(usuario))
            {
                Response.Redirect("Default.aspx");
                return;
            }

            if (!IsPostBack && Request.QueryString["id"] != null)
            {
                txtNumVenta.Text = Request.QueryString["id"];
                CargarFactura();
            }
        }

        protected void btnBuscarVenta_Click(object sender, EventArgs e)
        {
            CargarFactura();
        }

        private void CargarFactura()
        {
            try
            {
                pnlFactura.Visible = false;
                lblError.Visible = false;

                string numeroFactura = txtNumVenta.Text.Trim();

                if (string.IsNullOrWhiteSpace(numeroFactura))
                {
                    MostrarError("Por favor, ingrese un número de factura.");
                    return;
                }

                // --- PASO 1: BUSCAR LA VENTA ---
                VentaNegocio ventaNeg = new VentaNegocio();

                // 🔴 CORRECCIÓN AQUÍ: Usamos 'Dominio.Venta' explícitamente
                List<Dominio.Venta> listaVentas = ventaNeg.Listar();
                Dominio.Venta ventaSeleccionada = listaVentas.FirstOrDefault(x => x.NFactura == numeroFactura);

                if (ventaSeleccionada == null)
                {
                    MostrarError("No se encontró ninguna venta con ese ID.");
                    return;
                }

                // --- PASO 2: BUSCAR DETALLES ---
                DetalleVentaNegocio detalleNeg = new DetalleVentaNegocio();
                List<DetalleVenta> todosLosDetalles = detalleNeg.Listar();
                List<DetalleVenta> detallesVenta = todosLosDetalles.Where(x => x.IdVenta == ventaSeleccionada.IdVenta).ToList();

                if (detallesVenta.Count == 0)
                {
                    MostrarError("La venta existe pero no tiene productos asociados.");
                    return;
                }

                // --- PASO 3: CRUZAR CON PRODUCTOS ---
                ProductoNegocio prodNeg = new ProductoNegocio();
                List<Producto> todosLosProductos = prodNeg.Listar();

                foreach (var det in detallesVenta)
                {
                    Producto prodReal = todosLosProductos.FirstOrDefault(p => p.IdProducto == det.Producto.IdProducto);

                    if (prodReal != null)
                    {
                        det.Producto.Nombre = prodReal.Nombre;
                    }
                    else
                    {
                        det.Producto.Nombre = "(Producto eliminado)";
                    }
                }

                // --- PASO 4: CRUZAR CON CLIENTE ---
                ClienteNegocio cliNeg = new ClienteNegocio();
                List<Cliente> todosLosClientes = cliNeg.Listar();

                // Usamos ventaSeleccionada que ahora es de tipo Dominio.Venta
                Cliente clienteReal = todosLosClientes.FirstOrDefault(c => c.IdCliente == ventaSeleccionada.Cliente.IdCliente);


                // --- PASO 5: MOSTRAR DATOS ---

                lblNumeroFactura.Text = string.IsNullOrEmpty(ventaSeleccionada.NFactura) ? ventaSeleccionada.IdVenta.ToString() : ventaSeleccionada.NFactura;
                lblFecha.Text = ventaSeleccionada.Fecha.ToString("dd/MM/yyyy HH:mm");

                // Vendedor
                lblVendedor.Text = ventaSeleccionada.Usuario.IdUsuario.ToString();

                // Datos Cliente
                if (clienteReal != null)
                {
                    lblNombreCliente.Text = $"{clienteReal.Nombre} {clienteReal.Apellido}";

                    if (clienteReal.Dni.HasValue)
                        lblDniCuit.Text = $"DNI: {clienteReal.Dni}";
                    else if (clienteReal.Cuit.HasValue)
                        lblDniCuit.Text = $"CUIT: {clienteReal.Cuit}";
                    else
                        lblDniCuit.Text = "-";

                    lblDireccionCliente.Text = clienteReal.Direccion;
                    lblEmailCliente.Text = clienteReal.Email;
                }
                else
                {
                    lblNombreCliente.Text = "Consumidor Final";
                    lblDniCuit.Text = "-";
                    lblDireccionCliente.Text = "-";
                    lblEmailCliente.Text = "-";
                }

                // Grilla
                gvDetallesFactura.DataSource = detallesVenta;
                gvDetallesFactura.DataBind();

                // Total
                lblTotalPagar.Text = ventaSeleccionada.Total.ToString("C");

                pnlFactura.Visible = true;

            }
            catch (Exception ex)
            {
                MostrarError("Error al generar reporte: " + ex.Message);
            }
        }

        private void MostrarError(string msg)
        {
            lblError.Text = msg;
            lblError.Visible = true;
            pnlFactura.Visible = false;
        }

        protected void btnImprimir_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "print", "window.print();", true);
        }
    }
}