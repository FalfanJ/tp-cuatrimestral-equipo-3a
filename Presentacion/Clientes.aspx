<%@ Page Title="Gestión de Clientes" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Clientes.aspx.cs" Inherits="Presentacion.Clientes" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <style>
        body {
            background: linear-gradient(135deg, #2735F5 0%, #4D079C 100%) !important;
            background-attachment: fixed;
            background-size: cover;
            min-height: 100vh;
        }

        h2, .text-white-title {
            color: white !important;
            text-shadow: 0 2px 4px rgba(0,0,0,0.3);
        }

        .card-custom {
            border: none;
            border-radius: 15px;
            box-shadow: 0 8px 20px rgba(0,0,0,0.25);
            background-color: #ffffff;
            overflow: hidden;
        }

        .header-gradient-bg {
            background: linear-gradient(to right, #2735F5, #4D079C);
            color: white;
            padding: 15px 20px;
        }
        
        .header-red-bg {
             background-color: #D10000;
             color: white;
             padding: 15px 20px;
        }

        /* --- BOTONES RELLENOS --- */

        .btn-action-green {
            background-color: #8BD100; border: none; color: white; font-weight: 600;
            transition: transform 0.2s;
        }
        .btn-action-green:hover { background-color: #75b300; color: white; transform: scale(1.05); }

        .btn-action-red {
            background-color: #D10000; border: none; color: white; font-weight: 600;
            transition: transform 0.2s;
        }
        .btn-action-red:hover { background-color: #a30000; color: white; transform: scale(1.05); }

        .btn-general-blue {
             background-color: #8FADFA; border: none; color: white; font-weight: 600;
             transition: transform 0.2s;
        }
        .btn-general-blue:hover { background-color: #6c94f7; color: white; transform: scale(1.05); }

        .form-label { font-weight: 600; color: #4D079C; }
    </style>

    <div class="container pb-5">

        <asp:UpdatePanel ID="UpdatePanelGrid" runat="server" UpdateMode="Conditional">
            <ContentTemplate>

                <asp:HiddenField ID="hfIdClienteEliminar" runat="server" />
                <asp:HiddenField ID="hfIdPersonaEliminar" runat="server" />

                <div class="row mb-4 mt-4 align-items-center">
                    <div class="col-md-6">
                        <h2 class="fw-bold"><i class="fas fa-users me-2"></i>Gestión de Clientes</h2>
                    </div>
                    <div class="col-md-6 text-md-end">
                        <asp:LinkButton ID="btnAbrirModalNuevo" runat="server" CssClass="btn btn-action-green btn-lg shadow" OnClick="btnAbrirModalNuevo_Click">
                             <i class="fas fa-plus-circle me-1"></i> Nuevo Cliente
                        </asp:LinkButton>
                    </div>
                </div>

                <%-- BARRA DE BÚSQUEDA --%>
                <div class="card card-custom mb-4">
                    <div class="card-header header-gradient-bg">
                        <h5 class="mb-0"><i class="fas fa-search me-2"></i>Filtros</h5>
                    </div>
                    <div class="card-body">
                        <div class="row g-3">
                            <div class="col-md-8">
                                <div class="input-group">
                                    <asp:TextBox ID="txtBuscar" runat="server" CssClass="form-control" placeholder="Buscar por Nombre, Apellido o DNI..."></asp:TextBox>
                                    <asp:Button ID="btnBuscar" runat="server" Text="Buscar" CssClass="btn btn-action-green" OnClick="btnBuscar_Click" />
                                    <asp:Button ID="btnLimpiar" runat="server" Text="Limpiar" CssClass="btn btn-general-blue" OnClick="btnLimpiar_Click" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

                <%-- GRILLA --%>
                <div class="card card-custom shadow">
                    <div class="card-body p-0">
                        <div class="table-responsive">
                            <asp:GridView ID="gvClientes" runat="server" CssClass="table table-hover table-striped align-middle mb-0"
                                AutoGenerateColumns="False" DataKeyNames="IdCliente" GridLines="None"
                                OnRowCommand="gvClientes_RowCommand" EmptyDataText="No se encontraron clientes.">

                                <HeaderStyle BackColor="White" ForeColor="#1A0047" BorderStyle="None" Height="50px" Font-Bold="True" />
                                
                                <Columns>
                                    <asp:BoundField DataField="IdCliente" HeaderText="ID" />
                                    <asp:BoundField DataField="Nombre" HeaderText="Nombre" />
                                    <asp:BoundField DataField="Apellido" HeaderText="Apellido" />
                                    <asp:BoundField DataField="Email" HeaderText="Email" />
                                    <asp:BoundField DataField="Telefono" HeaderText="Teléfono" />
                                    <asp:BoundField DataField="Dni" HeaderText="DNI" />
                                    
                                    <asp:TemplateField HeaderText="Acciones" ItemStyle-CssClass="text-center">
                                        <ItemTemplate>
                                            <%-- Botón Editar: Relleno Verde --%>
                                            <asp:LinkButton ID="btnEditar" runat="server" CommandName="EditarCliente"
                                                CommandArgument='<%# Eval("IdCliente") + ";" + Eval("IdPersona") %>'
                                                CssClass="btn btn-sm btn-action-green shadow-sm me-1" ToolTip="Editar">
                                                <i class="fas fa-pencil-alt"></i> Editar
                                            </asp:LinkButton>

                                            <%-- Botón Eliminar: Relleno Rojo --%>
                                            <asp:LinkButton ID="btnEliminar" runat="server" CommandName="EliminarCliente"
                                                CommandArgument='<%# Eval("IdCliente") + ";" + Eval("IdPersona") + ";" + Eval("Nombre") + " " + Eval("Apellido") %>'
                                                CssClass="btn btn-sm btn-action-red shadow-sm" ToolTip="Eliminar">
                                                <i class="fas fa-trash-alt"></i> Eliminar
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>

    <%-- MODAL ÚNICO (NUEVO / EDITAR) --%>
    <div class="modal fade" id="modalFormularioCliente" tabindex="-1" aria-hidden="true">
        <div class="modal-dialog modal-lg modal-dialog-centered">
            <div class="modal-content card-custom">
                <asp:UpdatePanel ID="UpdatePanelFormulario" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        
                        <asp:HiddenField ID="hfIdCliente" runat="server" />
                        <asp:HiddenField ID="hfIdPersona" runat="server" />

                        <div class="modal-header header-gradient-bg">
                            <h5 class="modal-title fw-bold"><%= TituloModal %></h5>
                            <button type="button" class="btn-close btn-close-white" data-bs-dismiss="modal"></button>
                        </div>
                        <div class="modal-body p-4">
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
                        <div class="modal-footer bg-light">
                            <button type="button" class="btn btn-general-blue" data-bs-dismiss="modal">Cancelar</button>
                            <asp:Button ID="btnGuardar" runat="server" Text="Guardar" CssClass="btn btn-action-green px-4" OnClick="btnGuardar_Click" />
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>

    <%-- MODAL CONFIRMAR ELIMINAR --%>
    <div class="modal fade" id="modalEliminarCliente" tabindex="-1" aria-hidden="true">
        <div class="modal-dialog modal-dialog-centered">
            <div class="modal-content card-custom">
                <asp:UpdatePanel ID="UpdatePanelEliminar" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <div class="modal-header header-red-bg">
                            <h5 class="modal-title fw-bold">🗑️ Eliminar Cliente</h5>
                            <button type="button" class="btn-close btn-close-white" data-bs-dismiss="modal" aria-label="Close"></button>
                        </div>
                        <div class="modal-body text-center py-4">
                            <i class="fas fa-exclamation-circle text-danger fa-3x mb-3"></i>
                            <p class="fs-5">¿Estás seguro de que deseas eliminar este cliente?</p>
                            <p><strong><asp:Literal ID="lblNombreClienteEliminar" runat="server"></asp:Literal></strong></p>
                            <p class="text-muted small">Esta acción no se puede deshacer.</p>
                        </div>
                        <div class="modal-footer bg-light">
                            <button type="button" class="btn btn-general-blue" data-bs-dismiss="modal">Cancelar</button>
                            <asp:Button ID="btnConfirmarEliminar" runat="server" Text="Eliminar" CssClass="btn btn-action-red" OnClick="btnConfirmarEliminar_Click" />
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>

    <%-- TOAST NOTIFICACIONES --%>
    <div aria-live="polite" aria-atomic="true" style="position: fixed; top: 80px; right: 20px; z-index: 1060;">
        <div class="toast hide shadow-lg align-items-center border-0" role="alert" id="liveToast">
            <div class="d-flex">
                <div class="toast-body fw-bold" id="toastBody"></div>
                <button type="button" class="btn-close btn-close-white me-2 m-auto" data-bs-dismiss="toast"></button>
            </div>
        </div>
    </div>

    <script>
        function mostrarToast(mensaje, tipo) {
            var toastEl = document.getElementById('liveToast');
            var toastBody = document.getElementById('toastBody');

            toastBody.innerText = mensaje;

            
            if (tipo === 'danger') {
                toastEl.style.backgroundColor = "#D10000";
                toastEl.classList.add('text-white');
            } else if (tipo === 'warning') {
                toastEl.style.backgroundColor = "#ffc107";
                toastEl.classList.remove('text-white');
            } else {
                toastEl.style.backgroundColor = "#8BD100";
                toastEl.classList.add('text-white');
            }

            var toast = new bootstrap.Toast(toastEl);
            toast.show();
        }
    </script>

</asp:Content>