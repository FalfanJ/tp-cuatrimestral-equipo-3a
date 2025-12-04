<%@ Page Title="Compras" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Compras.aspx.cs" Inherits="Presentacion.Compras" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <style>
        body {
            background: linear-gradient(135deg, #2735F5 0%, #4D079C 100%) !important;
            background-attachment: fixed;
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
            background-color: #fff;
            overflow: hidden;
        }

        .header-gradient-bg {
            background: linear-gradient(to right, #2735F5, #4D079C);
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
                transform: scale(1.05);
            }

        .form-label {
            font-weight: 600;
            color: #4D079C;
        }
    </style>

    <div class="container pb-5">

        <div class="d-flex justify-content-between align-items-center">
            <h2 class="fw-bold"><i class="fas fa-cart-plus me-2"></i>Compras</h2>
            <asp:Button ID="btnNuevaCompra" runat="server" CssClass="btn btn-action-green btn-lg shadow"
                Text="Registrar nueva compra" OnClick="btnNuevaCompra_Click" />
        </div>

        <asp:Label ID="lblMensaje" runat="server" CssClass="text-danger fw-bold" Visible="False"></asp:Label>

        <!-- FILTROS -->
        <div class="card card-custom my-4">
            <div class="card-header header-gradient-bg">
                <h5 class="mb-0"><i class="fas fa-filter me-2"></i>Filtros de Búsqueda</h5>
            </div>

            <div class="card-body">

                <!-- Fila de filtros -->
                <div class="row g-3">

                    <div class="col-md-4">
                        <label class="form-label">Proveedor:</label>
                        <asp:DropDownList ID="ddlProveedorFiltro" runat="server" CssClass="form-select"
                            AutoPostBack="true" OnSelectedIndexChanged="Filtro_SelectedIndexChanged">
                        </asp:DropDownList>
                    </div>

                    <div class="col-md-4">
                        <label class="form-label">Fecha Desde:</label>
                        <asp:TextBox ID="txtFechaDesde" runat="server" CssClass="form-control"
                            TextMode="Date" AutoPostBack="true" OnTextChanged="Filtro_SelectedIndexChanged"></asp:TextBox>
                    </div>

                    <div class="col-md-4">
                        <label class="form-label">Fecha Hasta:</label>
                        <asp:TextBox ID="txtFechaHasta" runat="server" CssClass="form-control"
                            TextMode="Date" AutoPostBack="true" OnTextChanged="Filtro_SelectedIndexChanged"></asp:TextBox>
                    </div>

                    <div class="col-md-4">
                        <label class="form-label">Usuario Responsable:</label>
                        <asp:DropDownList ID="ddlUsuarioFiltro" runat="server" CssClass="form-select"
                            AutoPostBack="true" OnSelectedIndexChanged="Filtro_SelectedIndexChanged">
                        </asp:DropDownList>
                    </div>

                </div>

                <!-- Fila exclusiva para el botón → CENTRADO -->
                <div class="row mt-4">
                    <div class="col-12 d-flex justify-content-center">
                        <asp:Button ID="btnLimpiarFiltros" runat="server" Text="Limpiar Filtros"
                            CssClass="btn btn-general-blue px-5" OnClick="btnLimpiarFiltros_Click" />
                    </div>
                </div>

            </div>
        </div>


        <!-- RESUMEN -->
        <div class="card card-custom my-4 p-4">
            <div class="d-flex align-items-center mb-3">
                <i class="fas fa-chart-line me-2 text-primary" style="font-size: 22px;"></i>
                <h4 class="fw-bold mb-0">Resumen del último mes</h4>
            </div>

            <div class="p-3 rounded" style="background: #f7f9ff; border-left: 5px solid #4D079C;">
                <asp:Label ID="lblTopUsuario" runat="server" CssClass="fw-bold d-block mb-2"
                    Style="font-size: 18px; color: #4D079C;"></asp:Label>

                <asp:Label ID="lblTotalMes" runat="server" CssClass="fw-bold d-block"
                    Style="font-size: 18px; color: #2735F5;"></asp:Label>
            </div>
        </div>

        <!-- GRID DE COMPRAS -->
        <div class="card card-custom mt-4">
            <div class="card-header header-gradient-bg">
                <h4 class="mb-0 fw-bold"><i class="fas fa-list me-2"></i>Compras Realizadas</h4>
            </div>

            <div class="card-body">
                <div class="table-responsive">

                    <asp:GridView ID="gvCompras" runat="server"
                        CssClass="table table-hover table-striped align-middle"
                        AutoGenerateColumns="False" GridLines="None"
                        DataKeyNames="IdCompra"
                        EmptyDataText="No se han registrado compras."
                        OnRowCommand="gvCompras_RowCommand">

                        <Columns>
                            <asp:TemplateField HeaderText="Usuario Responsable">
                                <ItemTemplate>
                                    <%# Eval("Usuario.Email") %>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:BoundField DataField="Fecha" HeaderText="Fecha" DataFormatString="{0:dd/MM/yyyy}" />
                            <asp:BoundField DataField="ProveedorNombre" HeaderText="Proveedor" />
                            <asp:BoundField DataField="TotalProductos" HeaderText="Total Compra" DataFormatString="{0:C}" HtmlEncode="False" />

                            <asp:TemplateField HeaderText="Detalle">
                                <ItemTemplate>
                                    <%# FormatearDetalle(Eval("Detalle")) %>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Acciones">
                                <ItemTemplate>
                                    <button type="button" class="btn btn-danger btn-sm"
                                        data-bs-toggle="modal" data-bs-target="#modalConfirmar"
                                        onclick="setCompraAEliminar('<%# Eval("IdCompra") %>')">
                                        Eliminar
                                    </button>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>

                    </asp:GridView>

                    <!-- HiddenField donde guardo el ID -->
                    <asp:HiddenField ID="ocultoIdCompra" runat="server" />

                    <!-- MODAL CONFIRMACIÓN -->
                    <div class="modal fade" id="modalConfirmar" tabindex="-1">
                        <div class="modal-dialog modal-dialog-centered">
                            <div class="modal-content">

                                <div class="modal-header bg-danger text-white">
                                    <h5 class="modal-title">Confirmar Eliminación</h5>
                                    <button type="button" class="btn-close" data-bs-dismiss="modal"></button>
                                </div>

                                <div class="modal-body">
                                    ¿Seguro que deseas eliminar esta compra?
                                </div>

                                <div class="modal-footer">
                                    <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cancelar</button>

                                    <asp:Button ID="btnConfirmarEliminar" runat="server"
                                        CssClass="btn btn-danger" Text="Eliminar"
                                        OnClick="btnConfirmarEliminar_Click" />
                                </div>

                            </div>
                        </div>
                    </div>

                    <script>
                        function setCompraAEliminar(id) {
                            document.getElementById('<%= ocultoIdCompra.ClientID %>').value = id;
                        }
                    </script>

                </div>
            </div>
        </div>
    </div>

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
        function showToast(mensaje, tipo) {
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
