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

        .card-menu:hover {
            transform: translateY(-5px);
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

        <asp:UpdatePanel ID="upMain" runat="server" UpdateMode="Conditional">
            <ContentTemplate>

                <div class="d-flex justify-content-between align-items-center">
                    <h2 class="fw-bold"><i class="fas fa-cart-plus me-2"></i>Ventas</h2>
                    <asp:Button ID="Button1" runat="server"
                        CssClass="btn btn-action-green btn-lg shadow"
                        Text="Registrar Venta" OnClick="btnNuevaVenta_Click" />
                </div>
                <!-- Tabla de Ventas -->
                <div class="row mt-5">
                    <div class="col-12">
                        <div class="card card-custom shadow">
                            <div class="card-header header-gradient-bg">
                                <h4 class="mb-0 fw-bold"><i class="fas fa-shopping-cart me-2"></i>Ventas Registradas</h4>
                            </div>
                            <div class="card-body p-0">
                                <div class="table-responsive">
                                    <asp:GridView ID="gvVentas" runat="server"
                                        AutoGenerateColumns="False"
                                        CssClass="table table-hover table-striped align-middle mb-0"
                                        EmptyDataText="No hay ventas registradas"
                                        GridLines="None">

                                        <HeaderStyle BackColor="White" ForeColor="#1A0047" BorderStyle="None" Height="50px" Font-Bold="True" />

                                        <Columns>
                                            <asp:BoundField DataField="IdVenta" HeaderText="ID Venta" ItemStyle-CssClass="fw-bold" />
                                            <asp:BoundField DataField="Fecha" HeaderText="Fecha" DataFormatString="{0:dd/MM/yyyy}" />
                                            <asp:BoundField DataField="UsuarioEmail" HeaderText="Vendedor" />
                                            <asp:TemplateField HeaderText="Factura">
                                                <ItemTemplate>
                                                    <asp:HyperLink ID="hlFactura" runat="server" Text='<%# Eval("NFactura") %>'
                                                        NavigateUrl='<%# "ReporteFactura.aspx?nfactura=" + Eval("NFactura") %>' Target="_blank"
                                                        CssClass="text-decoration-none fw-bold">
                                                    </asp:HyperLink>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:BoundField DataField="Total" HeaderText="Total" DataFormatString="{0:C}" ItemStyle-CssClass="text-primary fw-bold" />
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <asp:BoundField DataField="Total" HeaderText="Total" DataFormatString="{0:C}" ItemStyle-CssClass="text-primary fw-bold" />
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
