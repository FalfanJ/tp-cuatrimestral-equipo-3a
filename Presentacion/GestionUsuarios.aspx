<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="GestionUsuarios.aspx.cs" Inherits="Presentacion.GestionUsuarios" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <style>
        body {
            background: linear-gradient(135deg, #2735F5 0%, #4D079C 100%) !important;
            background-attachment: fixed;
            background-size: cover;
            min-height: 100vh;
        }

        h2 {
            color: white !important;
            text-shadow: 0 2px 4px rgba(0,0,0,0.3);
            font-weight: bold;
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

        .btn-action-green {
            background-color: #8BD100;
            border: none;
            color: white;
            font-weight: 600;
            transition: transform 0.2s;
        }

            .btn-action-green:hover {
                background-color: #75b300;
                color: white;
                transform: scale(1.05);
            }

        .btn-action-red {
            background-color: #D10000;
            border: none;
            color: white;
            font-weight: 600;
            transition: transform 0.2s;
        }

            .btn-action-red:hover {
                background-color: #a30000;
                color: white;
                transform: scale(1.05);
            }

        .btn-general-blue {
            background-color: #8FADFA;
            border: none;
            color: white;
            font-weight: 600;
            transition: transform 0.2s;
        }

            .btn-general-blue:hover {
                background-color: #6c94f7;
                color: white;
                transform: scale(1.05);
            }

        .form-label {
            font-weight: 600;
            color: #4D079C;
        }
    </style>

    <div class="container pb-5">
        <div class="d-flex justify-content-between align-items-center mb-4 mt-4">
            <h2>Gestión de Usuarios</h2>
            <asp:Button ID="btnNuevoUsuario" runat="server" Text="Nuevo Usuario"
                CssClass="btn btn-action-green btn-lg shadow"
                OnClick="btnNuevoUsuario_Click" />
        </div>
        <div class="card card-custom mb-4">
            <div class="card-header header-gradient-bg">
                <h5 class="mb-0 fw-bold">Filtros de Búsqueda</h5>
            </div>
            <div class="card-body">
                <div class="row">
                    <div class="col-md-4">
                        <asp:TextBox ID="txtFiltroNombre" runat="server" CssClass="form-control" placeholder="Filtrar por nombre" />
                    </div>
                    <div class="col-md-4">
                        <asp:TextBox ID="txtFiltroEmail" runat="server" CssClass="form-control" placeholder="Filtrar por email" />
                    </div>
                    <div class="col-md-4 d-flex gap-2">
                        <asp:Button ID="btnFiltrar" runat="server" Text="Filtrar" CssClass="btn btn-action-green w-50" OnClick="btnFiltrar_Click" />
                        <asp:Button ID="btnResetFiltros" runat="server" Text="Resetear" CssClass="btn btn-general-blue w-50" OnClick="btnResetFiltros_Click" />
                    </div>
                </div>
            </div>
        </div>


        <div class="card card-custom shadow">
            <div class="card-body p-0">
                <div class="table-responsive">
                    <asp:GridView ID="gvUsuarios"
                        runat="server"
                        CssClass="table table-hover table-striped align-middle mb-0"
                        AutoGenerateColumns="False"
                        DataKeyNames="IdUsuario"
                        GridLines="None"
                        OnRowCommand="gvUsuarios_RowCommand">

                        <HeaderStyle BackColor="White" ForeColor="#1A0047" BorderStyle="None" Height="50px" Font-Bold="True" />

                        <Columns>
                            <asp:BoundField DataField="IdUsuario" HeaderText="ID" />
                            <asp:BoundField DataField="NombreUsuario" HeaderText="Nombre de Usuario" />
                            <asp:BoundField DataField="TipoUsuario" HeaderText="Perfil" />
                            <asp:BoundField DataField="email" HeaderText="Email" />

                            <asp:TemplateField HeaderText="Acciones" ItemStyle-CssClass="text-center">
                                <ItemTemplate>
                                    <asp:Button runat="server" CommandName="Editar"
                                        CommandArgument='<%# Eval("IdUsuario") %>'
                                        Text="Editar" CssClass="btn btn-action-green btn-sm me-2" />

                                    <button type="button" class="btn btn-action-red btn-sm"
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
        </div>
    </div>

    <div class="modal fade" id="modalNuevoUsuario" tabindex="-1" aria-labelledby="modalNuevoUsuarioLabel" aria-hidden="true">
        <div class="modal-dialog modal-dialog-centered">
            <div class="modal-content card-custom">
                <div class="modal-header header-gradient-bg">
                    <h5 class="modal-title fw-bold" id="modalNuevoUsuarioLabel"><%: tituloModal %></h5>
                    <button type="button" class="btn-close btn-close-white" data-bs-dismiss="modal"></button>
                </div>

                <div class="modal-body p-4">
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

                <div class="modal-footer bg-light">
                    <button type="button" class="btn btn-general-blue" data-bs-dismiss="modal">Cancelar</button>
                    <asp:Button ID="btnGuardarUsuario" runat="server" Text="Guardar" CssClass="btn btn-action-green px-4"
                        OnClick="btnGuardarUsuario_Click" />
                </div>
            </div>
        </div>
    </div>

    <div class="modal fade" id="modalConfirmarEliminar" tabindex="-1" aria-labelledby="modalConfirmarEliminarLabel" aria-hidden="true">
        <div class="modal-dialog modal-dialog-centered">
            <div class="modal-content card-custom">
                <div class="modal-header header-red-bg">
                    <h5 class="modal-title fw-bold" id="modalConfirmarEliminarLabel">Confirmar eliminación</h5>
                    <button type="button" class="btn-close btn-close-white" data-bs-dismiss="modal"></button>
                </div>
                <div class="modal-body text-center py-4">
                    <i class="fas fa-exclamation-circle text-danger fa-3x mb-3"></i>
                    <p class="fs-5">¿Estás seguro de que deseas eliminar el usuario <strong id="nombreUsuarioEliminar"></strong>?</p>
                    <asp:HiddenField ID="hfIdUsuarioEliminar" runat="server" />
                </div>
                <div class="modal-footer bg-light">
                    <button type="button" class="btn btn-general-blue" data-bs-dismiss="modal">Cancelar</button>
                    <asp:Button ID="btnEliminarUsuarioConfirmado" runat="server" CssClass="btn btn-action-red"
                        Text="Eliminar" OnClick="btnEliminarUsuarioConfirmado_Click" />
                </div>
            </div>
        </div>
    </div>

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

    <!-- TOAST NOTIFICACIONES -->
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
