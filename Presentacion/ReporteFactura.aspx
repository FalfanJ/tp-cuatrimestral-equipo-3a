<%@ Page Title="Reporte de Factura" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Factura.aspx.cs" Inherits="Presentacion.Factura" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <style>
        /* Fondo con degradado como el resto del sitio */
        body {
            background: linear-gradient(135deg, #2735F5 0%, #4D079C 100%) !important;
            min-height: 100vh;
        }

        /* Hoja de la factura */
        .factura-sheet {
            background-color: white;
            max-width: 850px;
            margin: 0 auto;
            padding: 40px;
            box-shadow: 0 10px 30px rgba(0,0,0,0.3);
            border-radius: 5px;
        }

        .factura-header {
            border-bottom: 2px solid #2735F5;
            margin-bottom: 20px;
            padding-bottom: 20px;
        }

        .company-name {
            color: #2735F5;
            font-weight: bold;
            font-size: 1.8rem;
        }

        .client-box {
            background-color: #f8f9fa;
            border: 1px solid #dee2e6;
            border-radius: 5px;
            padding: 15px;
            margin-bottom: 20px;
        }

        .total-box {
            background-color: #2735F5;
            color: white;
            padding: 10px 20px;
            font-size: 1.5rem;
            font-weight: bold;
            border-radius: 5px;
            text-align: right;
        }

        /* Modo Impresión */
        @media print {
            body { background: white !important; }
            nav, footer, .no-print { display: none !important; }
            .factura-sheet { box-shadow: none; margin: 0; width: 100%; max-width: 100%; }
        }
        
        /* Botones personalizados */
        .btn-action-green { background-color: #8BD100; color: white; border: none; }
        .btn-action-green:hover { background-color: #75b300; color: white; }
        .btn-general-blue { background-color: #8FADFA; color: white; border: none; }
        .btn-general-blue:hover { background-color: #6c94f7; color: white; }
    </style>

    <div class="container py-5">

        <%-- BUSCADOR --%>
        <div class="row justify-content-center mb-4 no-print">
            <div class="col-md-6">
                <div class="card shadow border-0">
                    <div class="card-body p-4">
                        <h5 class="mb-3 fw-bold" style="color: #4D079C;">Buscar Venta</h5>
                        <div class="input-group">
                            <asp:TextBox ID="txtNumVenta" runat="server" CssClass="form-control" placeholder="ID Venta" TextMode="Number"></asp:TextBox>
                            <asp:Button ID="btnBuscarVenta" runat="server" Text="Generar" CssClass="btn btn-primary" OnClick="btnBuscarVenta_Click" />
                        </div>
                        <asp:Label ID="lblError" runat="server" CssClass="text-danger mt-2 d-block fw-bold" Visible="false"></asp:Label>
                    </div>
                </div>
            </div>
        </div>

        <%-- HOJA FACTURA --%>
        <asp:Panel ID="pnlFactura" runat="server" Visible="false">
            
            <div class="factura-sheet">
                
                <%-- Cabecera --%>
                <div class="row factura-header align-items-center">
                    <div class="col-7">
                        <div class="company-name"><i class="fas fa-tools"></i> Ferretería Dos Clavos</div>
                        <small>Av. Siempre Viva 123</small><br />
                        <small>IVA Responsable Inscripto</small>
                    </div>
                    <div class="col-5 text-end">
                        <h2 class="fw-bold text-dark">FACTURA B</h2>
                        <h5 class="text-muted">N°: <asp:Label ID="lblNumeroFactura" runat="server"></asp:Label></h5>
                        <p class="mb-0"><strong>Fecha:</strong> <asp:Label ID="lblFecha" runat="server"></asp:Label></p>
                        <p class="mb-0"><strong>Vendedor ID:</strong> <asp:Label ID="lblVendedor" runat="server"></asp:Label></p>
                    </div>
                </div>

                <%-- Datos Cliente --%>
                <div class="row mb-2">
                    <div class="col-12">
                        <h5 class="fw-bold" style="color: #4D079C;">Datos del Cliente</h5>
                        <div class="client-box">
                            <div class="row">
                                <div class="col-md-6">
                                    <strong>Nombre:</strong> <asp:Label ID="lblNombreCliente" runat="server"></asp:Label><br />
                                    <strong>Identificación:</strong> <asp:Label ID="lblDniCuit" runat="server"></asp:Label>
                                </div>
                                <div class="col-md-6">
                                    <strong>Dirección:</strong> <asp:Label ID="lblDireccionCliente" runat="server"></asp:Label><br />
                                    <strong>Email:</strong> <asp:Label ID="lblEmailCliente" runat="server"></asp:Label>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

                <%-- Tabla Detalle --%>
                <div class="table-responsive mb-4">
                    <asp:GridView ID="gvDetallesFactura" runat="server" CssClass="table table-striped table-bordered align-middle" AutoGenerateColumns="False" GridLines="None">
                        <HeaderStyle CssClass="table-dark text-center" BackColor="#1A0047" />
                        <Columns>
                            <asp:BoundField DataField="Producto.Nombre" HeaderText="Producto" ItemStyle-Width="50%" />
                            <asp:BoundField DataField="Cantidad" HeaderText="Cant." ItemStyle-CssClass="text-center" />
                            <asp:BoundField DataField="PrecioUnitario" HeaderText="Precio Unit." DataFormatString="{0:C}" ItemStyle-CssClass="text-end" />
                            <asp:BoundField DataField="PrecioParcial" HeaderText="Subtotal" DataFormatString="{0:C}" ItemStyle-CssClass="text-end fw-bold" />
                        </Columns>
                    </asp:GridView>
                </div>

                <%-- Total --%>
                <div class="row justify-content-end">
                    <div class="col-md-5">
                        <div class="total-section d-flex justify-content-between">
                            <span>TOTAL:</span>
                            <asp:Label ID="lblTotalPagar" runat="server"></asp:Label>
                        </div>
                    </div>
                </div>

                <div class="text-center mt-5 text-muted">
                    <small>Gracias por su compra.</small>
                </div>

            </div>

            <%-- Botones Finales --%>
            <div class="text-center mt-4 mb-5 no-print">
                <asp:Button ID="btnImprimir" runat="server" Text="🖨 Imprimir" CssClass="btn btn-action-green btn-lg me-2 shadow" OnClick="btnImprimir_Click" />
                <a href="Default.aspx" class="btn btn-general-blue btn-lg shadow">Volver al Inicio</a>
            </div>

        </asp:Panel>

    </div>

</asp:Content>