using Dominio;
using Negocio;
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

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarGrid();
            }
        }

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
            }
            catch (Exception ex)
            {
              
                throw ex;
            }
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            string texto = txtBuscar.Text.Trim();
            CargarGrid(texto);
        }

        protected void gvClientes_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "EditarCliente")
            {
                string[] args = e.CommandArgument.ToString().Split(';');
                long idCliente = Convert.ToInt64(args[0]);
                long idPersona = Convert.ToInt64(args[1]);

                hdnClienteID.Value = idCliente.ToString();
                hdnPersonaID.Value = idPersona.ToString();

                // Buscar cliente en la lista
                ClienteNegocio cliNeg = new ClienteNegocio();
                Cliente cliente = cliNeg.Listar().FirstOrDefault(c => c.IdCliente == idCliente);

                if (cliente != null)
                {
                    txtEditNombre.Text = cliente.Nombre;
                    txtEditApellido.Text = cliente.Apellido;
                    txtEditEmail.Text = cliente.Email;
                    txtEditTelefono.Text = cliente.Telefono.ToString();
                    txtEditDNI.Text = cliente.Dni.HasValue ? cliente.Dni.Value.ToString() : "";
                    txtEditCUIT.Text = cliente.Cuit.HasValue ? cliente.Cuit.Value.ToString() : "";
                    txtEditDireccion.Text = cliente.Direccion;
                }
                // Forzamos la actualización del panel del modal de edición
                UpdatePanelEditar.Update();

                // Mostrar modal de edición por script
                ScriptManager.RegisterStartupScript(this, GetType(), "ShowEditModal", "$('#modalEditarCliente').modal('show');", true);
            }
            else if (e.CommandName == "EliminarCliente")
            {
                string[] args = e.CommandArgument.ToString().Split(';');
                long idCliente = Convert.ToInt64(args[0]);
                long idPersona = Convert.ToInt64(args[1]);
                string nombreCompleto = args.Length > 2 ? args[2] : "";

                hdnClienteID.Value = idCliente.ToString();
                hdnPersonaID.Value = idPersona.ToString();
                lblNombreClienteEliminar.Text = nombreCompleto;

                // Forzamos la actualización del panel del modal de eliminación
                UpdatePanelEliminar.Update();
                // Mostrar modal de eliminación por script
                ScriptManager.RegisterStartupScript(this, GetType(), "ShowDeleteModal", "$('#modalEliminarCliente').modal('show');", true);
            }
        }

        protected void btnGuardarNuevo_Click(object sender, EventArgs e)
        {
            try
            {
                // --- Validar campos obligatorios ---
                if (string.IsNullOrWhiteSpace(txtNombre.Text) ||
                    string.IsNullOrWhiteSpace(txtApellido.Text) ||
                    string.IsNullOrWhiteSpace(txtEmail.Text))
                {
                    MostrarAlerta("Por favor, complete Nombre, Apellido y Email.");
                    return;
                }

                // --- Validar email ---
                if (!Regex.IsMatch(txtEmail.Text.Trim(), @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
                {
                    MostrarAlerta("Ingrese un email válido.");
                    return;
                }

                // --- Convertir campos numéricos de forma segura ---
                long telefono = 0;
                long? dni = null;
                long? cuit = null;

                if (!string.IsNullOrWhiteSpace(txtTelefono.Text))
                {
                    if (!long.TryParse(txtTelefono.Text.Trim(), out telefono))
                    {
                        MostrarAlerta("Teléfono inválido. Solo se permiten números.");
                        return;
                    }
                }

                if (!string.IsNullOrWhiteSpace(txtDNI.Text))
                {
                    if (!long.TryParse(txtDNI.Text.Trim(), out long dniVal))
                        MostrarAlerta("DNI inválido. Solo se permiten números.");
                    else
                        dni = dniVal;
                }

                if (!string.IsNullOrWhiteSpace(txtCUIT.Text))
                {
                    if (!long.TryParse(txtCUIT.Text.Trim(), out long cuitVal))
                        MostrarAlerta("CUIT inválido. Solo se permiten números.");
                    else
                        cuit = cuitVal;
                }

                // --- Crear cliente ---
                Cliente nuevo = new Cliente
                {
                    Nombre = txtNombre.Text.Trim(),
                    Apellido = txtApellido.Text.Trim(),
                    Email = txtEmail.Text.Trim(),
                    Telefono = telefono,
                    Dni = dni,
                    Cuit = cuit,
                    Direccion = txtDireccion.Text.Trim(),
                    TipoPersona = true // por defecto físico
                };

                negocio.Agregar(nuevo);
                CargarGrid();

                // Cierra el modal con JS
                ScriptManager.RegisterStartupScript(this, GetType(), "HideNewModal", "$('#modalNuevoCliente').modal('hide');", true);

                // Limpiar campos
                LimpiarCamposNuevo();

                // Mensaje de éxito
                MostrarAlerta("Cliente agregado correctamente.");
            }
            catch (Exception ex)
            {
                MostrarAlerta($"Error al guardar el cliente: {ex.Message}");
            }
        }

        // Método para mostrar alertas (puede estar en tu clase ya)
        private void MostrarAlerta(string mensaje)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alerta", $"alert('{mensaje}');", true);
        }


        private void LimpiarCamposNuevo()
        {
            txtNombre.Text = "";
            txtApellido.Text = "";
            txtEmail.Text = "";
            txtTelefono.Text = "";
            txtDNI.Text = "";
            txtCUIT.Text = "";
            txtDireccion.Text = "";
        }

        protected void btnGuardarEdicion_Click(object sender, EventArgs e)
        {
            try
            {
                Cliente modificado = new Cliente
                {
                    IdCliente = Convert.ToInt64(hdnClienteID.Value),
                    IdPersona = Convert.ToInt64(hdnPersonaID.Value),
                    Nombre = txtEditNombre.Text.Trim(),
                    Apellido = txtEditApellido.Text.Trim(),
                    Email = txtEditEmail.Text.Trim(),
                    Telefono = !string.IsNullOrEmpty(txtEditTelefono.Text) ? Convert.ToInt64(txtEditTelefono.Text) : 0,
                    Dni = !string.IsNullOrEmpty(txtEditDNI.Text) ? Convert.ToInt64(txtEditDNI.Text) : (long?)null,
                    Cuit = !string.IsNullOrEmpty(txtEditCUIT.Text) ? Convert.ToInt64(txtEditCUIT.Text) : (long?)null,
                    Direccion = txtEditDireccion.Text.Trim(),
                    TipoPersona = true
                };

                negocio.Modificar(modificado);
                CargarGrid();

                ScriptManager.RegisterStartupScript(this, GetType(), "HideEditModal", "$('#modalEditarCliente').modal('hide');", true);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void btnConfirmarEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                long idCliente = Convert.ToInt64(hdnClienteID.Value);
                long idPersona = Convert.ToInt64(hdnPersonaID.Value);

                negocio.BajaLogica(idPersona, idCliente);
                CargarGrid();

                ScriptManager.RegisterStartupScript(this, GetType(), "HideDeleteModal", "$('#modalEliminarCliente').modal('hide');", true);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
