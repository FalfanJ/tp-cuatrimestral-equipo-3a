<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="GestionUsuarios.aspx.cs" Inherits="Presentacion.GestionUsuarios" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container mt-4">
        <!-- Encabezado -->
        <div class="d-flex justify-content-between align-items-center mb-3">
            <h2>Gestión de Usuarios</h2>
            <asp:Button ID="btnNuevoUsuario" runat="server" Text="Nuevo Usuario"
                CssClass="btn btn-success"
                OnClick="btnNuevoUsuario_Click" />
        </div>
        <!-- Filtros -->
<div class="row mb-3">
    <div class="col-md-4">
        <asp:TextBox ID="txtFiltroNombre" runat="server" CssClass="form-control" placeholder="Filtrar por nombre" />
    </div>
    <div class="col-md-4">
        <asp:TextBox ID="txtFiltroEmail" runat="server" CssClass="form-control" placeholder="Filtrar por email" />
    </div>
    <div class="col-md-4 d-flex gap-2">
        <asp:Button ID="btnFiltrar" runat="server" Text="Filtrar" CssClass="btn btn-primary" OnClick="btnFiltrar_Click" />
        <asp:Button ID="btnResetFiltros" runat="server" Text="Resetear Filtros" CssClass="btn btn-secondary" OnClick="btnResetFiltros_Click" />
    </div>
</div>


        <!-- Tabla de Usuarios -->
        <div class="table-responsive shadow-sm rounded mt-4">
            <asp:GridView ID="gvUsuarios"
                runat="server"
                CssClass="table table-striped table-hover align-middle"
                AutoGenerateColumns="False"
                DataKeyNames="IdUsuario"
                GridLines="None"
                OnRowCommand="gvUsuarios_RowCommand">

                <Columns>
                    <asp:BoundField DataField="IdUsuario" HeaderText="ID" />
                    <asp:BoundField DataField="NombreUsuario" HeaderText="Nombre de Usuario" />
                    <asp:BoundField DataField="TipoUsuario" HeaderText="Perfil" />
                    <asp:BoundField DataField="email" HeaderText="Email" />

                    <asp:TemplateField HeaderText="Acciones">
                        <ItemTemplate>
                            <asp:Button runat="server" CommandName="Editar"
                                CommandArgument='<%# Eval("IdUsuario") %>'
                                Text="Editar" CssClass="btn btn-warning btn-sm me-2" />

                            <button type="button" class="btn btn-danger btn-sm"
                                data-bs-toggle="modal"
                                data-bs-target="#modalConfirmarEliminar"
                                onclick="setUsuarioAEliminar('<%# Eval("IdUsuario") %>', '<%# Eval("NombreUsuario") %>')">
                                Eliminar
                            </button>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
    </div>

    <!--  Modal Crear / Editar Usuario -->
    <div class="modal fade" id="modalNuevoUsuario" tabindex="-1" aria-labelledby="modalNuevoUsuarioLabel" aria-hidden="true">
        <div class="modal-dialog modal-dialog-centered">
            <div class="modal-content rounded-3 shadow">
                <div class="modal-header bg-primary text-white">
                    <h5 class="modal-title" id="modalNuevoUsuarioLabel"><%: tituloModal %></h5>
                    <button type="button" class="btn-close btn-close-white" data-bs-dismiss="modal"></button>
                </div>

                <div class="modal-body">
                    <asp:HiddenField ID="hfIdUsuario" runat="server" />

                    <div class="mb-3">
                        <label for="txtNombreUsuario" class="form-label">Nombre de Usuario</label>
                        <asp:TextBox ID="txtNombreUsuario" runat="server" CssClass="form-control"
                            placeholder="Ingrese el nombre de usuario" />
                    </div>

                    <div class="mb-3">
                        <label for="txtEmail" class="form-label">Email</label>
                        <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control"
                            TextMode="Email" placeholder="ejemplo@correo.com" />
                        <asp:RegularExpressionValidator ID="valEmail" runat="server"
                            ControlToValidate="txtEmail"
                            ValidationExpression="^[^@\s]+@[^@\s]+\.[^@\s]+$"
                            ErrorMessage="Ingrese un email válido"
                            CssClass="text-danger small"
                            Display="Dynamic" />
                    </div>

                    <div class="mb-3">
                        <label for="ddlTipoUsuario" class="form-label">Perfil</label>
                        <asp:DropDownList ID="ddlTipoUsuario" runat="server" CssClass="form-select">
                            <asp:ListItem Text="Seleccione un perfil" Value="" />
                            <asp:ListItem Text="Administrador" Value="Administrador" />
                            <asp:ListItem Text="Vendedor" Value="Vendedor" />
                        </asp:DropDownList>
                    </div>

                    <div class="mb-3">
                        <label for="txtContrasenia" class="form-label">Contraseña</label>
                        <asp:TextBox ID="txtContrasenia" runat="server" CssClass="form-control"
                            TextMode="Password" placeholder="Ingrese la contraseña" />
                        <div class="form-check mt-2">
                            <input type="checkbox" class="form-check-input" id="chkMostrarContraseña" onclick="togglePassword()" />
                            <label class="form-check-label" for="chkMostrarContraseña">Mostrar contraseña</label>
                        </div>
                    </div>
                </div>

                <div class="modal-footer">
                    <asp:Button ID="btnGuardarUsuario" runat="server" Text="Guardar" CssClass="btn btn-primary"
                        OnClick="btnGuardarUsuario_Click" />
                    <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cancelar</button>
                </div>
            </div>
        </div>
    </div>

    <!-- Modal Confirmar Eliminación -->
    <div class="modal fade" id="modalConfirmarEliminar" tabindex="-1" aria-labelledby="modalConfirmarEliminarLabel" aria-hidden="true">
        <div class="modal-dialog modal-dialog-centered">
            <div class="modal-content rounded-3 shadow">
                <div class="modal-header bg-danger text-white">
                    <h5 class="modal-title" id="modalConfirmarEliminarLabel">Confirmar eliminación</h5>
                    <button type="button" class="btn-close btn-close-white" data-bs-dismiss="modal"></button>
                </div>
                <div class="modal-body">
                    ¿Estás seguro de que deseas eliminar el usuario <strong id="nombreUsuarioEliminar"></strong>?
                    <asp:HiddenField ID="hfIdUsuarioEliminar" runat="server" />
                </div>
                <div class="modal-footer">
                    <asp:Button ID="btnEliminarUsuarioConfirmado" runat="server" CssClass="btn btn-danger"
                        Text="Eliminar" OnClick="btnEliminarUsuarioConfirmado_Click" />
                    <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cancelar</button>
                </div>
            </div>
        </div>
    </div>

    <!-- JS scripts -->
    <script>
        function togglePassword() {
            const input = document.getElementById('<%= txtContrasenia.ClientID %>');
            input.type = input.type === "password" ? "text" : "password";
        }

        function setUsuarioAEliminar(id, nombre) {
            document.getElementById('<%= hfIdUsuarioEliminar.ClientID %>').value = id;
            document.getElementById("nombreUsuarioEliminar").innerText = nombre;
        }
    </script>
</asp:Content>
