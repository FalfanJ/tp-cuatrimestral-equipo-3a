<%@ Page Title="Compras y Ventas" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ComprasVentas.aspx.cs" Inherits="Presentacion.WebForm3" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <style>
        body {
            background: linear-gradient(135deg, #2735F5 0%, #4D079C 100%) !important;
            background-attachment: fixed;
            background-size: cover;
            min-height: 100vh;
        }

        h2, h4, .text-white-title {
            color: white !important;
            text-shadow: 0 2px 4px rgba(0,0,0,0.3);
        }

        .card-custom {
            border: none;
            border-radius: 15px;
            box-shadow: 0 8px 20px rgba(0,0,0,0.25);
            background-color: #ffffff;
            overflow: hidden;
            transition: transform 0.3s ease;
        }
        .card-menu:hover { transform: translateY(-5px); }

        .header-gradient-bg {
            background: linear-gradient(to right, #2735F5, #4D079C);
            color: white;
            padding: 15px 20px;
        }

        .btn-action-green {
            background-color: #8BD100; border: none; color: white; font-weight: 600;
            transition: transform 0.2s;
        }
        .btn-action-green:hover { background-color: #75b300; transform: scale(1.05); }

        .btn-general-blue {
            background-color: #8FADFA; border: none; color: white; font-weight: 600;
            transition: transform 0.2s;
        }
        .btn-general-blue:hover { background-color: #6c94f7; transform: scale(1.05); }

        .form-label { font-weight: 600; color: #4D079C; }
    </style>

    <div class="container pb-5">
        <h2 class="text-center mb-5 mt-4 fw-bold"><i class="fas fa-exchange-alt me-2"></i>Movimientos Comerciales</h2>

        <asp:UpdatePanel ID="upMain" runat="server" UpdateMode="Conditional">
            <ContentTemplate>

                <!-- Tarjetas Compra y Venta -->
                <div class="row justify-content-center">

                    <div class="col-md-5 mb-4">
                        <div class="card card-custom card-menu h-100">
                            <div class="card-header header-gradient-bg text-center">
                                <h4 class="mb-0 fw-bold"><i class="fas fa-shopping-bag me-2"></i>Compras</h4>
                            </div>
                            <div class="card-body text-center p-5">
                                <i class="fas fa-truck-loading fa-4x mb-4" style="color: #8FADFA;"></i>
                                <p class="card-text text-muted mb-4">Reponer stock de productos faltantes mediante un proveedor.</p>
                                <asp:Button ID="btnNuevaCompra" runat="server" 
                                    CssClass="btn btn-general-blue btn-lg w-100 shadow py-3" 
                                    Text="Registrar Compra" OnClick="btnNuevaCompra_Click" />
                            </div>
                        </div>
                    </div>

                    <div class="col-md-5 mb-4">
                        <div class="card card-custom card-menu h-100">
                            <div class="card-header header-gradient-bg text-center">
                                <h4 class="mb-0 fw-bold"><i class="fas fa-cash-register me-2"></i>Ventas</h4>
                            </div>
                            <div class="card-body text-center p-5">
                                <i class="fas fa-shopping-cart fa-4x mb-4" style="color: #8BD100;"></i>
                                <p class="card-text text-muted mb-4">Iniciar nueva venta a cliente y generar comprobante.</p>
                                <asp:Button ID="btnNuevaVenta" runat="server" 
                                    CssClass="btn btn-action-green btn-lg w-100 shadow py-3" 
                                    Text="Registrar Venta" OnClick="btnNuevaVenta_Click" />
                            </div>
                        </div>
                    </div>
                </div>

                <!-- Historial de Compras -->
                <div class="row justify-content-center mt-5">
                    <div class="col-12">
                        <div class="card card-custom">
                            <div class="card-header header-gradient-bg">
                                <h4 class="mb-0 fw-bold"><i class="fas fa-list me-2"></i>Compras Realizadas</h4>
                            </div>
                            <div class="card-body">
                                <div class="table-responsive">
                                    <asp:GridView ID="gvCompras" runat="server"
                                        CssClass="table table-hover table-striped align-middle"
                                        AutoGenerateColumns="False"
                                        DataKeyNames="IdCompra"
                                        EmptyDataText="No se han registrado compras."
                                        GridLines="None">
                                        <Columns>
                                            <asp:BoundField DataField="IdCompra" HeaderText="ID" />
                                            <asp:BoundField DataField="ProveedorNombre" HeaderText="Proveedor" />
                                            <asp:BoundField DataField="Fecha" HeaderText="Fecha" DataFormatString="{0:dd/MM/yyyy}" />
                                            <asp:BoundField DataField="TotalProductos" HeaderText="Total Compra" />
                                                <asp:TemplateField HeaderText="Detalle">
        <ItemTemplate>
            <%# FormatearDetalle(Eval("Detalle")) %>
        </ItemTemplate>
    </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

            </ContentTemplate>
        </asp:UpdatePanel>
    </div>

    <!-- Modal de Compra -->
    <div class="modal fade" id="modalCompra" tabindex="-1" aria-hidden="true">
        <div class="modal-dialog modal-xl modal-dialog-centered modal-dialog-scrollable">
            <div class="modal-content card-custom">
                <asp:UpdatePanel ID="upModalCompra" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <div class="modal-header header-gradient-bg">
                            <h5 class="modal-title fw-bold"><i class="fas fa-boxes me-2"></i>Reposición de Stock</h5>
                            <button type="button" class="btn-close btn-close-white" data-bs-dismiss="modal"></button>
                        </div>
                        <div class="modal-body bg-light p-4">

                            <div class="row mb-3">
                                <div class="col-md-6">
                                    <label class="form-label">Seleccione Proveedor:</label>
                                    <asp:DropDownList ID="ddlProveedores" runat="server" CssClass="form-select shadow-sm"></asp:DropDownList>
                                </div>
                                <div class="col-md-6 d-flex align-items-end">
                                    <div class="alert alert-primary py-2 px-3 m-0 w-100 shadow-sm text-center">
                                        <i class="fas fa-info-circle me-2"></i>Mostrando productos con stock bajo o nulo.
                                    </div>
                                </div>
                            </div>

                            <div class="card shadow-sm border-0">
                                <div class="card-body p-0">
                                    <div class="table-responsive">
                                        <asp:GridView ID="gvProductosFaltantes" runat="server"
                                            CssClass="table table-hover table-striped align-middle mb-0"
                                            AutoGenerateColumns="False"
                                            DataKeyNames="IdProducto"
                                            EmptyDataText="No hay productos con bajo stock."
                                            GridLines="None">
                                            <HeaderStyle BackColor="White" ForeColor="#1A0047" BorderStyle="None" Height="50px" Font-Bold="True" />
                                            <Columns>
                                                <asp:TemplateField ItemStyle-Width="50px" ItemStyle-CssClass="text-center">
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chkSeleccionar" runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="Nombre" HeaderText="Producto" />
                                                <asp:BoundField DataField="Marca.Nombre" HeaderText="Marca" />
                                                <asp:TemplateField HeaderText="Stock Actual" ItemStyle-CssClass="text-center">
                                                    <ItemTemplate>
                                                        <span class="badge bg-danger"><%# Eval("Stock") %></span>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="StockMinimo" HeaderText="Stock Mín." ItemStyle-CssClass="text-center text-muted" />
                                                <asp:TemplateField HeaderText="Cant. a Comprar">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtCantidadCompra" runat="server" CssClass="form-control form-control-sm text-center" TextMode="Number" min="1" placeholder="0"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>

                        </div>

                        <div class="modal-footer bg-light">
                            <button type="button" class="btn btn-general-blue" data-bs-dismiss="modal">Cancelar</button>
                            <asp:Button ID="btnConfirmarCompra" runat="server" Text="Confirmar Compra" CssClass="btn btn-action-green px-4" OnClick="btnConfirmarCompra_Click" />
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>

    <!-- Toast Notificaciones -->
    <div aria-live="polite" aria-atomic="true" style="position: fixed; top: 80px; right: 20px; z-index: 1060;">
        <div class="toast hide shadow-lg align-items-center border-0" role="alert" id="liveToast">
            <div class="d-flex">
                <div class="toast-body fw-bold" id="toastBody"></div>
                <button type="button" class="btn-close btn-close-white me-2 m-auto" data-bs-dismiss="toast"></button>
            </div>
        </div>
    </div>

    <script>
        function abrirModalCompra() {
            var modal = new bootstrap.Modal(document.getElementById('modalCompra'));
            modal.show();
        }
        function cerrarModalCompra() {
            var myModalEl = document.getElementById('modalCompra');
            var modal = bootstrap.Modal.getInstance(myModalEl);
            if (modal) { modal.hide(); }
        }
        function mostrarToast(mensaje, tipo) {
            var toastEl = document.getElementById('liveToast');
            var toastBody = document.getElementById('toastBody');
            toastBody.innerText = mensaje;
            if (tipo === 'danger') { toastEl.style.backgroundColor = "#D10000"; toastEl.classList.add('text-white'); }
            else { toastEl.style.backgroundColor = "#8BD100"; toastEl.classList.add('text-white'); }
            var toast = new bootstrap.Toast(toastEl);
            toast.show();
        }
    </script>

</asp:Content>
