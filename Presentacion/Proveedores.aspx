<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master"
    CodeBehind="Proveedores.aspx.cs" Inherits="Presentacion.Proveedores" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <style>
        /* Fondo degradado */
        body {
            background: linear-gradient(135deg, #2735F5 0%, #4D079C 100%) !important;
            background-attachment: fixed;
            background-size: cover;
            min-height: 100vh;
        }

        /* Títulos */
        h2, .text-white-title {
            color: white !important;
            text-shadow: 0 2px 4px rgba(0,0,0,0.3);
        }

        /* Tarjetas */
        .card-custom {
            border: none;
            border-radius: 15px;
            box-shadow: 0 8px 20px rgba(0,0,0,0.25);
            background-color: #ffffff;
            overflow: hidden;
        }

        /* Encabezados */
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

        /* --- BOTONES --- */
        
        /* Verde (#8BD100) - Para Editar y Guardar */
        .btn-action-green {
            background-color: #8BD100; border: none; color: white; font-weight: 600;
            transition: transform 0.2s;
        }
        .btn-action-green:hover { background-color: #75b300; color: white; transform: scale(1.05); }

        /* Rojo (#D10000) - Para Eliminar */
        .btn-action-red {
            background-color: #D10000; border: none; color: white; font-weight: 600;
            transition: transform 0.2s;
        }
        .btn-action-red:hover { background-color: #a30000; color: white; transform: scale(1.05); }

        /* Azul Claro (#8FADFA) - Secundarios */
        .btn-general-blue {
             background-color: #8FADFA; border: none; color: white; font-weight: 600;
             transition: transform 0.2s;
        }
        .btn-general-blue:hover { background-color: #6c94f7; color: white; transform: scale(1.05); }

        .form-label { font-weight: 600; color: #4D079C; }
    </style>

    <div class="container pb-5">
        <div class="row mb-4 mt-4 align-items-center">
            <div class="col-md-6">
                <h2 class="fw-bold"><i class="fas fa-truck me-2"></i>Gestión de Proveedores</h2>
            </div>
            <div class="col-md-6 text-md-end">
                <button type="button" class="btn btn-action-green btn-lg shadow" data-bs-toggle="modal" data-bs-target="#modalNuevoProveedor" 
                    onclick="limpiarFormulario()">
                    ✚ Nuevo Proveedor
                </button>
            </div>
        </div>

        <div class="card card-custom shadow">
            <div class="card-body p-0">
                <div class="table-responsive">
                    <asp:GridView ID="gvProveedor"
                        runat="server"
                        CssClass="table table-hover table-striped align-middle mb-0"
                        AutoGenerateColumns="False"
                        DataKeyNames="IdProveedor"
                        GridLines="None"
                        OnRowCommand="gvProveedor_RowCommand">

                        <HeaderStyle BackColor="White" ForeColor="#1A0047" BorderStyle="None" Height="50px" Font-Bold="True" />

                        <Columns>
                            <asp:BoundField DataField="IdProveedor" HeaderText="ID" />
                            <asp:BoundField DataField="Nombre" HeaderText="Nombre" />
                            <asp:BoundField DataField="CUIT" HeaderText="CUIT" />
                            <asp:BoundField DataField="Direccion" HeaderText="Dirección" />
                            <asp:BoundField DataField="Telefono" HeaderText="Teléfono" />
                            <asp:BoundField DataField="Email" HeaderText="Email" />

                            <asp:TemplateField HeaderText="Acciones" ItemStyle-CssClass="text-center">
                                <ItemTemplate>
                                    <%-- Botón Editar: Diseño original (Texto) con color Verde Nuevo --%>
                                    <asp:Button runat="server" CommandName="Editar" CommandArgument='<%# Eval("IdProveedor") %>'
                                        Text="Editar" CssClass="btn btn-action-green btn-sm me-2" />

                                    <%-- Botón Eliminar: Diseño original (Texto) con color Rojo Nuevo --%>
                                    <button type="button" class="btn btn-action-red btn-sm"
                                        data-bs-toggle="modal"
                                        data-bs-target="#modalEliminarProveedor"
                                        onclick="setProveedorAEliminar('<%# Eval("IdProveedor") %>', '<%# Eval("Nombre") %>')">
                                        Eliminar
                                    </button>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
        </div>
    </div>

    <%-- MODAL NUEVO / EDITAR --%>
    <div class="modal fade" id="modalNuevoProveedor" tabindex="-1" aria-labelledby="modalNuevoProveedorLabel" aria-hidden="true">
        <div class="modal-dialog modal-lg modal-dialog-centered">
            <div class="modal-content card-custom">
                <div class="modal-header header-gradient-bg">
                    <h5 class="modal-title fw-bold" id="modalNuevoProveedorLabel"><%= tituloModal %></h5>
                    <button type="button" class="btn-close btn-close-white" data-bs-dismiss="modal" aria-label="Cerrar"></button>
                </div>
                <div class="modal-body p-4">
                    <asp:HiddenField ID="hfIdProveedor" runat="server" />
                    <asp:Panel ID="pnlNuevoProveedor" runat="server">
                        <div class="row g-3">
                            <div class="col-md-6">
                                <label for="txtNombre" class="form-label">Nombre</label>
                                <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control" placeholder="Ej: Proveedor S.A."></asp:TextBox>
                            </div>
                            <div class="col-md-6">
                                <label for="txtCUIT" class="form-label">CUIT</label>
                                <asp:TextBox ID="txtCUIT" runat="server" CssClass="form-control" placeholder="Ej: 30-12345678-9"></asp:TextBox>
                            </div>
                            <div class="col-md-6">
                                <label for="txtDireccion" class="form-label">Dirección</label>
                                <asp:TextBox ID="txtDireccion" runat="server" CssClass="form-control" placeholder="Calle Falsa 123"></asp:TextBox>
                            </div>
                            <div class="col-md-6">
                                <label for="txtTelefono" class="form-label">Teléfono</label>
                                <asp:TextBox ID="txtTelefono" runat="server" CssClass="form-control" placeholder="Ej: +54 11 1234-5678"></asp:TextBox>
                            </div>
                            <div class="col-md-6">
                                <label for="txtEmail" class="form-label">Email</label>
                                <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" placeholder="proveedor@mail.com"></asp:TextBox>
                            </div>
                        </div>
                    </asp:Panel>
                </div>
                <div class="modal-footer bg-light">
                    <button type="button" class="btn btn-general-blue" data-bs-dismiss="modal">Cerrar</button>
                    <asp:Button ID="btnGuardarProveedor" runat="server" CssClass="btn btn-action-green px-4"
                        Text="Guardar" OnClick="btnGuardarProveedor_Click" />
                </div>
            </div>
        </div>
    </div>

    <%-- MODAL ELIMINAR --%>
    <div class="modal fade" id="modalEliminarProveedor" tabindex="-1" aria-labelledby="modalEliminarProveedorLabel" aria-hidden="true">
        <div class="modal-dialog modal-dialog-centered">
            <div class="modal-content card-custom">
                <div class="modal-header header-red-bg">
                    <h5 class="modal-title fw-bold" id="modalEliminarProveedorLabel">Eliminar Proveedor</h5>
                    <button type="button" class="btn-close btn-close-white" data-bs-dismiss="modal" aria-label="Cerrar"></button>
                </div>
                <div class="modal-body text-center py-4">
                    <i class="fas fa-exclamation-circle text-danger fa-3x mb-3"></i>
                    <asp:HiddenField ID="hfIdProveedorEliminar" runat="server" />
                    <p class="fs-5">¿Estás seguro de que deseas eliminar al proveedor <strong id="nombreProveedorAEliminar"></strong>?</p>
                </div>
                <div class="modal-footer bg-light">
                    <button type="button" class="btn btn-general-blue" data-bs-dismiss="modal">Cancelar</button>
                    <asp:Button ID="btnEliminarProveedorConfirmado" runat="server" CssClass="btn btn-action-red"
                        Text="Confirmar Eliminación" OnClick="btnEliminarProveedorConfirmado_Click" />
                </div>
            </div>
        </div>
    </div>

    <div aria-live="polite" aria-atomic="true" style="position: fixed; top: 20px; right: 20px; z-index: 1060;">
        <div class="toast hide shadow-lg align-items-center border-0" role="alert" id="liveToast">
            <div class="d-flex">
                <div class="toast-body fw-bold" id="toastBody"></div>
                <button type="button" class="btn-close btn-close-white me-2 m-auto" data-bs-dismiss="toast"></button>
            </div>
        </div>
    </div>

    <script>
        function setProveedorAEliminar(id, nombre) {
            document.getElementById('<%= hfIdProveedorEliminar.ClientID %>').value = id;
            document.getElementById('nombreProveedorAEliminar').textContent = nombre;
        }

        function limpiarFormulario() {
            document.getElementById('<%= hfIdProveedor.ClientID %>').value = "";
            document.getElementById('<%= txtNombre.ClientID %>').value = "";
            document.getElementById('<%= txtCUIT.ClientID %>').value = "";
            document.getElementById('<%= txtDireccion.ClientID %>').value = "";
            document.getElementById('<%= txtTelefono.ClientID %>').value = "";
            document.getElementById('<%= txtEmail.ClientID %>').value = "";
            document.getElementById('modalNuevoProveedorLabel').innerText = "Nuevo Proveedor";
        }

        function mostrarToast(mensaje, tipo) {
            var toastEl = document.getElementById('liveToast');
            var toastBody = document.getElementById('toastBody');

            toastBody.innerText = mensaje;

            if (tipo === 'danger') {
                toastEl.style.backgroundColor = "#D10000";
                toastEl.classList.add('text-white');
            } else {
                toastEl.style.backgroundColor = "#8BD100";
                toastEl.classList.add('text-white');
            }

            var toast = new bootstrap.Toast(toastEl);
            toast.show();
        }
    </script>

</asp:Content>