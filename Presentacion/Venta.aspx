<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Venta.aspx.cs" Inherits="Presentacion.Venta" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type='text/javascript'>
        function closeModal() {
            var myModalEl = document.getElementById('modalProductos');
            var modal = bootstrap.Modal.getInstance(myModalEl); // Returns a Bootstrap modal instance

            if (!modal) {
                modal = new bootstrap.Modal(myModalEl);
            }
            modal.hide();
        }
        function openModalFin() {
            var myModalEl = document.getElementById('modalFinalizar');
            var modal = bootstrap.Modal.getInstance(myModalEl); // Returns a Bootstrap modal instance
            if (!modal) {
                modal = new bootstrap.Modal(myModalEl);
            }
            modal.show();
        }
        function openModalFinFin() {
            var myModalEl = document.getElementById('modalFIN');
            var modal = bootstrap.Modal.getInstance(myModalEl); // Returns a Bootstrap modal instance
            if (!modal) {
                modal = new bootstrap.Modal(myModalEl);
            }
            modal.show();
        }
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container mt-4">
        <div class="d-flex justify-content-between align-items-center mb-3">
            <div class="container">
                <h2 class="mb-0">Gestión Ventas</h2>
                <asp:Button Text="Cencelar Venta" runat="server" CssClass="btn btn-secondary btn-dark" ID="btnCancelarVenta" OnClick="btnCancelarVenta_Click"/>
                <button type="button" class="btn btn-success" data-bs-toggle="modal" data-bs-target="#modalProductos">Agregar Productos</button>
            </div>
        </div>


        <%--Grilla de productos a veder--%>
        <asp:UpdatePanel runat="server" ID="upDetalleGrid" UpdateMode="Conditional">
            <ContentTemplate>
                <div class="table-responsive shadow-sm rounded mt-4">
                    <asp:GridView
                        runat="server"
                        ID="gvDetalle"
                        CssClass="table table-striped table-hover align-middle"
                        AutoGenerateColumns="false"
                        GridLines="None"
                        OnRowCommand="gvDetalle_RowCommand"
                        EmptyDataText="No se han agregado productos."
                        DataKeyNames="ID">

                        <HeaderStyle CssClass="table-dark" />
                        <Columns>
                            <asp:BoundField DataField="Producto.Nombre" HeaderText="Nombre" />
                            <asp:BoundField DataField="Producto.NSerie" HeaderText="Numero Serie" />
                            <asp:BoundField DataField="Cantidad" HeaderText="Cantidad" />
                            <asp:BoundField DataField="PrecioUnitario" HeaderText="$ARS Unitario" />
                            <asp:BoundField DataField="PorcentajeGanancia" HeaderText="%Ganancia" />
                            <asp:BoundField DataField="PrecioParcial" HeaderText="$ARS Parcial" />
                            <asp:TemplateField HeaderText="Eliminar" ItemStyle-CssClass="text-center">
                                <ItemTemplate>
                                    <asp:LinkButton
                                        runat="server"
                                        CommandName="Eliminar"
                                        CommandArgument='<%# Eval("ID") %>'
                                        CssClass="btn btn-sm btn-outline-primary me-2"
                                        ToolTip="Eliminar" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
                <div class="container mt-4">
                    <div class="d-flex justify-content-between align-items-center mb-3">
                        <label>Total: </label>
                        <asp:Label ID="lblTotal" runat="server"></asp:Label>
                        <asp:Button Text="Finalizar" ID="btnAbrirModalFinalizar" OnClick="btnAbrirModalFinalizar_Click" runat="server" CssClass="btn btn-primary" Enabled="false" />
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>


    <%--Modal Selecion Productos--%>
    <div class="modal fade" id="modalProductos" tabindex="-1">
        <div class="modal-dialog modal-xl modal-dialog-scrollable">
            <div class="modal-content">

                <asp:UpdatePanel runat="server" ID="upProductosGrid" UpdateMode="Conditional">
                    <ContentTemplate>
                        <%--Header--%>
                        <div class="modal-header">
                            <h1 class="modal-title fs-5">Selecion de Productos</h1>
                            <asp:Button runat="server" ID="btnCerrarModal" OnClick="btnCerrarModal_Click" CssClass="btn-close" />
                        </div>

                        <%--Grilla--%>
                        <div class="modal-body">
                            <div class="table-responsive shadow-sm rounded mt-4">
                                <asp:GridView
                                    runat="server"
                                    ID="gvProdcutos"
                                    CssClass="table table-striped table-hover align-middle"
                                    AutoGenerateColumns="false"
                                    OnSelectedIndexChanged="gvProdcutos_SelectedIndexChanged"
                                    DataKeyNames="IdProducto">

                                    <HeaderStyle CssClass="table-dark" />
                                    <Columns>
                                        <asp:CommandField ShowSelectButton="true" SelectText="Selecionar" HeaderText="Accion" />
                                        <asp:BoundField DataField="Nombre" HeaderText="Nombre" />
                                        <asp:BoundField DataField="NSerie" HeaderText="Numero Series" />
                                        <asp:BoundField DataField="Marca.Nombre" HeaderText="Marca" />
                                        <asp:BoundField DataField="Categoria.Nombre" HeaderText="Categoria" />
                                        <asp:BoundField DataField="Precio" HeaderText="Precio" />
                                        <asp:BoundField DataField="Stock" HeaderText="Stock" />
                                        <asp:BoundField DataField="PorcentajeGanancia" HeaderText="% Ganancia" />
                                        <asp:BoundField DataField="Modelo" HeaderText="Modelo" />
                                        <asp:BoundField DataField="Descripcion" HeaderText="Descripcion" />
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>

                        <%--Pie--%>
                        <div class="modal-footer">
                            <div class="container text-center">
                                <div class="row row-cols-5">
                                    <div class="col">
                                        <asp:Label runat="server" ID="lblProducto" CssClass="fw-bold me-3" />
                                    </div>
                                    <div class="col">
                                        <asp:Label runat="server" ID="lblPrecio" CssClass="me-3" />
                                    </div>
                                    <div class="col">
                                        <asp:Label runat="server" ID="lblParcial" CssClass="ms-3 fw-bold" />
                                    </div>
                                    <div class="col">
                                        <asp:TextBox
                                            runat="server"
                                            ID="txtCantidad"
                                            CssClass="form-control d-inline-block"
                                            AutoPostBack="true"
                                            OnTextChanged="txtCantidad_TextChanged"
                                            Enabled="false" placeholder="Cantidad" />
                                        <asp:Label runat="server" ID="lblErrorCantidad" CssClass="text-danger" />
                                    </div>
                                    <div class="col">
                                        <asp:Button
                                            runat="server"
                                            ID="btnAgregar"
                                            CssClass="btn btn-primary ms-3"
                                            Text="Agregar"
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

    <%--Modal confirmacion Venta--%>
    <div class="modal fade" id="modalFinalizar" tabindex="-1" aria-labelledby="modalFinalizarLabel" aria-hidden="true">
        <div class="modal-dialog modal-dialog-centered">
            <div class="modal-content">
                <div class="modal-header">
                    <h1 class="modal-title fs-5" id="exampleModalLabel">Finalizacion Venta</h1>
                </div>
                <div class="modal-body text-center py-4">
                    <p class="fs-5">¿Estás seguro de que deseas finalizar la venta?</p>
                    <p class="text-muted small">Esta acción no se puede deshacer.</p>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cancelar</button>
                    <asp:Button Text="Finalizar venta" runat="server" ID="btnFinalizar" OnClick="btnFinalizar_Click" CssClass="btn btn-primary" />
                </div>
            </div>
        </div>
    </div>
    <div class="modal fade" id="modalFIN" tabindex="-1" data-bs-backdrop="static" data-bs-keyboard="false">
        <div class="modal-dialog modal-dialog-centered">
            <div class="modal-content">
                <div class="modal-body text-center py-4">
                    <p class="fs-5">Venta cargada correctamente</p>
                </div>
                <div class="modal-footer text-center">
                    <asp:Button Text="Salir" runat="server" CssClass="btn btn-success" ID="btnFin" OnClick="btnFin_Click"/>
                </div>
            </div>
        </div>
    </div>

    <%--Modal Cancelacion--%>
    <div class="modal fade" id="modalCaneclacion" tabindex="-1" aria-labelledby="modalFinalizarLabel" aria-hidden="true">
        <div class="modal-dialog modal-dialog-centered">
            <div class="modal-content">
                <div class="modal-header">

                </div>
            </div>
        </div>
    </div>
</asp:Content>
