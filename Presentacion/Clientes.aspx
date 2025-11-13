<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Clientes.aspx.cs" Inherits="Presentacion.Clientes" %>



<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    

    <asp:HiddenField ID="hdnClienteID" runat="server" Value="0" />
    <asp:HiddenField ID="hdnPersonaID" runat="server" Value="0" />

    <div class="container mt-4">
        
        <div class="d-flex justify-content-between align-items-center mb-3">
            <h2 class="mb-0">Gestión de Clientes</h2>
            <button type="button" class="btn btn-success" data-bs-toggle="modal" data-bs-target="#modalNuevoCliente">
                Nuevo Cliente
            </button>
        </div>

        <div class="row mb-3 mt-4">
            <div class="col-md-8 col-lg-6">
                <div class="input-group shadow-sm">
                    <asp:TextBox ID="txtBuscar" runat="server" CssClass="form-control" placeholder="Buscar cliente por Nombre, Apellido o DNI..."></asp:TextBox>
                    <asp:Button ID="btnBuscar" runat="server" Text="Buscar" CssClass="btn btn-primary" OnClick="btnBuscar_Click" />
                </div>
            </div>
        </div>

        <asp:UpdatePanel ID="UpdatePanelGrid" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <div class="table-responsive shadow-sm rounded mt-4">
                    <asp:GridView ID="gvClientes"
                        runat="server"
                        CssClass="table table-striped table-hover align-middle"
                        AutoGenerateColumns="False"
                        DataKeyNames="IdCliente"
                        GridLines="None"
                        OnRowCommand="gvClientes_RowCommand"
                        EmptyDataText="No se encontraron clientes.">

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
                                    <div class="d-flex gap-2 justify-content-center">
                                        <asp:LinkButton ID="btnEditar" runat="server"
                                            data-bs-toggle="modal" data-bs-target="#modalEditarCliente"
                                            CssClass="btn btn-warning btn-sm"
                                            CommandName="EditarCliente"
                                            CommandArgument='<%# Eval("IdCliente") + ";" + Eval("IdPersona") %>'>
                                            ✏️ Editar
                                        </asp:LinkButton>
                                        
                                        <asp:LinkButton data-bs-toggle="modal" data-bs-target="#modalEliminarCliente" runat="server"
                                            CssClass="btn btn-danger btn-sm"
                                            CommandName="EliminarCliente"
                                            CommandArgument='<%# Eval("IdCliente") + ";" + Eval("IdPersona") + ";" + Eval("Nombre") + " " + Eval("Apellido") %>'>
                                            🗑️ Eliminar
                                        </asp:LinkButton>
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
            </ContentTemplate>
            <Triggers>
    <asp:AsyncPostBackTrigger ControlID="btnBuscar" EventName="Click" />
    <asp:AsyncPostBackTrigger ControlID="gvClientes" EventName="RowCommand" />
</Triggers>
        </asp:UpdatePanel>

    </div>

    <div class="modal fade" id="modalNuevoCliente" tabindex="-1" aria-labelledby="modalNuevoClienteLabel" aria-hidden="true">
        <div class="modal-dialog modal-lg modal-dialog-centered">
            <div class="modal-content">
                <asp:UpdatePanel ID="UpdatePanelNuevo" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <div class="modal-header bg-success text-white">
                            <h5 class="modal-title" id="modalNuevoClienteLabel">➕ Nuevo Cliente</h5>
                            <button type="button" class="btn-close btn-close-white" data-bs-dismiss="modal" aria-label="Cerrar"></button>
                        </div>
                        <div class="modal-body">
                            <div class="row g-3">
                                <div class="col-md-6">
                                    <label for="txtNombre" class="form-label">Nombre</label>
                                    <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control" placeholder="Ej: Juan"></asp:TextBox>
                                </div>
                                <div class="col-md-6">
                                    <label for="txtApellido" class="form-label">Apellido</label>
                                    <asp:TextBox ID="txtApellido" runat="server" CssClass="form-control" placeholder="Ej: Pérez"></asp:TextBox>
                                </div>
                                <div class="col-md-6">
                                    <label for="txtEmail" class="form-label">Email</label>
                                    <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" placeholder="cliente@mail.com" TextMode="Email"></asp:TextBox>
                                </div>
                                <div class="col-md-6">
                                    <label for="txtTelefono" class="form-label">Teléfono</label>
                                    <asp:TextBox ID="txtTelefono" runat="server" CssClass="form-control" placeholder="Ej: +54 11 1234-5678"></asp:TextBox>
                                </div>
                                <div class="col-md-6">
                                    <label for="txtDNI" class="form-label">DNI</label>
                                    <asp:TextBox ID="txtDNI" runat="server" CssClass="form-control" placeholder="28123456"></asp:TextBox>
                                </div>
                                <div class="col-md-6">
                                    <label for="txtCUIT" class="form-label">CUIT</label>
                                    <asp:TextBox ID="txtCUIT" runat="server" CssClass="form-control" placeholder="20-28123456-5"></asp:TextBox>
                                </div>
                                <div class="col-12">
                                    <label for="txtDireccion" class="form-label">Dirección</label>
                                    <asp:TextBox ID="txtDireccion" runat="server" CssClass="form-control" placeholder="Calle Falsa 123"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="modal-footer">
                            <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cancelar</button>
                            <asp:Button ID="btnGuardarNuevo" runat="server" Text="Guardar Cliente" CssClass="btn btn-success" OnClick="btnGuardarNuevo_Click" />
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>

    <div class="modal fade" id="modalEditarCliente" tabindex="-1" aria-labelledby="modalEditarClienteLabel" aria-hidden="true">
        <div class="modal-dialog modal-lg modal-dialog-centered">
            <div class="modal-content">
                <asp:UpdatePanel ID="UpdatePanelEditar" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <div class="modal-header bg-warning text-dark">
                            <h5 class="modal-title" id="modalEditarClienteLabel">✏️ Editar Cliente</h5>
                            <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Cerrar"></button>
                        </div>
                        <div class="modal-body">
                            <div class="row g-3">
                                <div class="col-md-6">
                                    <label for="txtEditNombre" class="form-label">Nombre</label>
                                    <asp:TextBox ID="txtEditNombre" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="col-md-6">
                                    <label for="txtEditApellido" class="form-label">Apellido</label>
                                    <asp:TextBox ID="txtEditApellido" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="col-md-6">
                                    <label for="txtEditEmail" class="form-label">Email</label>
                                    <asp:TextBox ID="txtEditEmail" runat="server" CssClass="form-control" TextMode="Email"></asp:TextBox>
                                </div>
                                <div class="col-md-6">
                                    <label for="txtEditTelefono" class="form-label">Teléfono</label>
                                    <asp:TextBox ID="txtEditTelefono" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="col-md-6">
                                    <label for="txtEditDNI" class="form-label">DNI</label>
                                    <asp:TextBox ID="txtEditDNI" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="col-md-6">
                                    <label for="txtEditCUIT" class="form-label">CUIT</label>
                                    <asp:TextBox ID="txtEditCUIT" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="col-12">
                                    <label for="txtEditDireccion" class="form-label">Dirección</label>
                                    <asp:TextBox ID="txtEditDireccion" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="modal-footer">
                            <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cancelar</button>
                            <asp:Button ID="btnGuardarEdicion" runat="server" Text="Guardar Cambios" CssClass="btn btn-warning" OnClick="btnGuardarEdicion_Click" />
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>

    <div class="modal fade" id="modalEliminarCliente" tabindex="-1" aria-labelledby="modalEliminarClienteLabel" aria-hidden="true">
        <div class="modal-dialog modal-dialog-centered">
            <div class="modal-content">
                <asp:UpdatePanel ID="UpdatePanelEliminar" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <div class="modal-header bg-danger text-white">
                            <h5 class="modal-title" id="modalEliminarClienteLabel">🗑️ Eliminar Cliente</h5>
                            <button type="button" class="btn-close btn-close-white" data-bs-dismiss="modal" aria-label="Cerrar"></button>
                        </div>
                        <div class="modal-body text-center">
                            <p class="fs-5">¿Estás seguro de que deseas eliminar este cliente?</p>
                            <p><strong><asp:Literal ID="lblNombreClienteEliminar" runat="server"></asp:Literal></strong></p>
                            <p class="text-muted mb-0">Esta acción no se puede deshacer.</p>
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
</asp:Content>