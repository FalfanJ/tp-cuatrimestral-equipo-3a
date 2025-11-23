<%@ Page Title="Gestión de Clientes" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Clientes.aspx.cs" Inherits="Presentacion.Clientes" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div class="container mt-4">

        <asp:UpdatePanel ID="UpdatePanelGrid" runat="server" UpdateMode="Conditional">
            <ContentTemplate>

                <asp:HiddenField ID="hfIdClienteEliminar" runat="server" />
                <asp:HiddenField ID="hfIdPersonaEliminar" runat="server" />

                <div class="row mb-3">
                    <div class="col-md-6">
                        <h2><i class="fas fa-users"></i> Gestión de Clientes</h2>
                    </div>
                    <div class="col-md-6 text-md-end">
                        <asp:LinkButton ID="btnAbrirModalNuevo" runat="server" CssClass="btn btn-success" OnClick="btnAbrirModalNuevo_Click">
                             ➕ Nuevo Cliente
                        </asp:LinkButton>
                    </div>
                </div>

                <%-- BARRA DE BÚSQUEDA --%>
                <div class="card shadow-sm mb-4">
                    <div class="card-body">
                        <div class="row g-3">
                            <div class="col-md-8">
                                <div class="input-group">
                                    <asp:TextBox ID="txtBuscar" runat="server" CssClass="form-control" placeholder="Buscar por Nombre, Apellido o DNI..."></asp:TextBox>
                                    <asp:Button ID="btnBuscar" runat="server" Text="Buscar" CssClass="btn btn-primary" OnClick="btnBuscar_Click" />
                                    <asp:Button ID="btnLimpiar" runat="server" Text="Limpiar" CssClass="btn btn-outline-secondary" OnClick="btnLimpiar_Click" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

                <%-- GRILLA --%>
                <div class="table-responsive shadow-sm rounded">
                    <asp:GridView ID="gvClientes" runat="server" CssClass="table table-hover align-middle"
                        AutoGenerateColumns="False" DataKeyNames="IdCliente" GridLines="None"
                        OnRowCommand="gvClientes_RowCommand" EmptyDataText="No se encontraron clientes.">

                        <HeaderStyle CssClass="table-dark" />
                        <Columns>
                            <asp:BoundField DataField="IdCliente" HeaderText="ID" />
                            <asp:BoundField DataField="Nombre" HeaderText="Nombre" />
                            <asp:BoundField DataField="Apellido" HeaderText="Apellido" />
                            <asp:BoundField DataField="Email" HeaderText="Email" />
                            <asp:BoundField DataField="Telefono" HeaderText="Teléfono" />
                            <asp:BoundField DataField="Dni" HeaderText="DNI" />
                            
                            <asp:TemplateField HeaderText="Acciones" ItemStyle-CssClass="text-center">
                                <ItemTemplate>
                                    <%-- Botón Editar --%>
                                    <asp:LinkButton ID="btnEditar" runat="server" CommandName="EditarCliente"
                                        CommandArgument='<%# Eval("IdCliente") + ";" + Eval("IdPersona") %>'
                                        CssClass="btn btn-sm btn-outline-primary me-2" ToolTip="Editar">
                                        <i class="fas fa-pencil-alt"></i> Editar
                                    </asp:LinkButton>

                                    <%-- Botón Eliminar --%>
                                    <asp:LinkButton ID="btnEliminar" runat="server" CommandName="EliminarCliente"
                                        CommandArgument='<%# Eval("IdCliente") + ";" + Eval("IdPersona") + ";" + Eval("Nombre") + " " + Eval("Apellido") %>'
                                        CssClass="btn btn-sm btn-outline-danger" ToolTip="Eliminar">
                                        <i class="fas fa-trash-alt"></i> Eliminar
                                    </asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>

    <%-- MODAL ÚNICO (NUEVO / EDITAR) --%>
    <div class="modal fade" id="modalFormularioCliente" tabindex="-1" aria-hidden="true">
        <div class="modal-dialog modal-lg modal-dialog-centered">
            <div class="modal-content">
                <asp:UpdatePanel ID="UpdatePanelFormulario" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        
                        <asp:HiddenField ID="hfIdCliente" runat="server" />
                        <asp:HiddenField ID="hfIdPersona" runat="server" />

                        <div class="modal-header bg-primary text-white">
                            <h5 class="modal-title"><%= TituloModal %></h5>
                            <button type="button" class="btn-close btn-close-white" data-bs-dismiss="modal"></button>
                        </div>
                        <div class="modal-body">
                            <div class="row g-3">
                                <div class="col-md-6">
                                    <label class="form-label">Nombre *</label>
                                    <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="col-md-6">
                                    <label class="form-label">Apellido *</label>
                                    <asp:TextBox ID="txtApellido" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="col-md-6">
                                    <label class="form-label">Email *</label>
                                    <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" TextMode="Email"></asp:TextBox>
                                </div>
                                <div class="col-md-6">
                                    <label class="form-label">Teléfono</label>
                                    <asp:TextBox ID="txtTelefono" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="col-md-6">
                                    <label class="form-label">DNI</label>
                                    <asp:TextBox ID="txtDNI" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="col-md-6">
                                    <label class="form-label">CUIT</label>
                                    <asp:TextBox ID="txtCUIT" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="col-12">
                                    <label class="form-label">Dirección</label>
                                    <asp:TextBox ID="txtDireccion" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="modal-footer">
                            <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cancelar</button>
                            <asp:Button ID="btnGuardar" runat="server" Text="Guardar" CssClass="btn btn-primary" OnClick="btnGuardar_Click" />
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>

    <%-- MODAL CONFIRMAR ELIMINAR --%>
    <div class="modal fade" id="modalEliminarCliente" tabindex="-1" aria-hidden="true">
        <div class="modal-dialog modal-dialog-centered">
            <div class="modal-content">
                <asp:UpdatePanel ID="UpdatePanelEliminar" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <div class="modal-header bg-danger text-white">
                            <h5 class="modal-title">🗑️ Eliminar Cliente</h5>
                            <button type="button" class="btn-close btn-close-white" data-bs-dismiss="modal" aria-label="Close"></button>
                        </div>
                        <div class="modal-body text-center">
                            <p class="fs-5">¿Estás seguro de que deseas eliminar este cliente?</p>
                            <p><strong><asp:Literal ID="lblNombreClienteEliminar" runat="server"></asp:Literal></strong></p>
                            <p class="text-muted small">Esta acción no se puede deshacer.</p>
                        </div>
                        <div class="modal-footer justify-content-center">
                            <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cancelar</button>
                            <asp:Button ID="btnConfirmarEliminar" runat="server" Text="Eliminar" CssClass="btn btn-danger" OnClick="btnConfirmarEliminar_Click" />
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>

    <%-- TOAST NOTIFICACIONES --%>
    <div aria-live="polite" aria-atomic="true" style="position: fixed; top: 20px; right: 20px; z-index: 1060;">
        <div class="toast hide" role="alert" id="liveToast">
            <div class="toast-header">
                <strong class="me-auto" id="toastHeader">Sistema</strong>
                <button type="button" class="btn-close" data-bs-dismiss="toast"></button>
            </div>
            <div class="toast-body" id="toastBody"></div>
        </div>
    </div>

    <script>
        function mostrarToast(mensaje, tipo) {
            var toastEl = document.getElementById('liveToast');
            var toastBody = document.getElementById('toastBody');
            var toastHeader = document.getElementById('toastHeader');

            toastBody.innerText = mensaje;
            if (tipo === 'danger') {
                toastHeader.classList.add('text-danger');
                toastHeader.classList.remove('text-success');
                toastHeader.innerText = "Error";
            } else if (tipo === 'warning') {
                toastHeader.classList.add('text-warning');
                toastHeader.classList.remove('text-success');
                toastHeader.innerText = "Atención";
            } else {
                toastHeader.classList.remove('text-danger');
                toastHeader.classList.remove('text-warning');
                toastHeader.classList.add('text-success');
                toastHeader.innerText = "Éxito";
            }

            var toast = new bootstrap.Toast(toastEl);
            toast.show();
        }
    </script>

</asp:Content>