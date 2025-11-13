<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="ReporteFactura.aspx.cs"
    Inherits="Presentacion.Factura" %>




<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <%-- Script y Estilos para Impresión (Sin cambios) --%>
    <style>
        @media print {
            body * {
                visibility: hidden;
            }
            #areaImprimible, #areaImprimible * {
                visibility: visible;
            }
            #areaImprimible {
                position: absolute;
                left: 0;
                top: 0;
                width: 100%;
                margin: 0;
                padding: 0;
            }
            .no-print {
                display: none;
            }
        }
    </style>
    
    <div class="container mt-4">
        <asp:UpdatePanel ID="UpdatePanelFactura" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                
                <%-- SECCIÓN DE BÚSQUEDA (Sin cambios) --%>
                <div class="card shadow-sm mb-4 no-print">
                    <div class="card-header bg-light">
                        <h4 class="mb-0">Generar Factura</h4>
                    </div>
                    <div class="card-body">
                        <div class="row g-3 align-items-end">
                            <div class="col-md-4">
                                <label for="txtNumVenta" class="form-label">Número de Venta:</label>
                                <asp:TextBox ID="txtNumVenta" runat="server" CssClass="form-control" placeholder="Ingrese el ID de la Venta"></asp:TextBox>
                            </div>
                            <div class="col-md-4">
                                <asp:Button ID="btnBuscarVenta" runat="server" Text="Buscar Venta" CssClass="btn btn-primary" OnClick="btnBuscarVenta_Click" />
                            </div>
                        </div>
                  <asp:Label ID="lblError" runat="server" CssClass="text-danger mt-2" EnableViewState="false"></asp:Label>
                    </div>
                </div>

                <%-- ÁREA DE IMPRESIÓN DE FACTURA --%>
                <div id="areaImprimible">
                    <asp:Panel ID="pnlFactura" runat="server" Visible="false">
                        <div class="card shadow-sm">
                            <div class="card-body p-4">

                                <%-- Cabecera de Factura y Botón Imprimir (Sin cambios) --%>
                                <div class="row mb-4">
                                    <div class="col-md-6">
                                        <h2>Factura</h2>
                                    </div>
                                    <div class="col-md-6 text-md-end no-print">
                                        <asp:Button ID="btnImprimir" runat="server" Text="🖨️ Imprimir" CssClass="btn btn-info" OnClientClick="window.print(); return false;" />
                                    </div>
                                </div>

                                <%-- Datos del Cliente y Factura --%>
                                <div class="row border-bottom pb-3 mb-3">
                                    <div class="col-md-6">
                                        <h5>Cliente:</h5>
                                        <asp:Literal ID="litNombreCliente" runat="server"></asp:Literal><br />
                                        <asp:Literal ID="litCuitCliente" runat="server"></asp:Literal><br />
                                        <asp:Literal ID="litDireccionCliente" runat="server"></asp:Literal>
                                    </div>
                                    <div class="col-md-6 text-md-end">
                                        <%-- CAMBIO AQUÍ: Usamos NFactura --%>
                                        <h5>Nro. Factura: <asp:Literal ID="litNumeroFactura" runat="server"></asp:Literal></h5>
                                        <strong>Fecha:</strong> <asp:Literal ID="litFechaFactura" runat="server"></asp:Literal>
                                    </div>
                                </div>

                                <%-- Detalles de la Venta (GridView) --%>
                                <div class="table-responsive">
                                    <asp:GridView ID="gvDetallesVenta"
                                        runat="server"
                                        CssClass="table align-middle"
                                        AutoGenerateColumns="False"
                                        GridLines="Vertical"
                                        ShowHeaderWhenEmpty="true"
                                        EmptyDataText="No hay productos en esta venta.">
                                        
                                        <HeaderStyle CssClass="table-dark" />
                                        <Columns>
                                            <asp:BoundField DataField="Producto.Nombre" HeaderText="Producto" />
                                            <asp:BoundField DataField="Cantidad" HeaderText="Cantidad" ItemStyle-CssClass="text-center" />
                                            
                                            <%-- CAMBIO AQUÍ: Añadido formato de moneda a PrecioUnitario --%>
                                            <asp:BoundField DataField="PrecioUnitario" HeaderText="Precio Unit." DataFormatString="{0:C}" ItemStyle-CssClass="text-end" />
                                            
                                            <%-- CAMBIO AQUÍ: Usamos PrecioParcial en lugar de calcularlo --%>
                                            <asp:BoundField DataField="PrecioParcial" HeaderText="Subtotal" DataFormatString="{0:C}" ItemStyle-CssClass="text-end" />
                                        </Columns>
                                        <FooterStyle CssClass="fw-bold fs-5 text-end" />
                                    </asp:GridView>
                                </div>

                                <%-- Total de la Factura (Sin cambios) --%>
                                <div class="row justify-content-end mt-3">
                                    <div class="col-md-4">
                                        <div class="card bg-light">
                                            <div class="card-body p-3">
                                                <h4 class="mb-0 text-end">
                                                    Total: <asp:Literal ID="litTotalFactura" runat="server"></asp:Literal>
                                                </h4>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                            </div>
                        </div>
                    </asp:Panel>
                </div>

            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btnBuscarVenta" EventName="Click" />
            </Triggers>
        </asp:UpdatePanel>
    </div>
</asp:Content>