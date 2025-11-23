<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master"
    CodeBehind="Proveedores.aspx.cs" Inherits="Presentacion.Proveedores" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div class="container mt-4">
        <div class="d-flex justify-content-between align-items-center mb-3">
            <h2 class="mb-0">Gestión de Proveedores</h2>
            
            <button type="button" class="btn btn-success" data-bs-toggle="modal" data-bs-target="#modalNuevoProveedor" 
                onclick="limpiarFormulario()">
                ✚ Nuevo Proveedor
            </button>
        </div>

        <div class="table-responsive shadow-sm rounded mt-5">
            <asp:GridView ID="gvProveedor"
                runat="server"
                CssClass="table table-striped table-hover align-middle"
                AutoGenerateColumns="False"
                DataKeyNames="IdProveedor"
                GridLines="None"
                OnRowCommand="gvProveedor_RowCommand">

                <Columns>
                    <asp:BoundField DataField="IdProveedor" HeaderText="ID" />
                    <asp:BoundField DataField="Nombre" HeaderText="Nombre" />
                    <asp:BoundField DataField="CUIT" HeaderText="CUIT" />
                    <asp:BoundField DataField="Direccion" HeaderText="Dirección" />
                    <asp:BoundField DataField="Telefono" HeaderText="Teléfono" />
                    <asp:BoundField DataField="Email" HeaderText="Email" />

                    <asp:TemplateField HeaderText="Acciones">
                        <ItemTemplate>
                            <asp:Button runat="server" CommandName="Editar" CommandArgument='<%# Eval("IdProveedor") %>'
                                Text="Editar" CssClass="btn btn-warning btn-sm me-2" />

                            <button type="button" class="btn btn-danger btn-sm"
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

    <div class="modal fade" id="modalNuevoProveedor" tabindex="-1" aria-labelledby="modalNuevoProveedorLabel" aria-hidden="true">
        <div class="modal-dialog modal-lg modal-dialog-centered">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="modalNuevoProveedorLabel"><%= tituloModal %></h5>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Cerrar"></button>
                </div>
                <div class="modal-body">
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
                                <asp:TextBox ID="txtDireccion" runat="server" CssClass="form-control" placeholder="Calle Falsa 123, Buenos Aires"></asp:TextBox>
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
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cerrar</button>
                    <asp:Button ID="btnGuardarProveedor" runat="server" CssClass="btn btn-success"
                        Text="Guardar" OnClick="btnGuardarProveedor_Click" />
                </div>
            </div>
        </div>
    </div>

    <div class="modal fade" id="modalEliminarProveedor" tabindex="-1" aria-labelledby="modalEliminarProveedorLabel" aria-hidden="true">
        <div class="modal-dialog modal-dialog-centered">
            <div class="modal-content">
                <div class="modal-header bg-danger text-white">
                    <h5 class="modal-title" id="modalEliminarProveedorLabel">Eliminar Proveedor</h5>
                    <button type="button" class="btn-close btn-close-white" data-bs-dismiss="modal" aria-label="Cerrar"></button>
                </div>
                <div class="modal-body">
                    <asp:HiddenField ID="hfIdProveedorEliminar" runat="server" />
                    <p>¿Estás seguro de que deseas eliminar al proveedor <strong id="nombreProveedorAEliminar"></strong>?</p>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cancelar</button>
                    <asp:Button ID="btnEliminarProveedorConfirmado" runat="server" CssClass="btn btn-danger"
                        Text="Eliminar" OnClick="btnEliminarProveedorConfirmado_Click" />
                </div>
            </div>
        </div>
    </div>

    <%-- HTML DEL TOAST --%>
    <div aria-live="polite" aria-atomic="true" style="position: fixed; top: 20px; right: 20px; z-index: 1060;">
        <div class="toast hide" role="alert" id="liveToast">
            <div class="toast-header">
                <strong class="me-auto" id="toastHeader">Notificación</strong>
                <button type="button" class="btn-close" data-bs-dismiss="toast" aria-label="Close"></button>
            </div>
            <div class="toast-body" id="toastBody">
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
            var toastHeader = document.getElementById('toastHeader');

            toastBody.innerText = mensaje;

            if (tipo === 'danger') {
                toastHeader.classList.remove('text-success');
                toastHeader.classList.add('text-danger');
                toastHeader.innerText = "Error";
            } else {
                toastHeader.classList.remove('text-danger');
                toastHeader.classList.add('text-success');
                toastHeader.innerText = "Éxito";
            }

            var toast = new bootstrap.Toast(toastEl);
            toast.show();
        }
    </script>

</asp:Content>