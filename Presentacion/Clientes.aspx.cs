using Dominio;
using Negocio;
using Presentacion.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Presentacion
{
    public partial class Clientes : System.Web.UI.Page
    {
        private ClienteNegocio negocio = new ClienteNegocio();

        protected string TituloModal = "➕ Nuevo Cliente";

        protected void Page_Load(object sender, EventArgs e)
        {
            // Validar sesión
            if (Session["usuario"] == null)
            {
                Response.Redirect("Login.aspx");
                return;
            }

            dynamic usuario = Session["usuario"];

          
        }

        // --- CARGA DE DATOS ---
        private void CargarGrid(string filtro = "")
        {
            try
            {
                List<Cliente> lista = negocio.Listar();

                if (!string.IsNullOrEmpty(filtro))
                {
                    filtro = filtro.ToLower();
                    lista = lista.Where(c =>
                        (c.Nombre != null && c.Nombre.ToLower().Contains(filtro)) ||
                        (c.Apellido != null && c.Apellido.ToLower().Contains(filtro)) ||
                        (c.Dni.HasValue && c.Dni.Value.ToString().Contains(filtro))
                    ).ToList();
                }

                gvClientes.DataSource = lista;
                gvClientes.DataBind();
                UpdatePanelGrid.Update();
            }
            catch (Exception ex)
            {
                MostrarMensaje($"Error al cargar clientes: {ex.Message}", "danger");
            }
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            string texto = txtBuscar.Text.Trim();
            CargarGrid(texto);
        }

        protected void btnLimpiar_Click(object sender, EventArgs e)
        {
            txtBuscar.Text = string.Empty;
            CargarGrid();
        }

        // --- ROW COMMAND (EDITAR Y PREPARAR ELIMINAR) ---
        protected void gvClientes_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "EditarCliente")
            {
                try
                {
                    string[] args = e.CommandArgument.ToString().Split(';');
                    long idCliente = Convert.ToInt64(args[0]);
                    long idPersona = Convert.ToInt64(args[1]);

                    CargarDatosEnModal(idCliente, idPersona);

                    TituloModal = "✏️ Editar Cliente";
                    UpdatePanelFormulario.Update();

                    // Abrir modal
                    ScriptManager.RegisterStartupScript(this, GetType(), "abrirModal",
                        "var myModal = new bootstrap.Modal(document.getElementById('modalFormularioCliente')); myModal.show();", true);
                }
                catch (Exception ex)
                {
                    MostrarMensaje($"Error al cargar edición: {ex.Message}", "danger");
                }
            }
            else if (e.CommandName == "EliminarCliente")
            {
                try
                {
                    string[] args = e.CommandArgument.ToString().Split(';');

                    hfIdClienteEliminar.Value = args[0];
                    hfIdPersonaEliminar.Value = args[1];
                    string nombreCompleto = args.Length > 2 ? args[2] : "este cliente";

                    lblNombreClienteEliminar.Text = nombreCompleto;
                    UpdatePanelEliminar.Update();

                    // Abrir modal eliminar
                    ScriptManager.RegisterStartupScript(this, GetType(), "abrirModalEliminar",
                        "var myModal = new bootstrap.Modal(document.getElementById('modalEliminarCliente')); myModal.show();", true);
                }
                catch (Exception ex)
                {
                    MostrarMensaje($"Error al preparar eliminación: {ex.Message}", "danger");
                }
            }
        }

        private void CargarDatosEnModal(long idCliente, long idPersona)
        {
            Cliente cliente = negocio.Listar().FirstOrDefault(c => c.IdCliente == idCliente);

            if (cliente != null)
            {
                // Guardamos IDs para saber que estamos editando
                hfIdCliente.Value = cliente.IdCliente.ToString();
                hfIdPersona.Value = cliente.IdPersona.ToString();

                txtNombre.Text = cliente.Nombre;
                txtApellido.Text = cliente.Apellido;
                txtEmail.Text = cliente.Email;
                txtTelefono.Text = cliente.Telefono.ToString();
                txtDNI.Text = cliente.Dni.HasValue ? cliente.Dni.Value.ToString() : "";
                txtCUIT.Text = cliente.Cuit.HasValue ? cliente.Cuit.Value.ToString() : "";
                txtDireccion.Text = cliente.Direccion;
            }
        }

        // --- BOTÓN GUARDAR (NUEVO O EDITAR) ---
        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtNombre.Text) || string.IsNullOrWhiteSpace(txtApellido.Text) || string.IsNullOrWhiteSpace(txtEmail.Text))
                {
                    MostrarMensaje("Por favor, complete Nombre, Apellido y Email.", "warning");
                    return;
                }

                if (!Regex.IsMatch(txtEmail.Text.Trim(), @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
                {
                    MostrarMensaje("Ingrese un email válido.", "warning");
                    return;
                }

                Cliente cliente = new Cliente();
                cliente.Nombre = txtNombre.Text.Trim();
                cliente.Apellido = txtApellido.Text.Trim();
                cliente.Email = txtEmail.Text.Trim();
                cliente.Direccion = txtDireccion.Text.Trim();
                cliente.TipoPersona = true;

                if (long.TryParse(txtTelefono.Text.Trim(), out long tel)) cliente.Telefono = tel;
                if (long.TryParse(txtDNI.Text.Trim(), out long dni)) cliente.Dni = dni;
                if (long.TryParse(txtCUIT.Text.Trim(), out long cuit)) cliente.Cuit = cuit;

                // 3. Decidir si es Insert o Update
                if (string.IsNullOrEmpty(hfIdCliente.Value))
                {
                    // NUEVO
                    negocio.Agregar(cliente);
                    MostrarMensaje("Cliente agregado correctamente.", "success");
                }
                else
                {
                    // EDITAR
                    cliente.IdCliente = Convert.ToInt64(hfIdCliente.Value);
                    cliente.IdPersona = Convert.ToInt64(hfIdPersona.Value); // Necesario para tu lógica de negocio
                    negocio.Modificar(cliente);
                    MostrarMensaje("Cliente modificado correctamente.", "success");
                }

                CargarGrid();
                LimpiarCamposModal();

                // Cerrar modal
                ScriptManager.RegisterStartupScript(this, GetType(), "cerrarModal", "$('#modalFormularioCliente').modal('hide');", true);
            }
            catch (Exception ex)
            {
                MostrarMensaje($"Error al guardar: {ex.Message}", "danger");
            }
        }

        protected void btnConfirmarEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(hfIdClienteEliminar.Value)) return;

                long idCliente = Convert.ToInt64(hfIdClienteEliminar.Value);
                long idPersona = Convert.ToInt64(hfIdPersonaEliminar.Value);

                negocio.BajaLogica(idPersona, idCliente);
                CargarGrid();

                MostrarMensaje("Cliente eliminado correctamente.", "success");

                // Limpiar IDs
                hfIdClienteEliminar.Value = "";
                hfIdPersonaEliminar.Value = "";

                // Cerrar modal
                ScriptManager.RegisterStartupScript(this, GetType(), "cerrarModalEliminar", "$('#modalEliminarCliente').modal('hide');", true);
            }
            catch (Exception ex)
            {
                MostrarMensaje($"Error al eliminar: {ex.Message}", "danger");
            }
        }

       
        protected void btnAbrirModalNuevo_Click(object sender, EventArgs e)
        {
            LimpiarCamposModal();
            TituloModal = "➕ Nuevo Cliente";
            ScriptManager.RegisterStartupScript(this, GetType(), "abrirModalNuevo",
                   "var myModal = new bootstrap.Modal(document.getElementById('modalFormularioCliente')); myModal.show();", true);
        }

        private void LimpiarCamposModal()
        {
            hfIdCliente.Value = string.Empty;
            hfIdPersona.Value = string.Empty;
            txtNombre.Text = "";
            txtApellido.Text = "";
            txtEmail.Text = "";
            txtTelefono.Text = "";
            txtDNI.Text = "";
            txtCUIT.Text = "";
            txtDireccion.Text = "";
            TituloModal = "➕ Nuevo Cliente";
        }

        private void MostrarMensaje(string mensaje, string tipo)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "toast",
                $"mostrarToast('{mensaje.Replace("'", "")}','{tipo}');", true);
        }
    }
}