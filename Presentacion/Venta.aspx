<%@ Page Title="Ventas" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Venta.aspx.cs" Inherits="Presentacion.Venta" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .card-custom {
            border: none;
            border-radius: 15px;
            box-shadow: 0 8px 20px rgba(0,0,0,0.25);
            background-color: #ffffff;
            overflow: hidden;
        }

        body {
            background: linear-gradient(135deg, #2735F5 0%, #4D079C 100%) !important;
            background-attachment: fixed;
            background-size: cover;
            min-height: 100vh;
        }

        h2, .text-white-title {
            color: white !important;
            text-shadow: 0 2px 4px rgba(0,0,0,0.3);
        }
    </style>
    <script type='text/javascript'>
        function closeModal() {
            var myModalEl = document.getElementById('modalProductos');
            var modal = bootstrap.Modal.getInstance(myModalEl);
            if (!modal) {
                modal = new bootstrap.Modal(myModalEl);
            }
            modal.hide();
        }
        function openModalFin() {
            var myModalEl = document.getElementById('modalFinalizar');
            var modal = bootstrap.Modal.getInstance(myModalEl);
            if (!modal) {
                modal = new bootstrap.Modal(myModalEl);
            }
            modal.show();
        }
        function openModalFinFin() {
            var myModalEl = document.getElementById('modalFIN');
            var modal = bootstrap.Modal.getInstance(myModalEl);
            if (!modal) {
                modal = new bootstrap.Modal(myModalEl);
            }
            modal.show();
        }
        function openModalError() {
            var myModalEl = document.getElementById('modalError');
            var modal = bootstrap.Modal.getInstance(myModalEl);
            if (!modal) {
                modal = new bootstrap.Modal(myModalEl);
            }
            modal.show();
        }
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">


    <style>
        body {
            background: linear-gradient(135deg, #2735F5 0%, #4D079C 100%) !important;
            background-attachment: fixed;
            background-size: cover;
            min-height: 100vh;
        }

        h2, h1, .text-white-title {
            color: white !important;
            text-shadow: 0 2px 4px rgba(0,0,0,0.3);
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

        .form-label, label {
            font-weight: 600;
            color: #4D079C;
        }

        .lbl-precio {
            font-size: 1.1rem;
            font-weight: bold;
            color: #2735F5;
        }
    </style>

    <div class="container pb-5">

        <div class="row mb-4 mt-4 align-items-center">
            <div class="col-md-6">
                <h2 class="fw-bold"><i class="fas fa-shopping-cart me-2"></i>Gestión Ventas</h2>
            </div>
            <div class="col-md-6 text-md-end">
                <asp:Button Text="Cancelar Venta" runat="server" CssClass="btn btn-action-red btn-lg shadow me-2" ID="btnCancelarVenta" OnClick="btnCancelarVenta_Click" />
                <button type="button" class="btn btn-action-green btn-lg shadow" data-bs-toggle="modal" data-bs-target="#modalProductos">
                    <i class="fas fa-plus me-1"></i>Agregar Productos
                </button>
            </div>
        </div>

        <%-- GRILLA DE CARRITO DE COMPRAS --%>
        <asp:UpdatePanel runat="server" ID="upDetalleGrid" UpdateMode="Conditional">
            <ContentTemplate>

                <div class="card card-custom shadow mb-4">
                    <div class="card-body p-0">
                        <div class="table-responsive">
                            <asp:GridView
                                runat="server"
                                ID="gvDetalle"
                                CssClass="table table-hover table-striped align-middle mb-0"
                                AutoGenerateColumns="false"
                                GridLines="None"
                                OnRowCommand="gvDetalle_RowCommand"
                                EmptyDataText="No se han agregado productos."
                                DataKeyNames="ID">

                                <HeaderStyle BackColor="White" ForeColor="#1A0047" BorderStyle="None" Height="50px" Font-Bold="True" />

                                <Columns>
                                    <asp:BoundField DataField="Producto.Nombre" HeaderText="Nombre" />
                                    <asp:BoundField DataField="Producto.NSerie" HeaderText="N° Serie" />
                                    <asp:BoundField DataField="Cantidad" HeaderText="Cant." ItemStyle-CssClass="fw-bold" />
                                    <asp:BoundField DataField="PrecioUnitario" HeaderText="$ Unitario" DataFormatString="{0:C}" />
                                    <asp:BoundField DataField="PorcentajeGanancia" HeaderText="% Gan." />
                                    <asp:BoundField DataField="PrecioParcial" HeaderText="$ Subtotal" DataFormatString="{0:C}" ItemStyle-CssClass="fw-bold text-primary" />

                                    <asp:TemplateField HeaderText="Acción" ItemStyle-CssClass="text-center">
                                        <ItemTemplate>
                                            <asp:LinkButton
                                                runat="server"
                                                CommandName="Eliminar"
                                                CommandArgument='<%# Eval("ID") %>'
                                                CssClass="btn btn-sm btn-action-red shadow-sm"
                                                ToolTip="Eliminar">
                                                <i class="fas fa-trash-alt"></i> Eliminar
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>

                    <div class="card-footer bg-white p-3">
                        <div class="d-flex justify-content-between align-items-center">
                            <div>
                                <label class="fs-4 me-2">Total:</label>
                                <asp:Label ID="lblTotal" runat="server" CssClass="fs-3 fw-bold text-success">$ 0,00</asp:Label>
                            </div>
                            <asp:Button Text="Finalizar Venta" ID="btnAbrirModalFinalizar" OnClick="btnAbrirModalFinalizar_Click" runat="server" CssClass="btn btn-action-green px-4 py-2 shadow" Enabled="false" />
                        </div>
                    </div>
                </div>

            </ContentTemplate>
        </asp:UpdatePanel>
    </div>


    <%-- MODAL SELECCION PRODUCTOS --%>
    <div class="modal fade" id="modalProductos" tabindex="-1">
        <div class="modal-dialog modal-xl modal-dialog-scrollable modal-dialog-centered">
            <div class="modal-content card-custom">

                <asp:UpdatePanel runat="server" ID="upProductosGrid" UpdateMode="Conditional">
                    <ContentTemplate>

                        <div class="modal-header header-gradient-bg">
                            <h5 class="modal-title fw-bold">Selección de Productos</h5>
                            <asp:Button runat="server" ID="btnCerrarModal" OnClick="btnCerrarModal_Click" CssClass="btn-close btn-close-white" />
                        </div>

                        <div class="modal-body p-0">
                            <div class="table-responsive">
                                <asp:GridView
                                    runat="server"
                                    ID="gvProdcutos"
                                    CssClass="table table-hover table-striped align-middle mb-0"
                                    AutoGenerateColumns="false"
                                    OnSelectedIndexChanged="gvProdcutos_SelectedIndexChanged"
                                    DataKeyNames="IdProducto">

                                    <HeaderStyle BackColor="White" ForeColor="#1A0047" BorderStyle="None" Height="50px" Font-Bold="True" />

                                    <Columns>
                                        <asp:CommandField ShowSelectButton="true" SelectText="Seleccionar" HeaderText="Acción" ControlStyle-CssClass="btn btn-sm btn-outline-primary fw-bold" />
                                        <asp:BoundField DataField="Nombre" HeaderText="Nombre" />
                                        <asp:BoundField DataField="NSerie" HeaderText="N° Serie" />
                                        <asp:BoundField DataField="Marca.Nombre" HeaderText="Marca" />
                                        <asp:BoundField DataField="Categoria.Nombre" HeaderText="Categoría" />
                                        <asp:BoundField DataField="Precio" HeaderText="Precio" DataFormatString="{0:C}" />
                                        <asp:BoundField DataField="Stock" HeaderText="Stock" />
                                        <asp:BoundField DataField="PorcentajeGanancia" HeaderText="% Gan." />
                                        <asp:BoundField DataField="Modelo" HeaderText="Modelo" />
                                        <asp:BoundField DataField="Descripcion" HeaderText="Descripción" />
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>

                        <%-- Pie del Modal para agregar cantidad --%>
                        <div class="modal-footer bg-light">
                            <div class="container-fluid">
                                <div class="row align-items-end justify-content-center text-center g-2">
                                    <div class="col-md-3 text-start">
                                        <small class="text-muted d-block">Producto:</small>
                                        <asp:Label runat="server" ID="lblProducto" CssClass="fw-bold text-primary" Text="-" />
                                    </div>
                                    <div class="col-md-2">
                                        <small class="text-muted d-block">Precio:</small>
                                        <asp:Label runat="server" ID="lblPrecio" CssClass="fw-bold" Text="$ 0" />
                                    </div>
                                    <div class="col-md-2">
                                        <small class="text-muted d-block">Subtotal:</small>
                                        <asp:Label runat="server" ID="lblParcial" CssClass="fw-bold text-success" Text="$ 0" />
                                    </div>
                                    <div class="col-md-2">
                                        <small class="text-muted d-block">Cantidad:</small>
                                        <asp:TextBox
                                            runat="server"
                                            ID="txtCantidad"
                                            CssClass="form-control text-center"
                                            AutoPostBack="true"
                                            OnTextChanged="txtCantidad_TextChanged"
                                            Enabled="false" placeholder="0" />
                                    </div>
                                    <div class="col-md-3">
                                        <asp:Label runat="server" ID="lblErrorCantidad" CssClass="text-danger small d-block mb-1" />
                                        <asp:Button
                                            runat="server"
                                            ID="btnAgregar"
                                            CssClass="btn btn-action-green w-100"
                                            Text="Agregar al Carrito"
                                            OnClick="btnAgregar_Click"
                                            Enabled="false" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>

    <%-- MODAL CONFIRMACION VENTA --%>
    <div class="modal fade" id="modalFinalizar" tabindex="-1" aria-labelledby="modalFinalizarLabel" aria-hidden="true">
        <div class="modal-dialog modal-dialog-centered">
            <div class="modal-content card-custom">
                <div class="modal-header header-gradient-bg">
                    <h5 class="modal-title fw-bold" id="exampleModalLabel">Finalizar Venta</h5>
                </div>
                <div class="modal-body text-center py-4">
                    <i class="fas fa-check-circle text-success fa-3x mb-3"></i>
                    <p class="fs-5">¿Estás seguro de que deseas finalizar la venta?</p>
                    <p class="text-muted small">Esta acción registrará la transacción.</p>
                </div>
                <div class="modal-footer bg-light">
                    <button type="button" class="btn btn-general-blue" data-bs-dismiss="modal">Cancelar</button>
                    <asp:Button Text="Confirmar Venta" runat="server" ID="btnFinalizar" OnClick="btnFinalizar_Click" CssClass="btn btn-action-green" />
                </div>
            </div>
        </div>
    </div>

    <%-- MODAL FIN (EXITO) --%>
    <div class="modal fade" id="modalFIN" tabindex="-1" data-bs-backdrop="static" data-bs-keyboard="false">
        <div class="modal-dialog modal-dialog-centered">
            <div class="modal-content card-custom">
                <div class="modal-header bg-success text-white">
                    <h5 class="modal-title fw-bold">¡Venta Exitosa!</h5>
                </div>
                <div class="modal-body text-center py-4">
                    <i class="fas fa-receipt fa-3x mb-3 text-success"></i>
                    <p class="fs-4 fw-bold">Venta cargada correctamente</p>
                    <p class="mt-2">
                        Número factura:
                        <asp:Label ID="lblNumeroFactura" runat="server"></asp:Label>
                    </p>
                </div>
                <div class="modal-footer bg-light justify-content-center">
                    <asp:Button Text="Ver Reporte" runat="server" CssClass="btn btn-info px-5" ID="btnReporte" OnClick="btnReporte_Click" />
                    <asp:Button Text="Salir" runat="server" CssClass="btn btn-action-green px-5" ID="btnFin" OnClick="btnFin_Click" />
                </div>
            </div>
        </div>
    </div>

    <%-- MODAL CANCELACION (Vacio o estructura base) --%>
    <div class="modal fade" id="modalError" tabindex="-1" aria-labelledby="modalFinalizarLabel" aria-hidden="true" data-bs-backdrop="static">
        <div class="modal-dialog modal-dialog-centered">
            <div class="modal-content card-custom">
                <div class="modal-body text-center py-4">
                    <p class="mt-2">
                        Error: 
                   
                        <asp:Label runat="server" CssClass="text-danger mt-2 d-block fw-bold" ID="lblErrorTotal"></asp:Label>
                    </p>
                </div>
                <div class="modal-footer bg-light justify-content-center">
                    <asp:Button Text="Salir" runat="server" CssClass="btn btn-action-red px-5" ID="btnErrorSalir" OnClick="btnFin_Click" />
                </div>
            </div>
        </div>
    </div>

</asp:Content>

