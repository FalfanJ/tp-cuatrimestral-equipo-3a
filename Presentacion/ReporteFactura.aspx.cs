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
            // Validar sesión
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
            // No se carga nada al inicio
        }

        protected void btnBuscarVenta_Click(object sender, EventArgs e)
        {
            try
            {
                lblError.Text = "";
                pnlFactura.Visible = false;

                if (string.IsNullOrWhiteSpace(txtNumVenta.Text))
                {
                    lblError.Text = "Debe ingresar un número de venta.";
                    return;
                }

                if (!long.TryParse(txtNumVenta.Text.Trim(), out long idVenta))
                {
                    lblError.Text = "El número de venta no es válido.";
                    return;
                }

                // === 1. Obtener la venta de la lista ===
                VentaNegocio ventaNegocio = new VentaNegocio();
                List<Venta> ventas = ventaNegocio.Listar();
                Venta venta = ventas.FirstOrDefault(v => v.IdVenta == idVenta);

                if (venta == null)
                {
                    lblError.Text = "No se encontró una venta con ese número.";
                    return;
                }

                // === 2. Obtener los detalles de esa venta ===
                DetalleVentaNegocio detalleNegocio = new DetalleVentaNegocio();
                List<DetalleVenta> detalles = detalleNegocio.Listar()
                    .Where(d => d.IdVenta == idVenta)
                    .ToList();

                if (detalles.Count == 0)
                {
                    lblError.Text = "No hay detalles asociados a esta venta.";
                    return;
                }

                // === 3. Cargar los datos del cliente (si existen) ===
                // Como tu VentaNegocio no los llena, verificamos nulls
                string nombreCliente = venta.Cliente?.Nombre ?? $"Cliente #{venta.Cliente?.IdCliente}";
                string cuitCliente = venta.Cliente.Cuit.HasValue ? venta.Cliente.Cuit.Value.ToString() : "No informado";
                string direccionCliente = venta.Cliente?.Direccion ?? "No disponible";

                litNombreCliente.Text = nombreCliente;
                litCuitCliente.Text = "CUIT: " + cuitCliente;
                litDireccionCliente.Text = "Dirección: " + direccionCliente;


                // === 4. Cargar cabecera de factura ===
                litNumeroFactura.Text = venta.NFactura;
                litFechaFactura.Text = venta.Fecha.ToString("dd/MM/yyyy");

                // === 5. Cargar detalle ===
                gvDetallesVenta.DataSource = detalles;
                gvDetallesVenta.DataBind();

                // === 6. Total ===
                decimal total = detalles.Sum(d => d.PrecioParcial);
                litTotalFactura.Text = total.ToString("C");

                // === 7. Mostrar factura ===
                pnlFactura.Visible = true;
            }
            catch (Exception ex)
            {
                lblError.Text = "Error al buscar la venta: " + ex.Message;
                pnlFactura.Visible = false;
            }
        }
    }
}
