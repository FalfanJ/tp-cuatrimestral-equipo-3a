<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Venta.aspx.cs" Inherits="Presentacion.Venta" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container mt-4">
        <div class="d-flex justify-content-between align-items-center mb-3">
            <h2 class="mb-0">Gestión Ventas</h2>
            <button type="button" class="btn btn-success" data-bs-toggle="modal" data-bs-target="#modalProductos">Agregar Productos</button>
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
                            <asp:BoundField DataField="PrecioParcial" HeaderText="$ARS Parcial" />
                            <asp:BoundField DataField="PorcentajeGanancia" HeaderText="%Ganancia" />
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
                <div class="container">
                    <div class="row">
                        <div class="col">
                            <asp:Label ID="lblTotal" runat="server"></asp:Label>
                        </div>
                        <div class="col">
                            <asp:Button Text="Finalizar" runat="server" />
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>


    <%--Modal Selecion Productos--%>
    <div class="modal fade" id="modalProductos" tabindex="-1">
        <div class="modal-dialog modal-xl modal-dialog-scrollable">
            <div class="modal-content">

                <div class="modal-header">
                    <h1 class="modal-title fs-5">Selecion de Productos</h1>
                    <button type="button" class="btn-close" data-bs-dismiss="modal"></button>
                </div>

                <%--Grilla--%>
                <%--<div class="modal-body">
                    <asp:UpdatePanel runat="server" ID="upProductosGrid" UpdateMode="Conditional">
                        <ContentTemplate>
                            <div class="table-responsive shadow-sm rounded mt-4">
                                <asp:GridView
                                    runat="server"
                                    ID="gvProdcutos"
                                    OnRowCommand="gvProdcutos_RowCommand"
                                    CssClass="table table-striped table-hover align-middle"
                                    AutoGenerateColumns="false">

                                    <HeaderStyle CssClass="table-dark" />
                                    <Columns>
                                        <asp:CommandField ShowSelectButton="true" />
                                        <asp:BoundField DataField="Nombre" HeaderText="Nombre" />
                                        <asp:BoundField DataField="NSerie" HeaderText="Numero Series" />
                                        <asp:BoundField DataField="Marca.Nombre" HeaderText="Marca" />
                                        <asp:BoundField DataField="Categoria.Nombre" HeaderText="Categoria" />
                                        <asp:BoundField DataField="Precio" HeaderText="Precio" />
                                        <asp:BoundField DataField="Stock" HeaderText="Stock" />
                                        <asp:BoundField DataField="PorcentajeGanancia" HeaderText="PorcentajeGanancia" />
                                        <asp:BoundField DataField="Modelo" HeaderText="Modelo" />
                                        <asp:BoundField DataField="Descripcion" HeaderText="Descripcion" />
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>--%>

                <asp:UpdatePanel runat="server" ID="upProductosGrid" UpdateMode="Conditional">
                    <ContentTemplate>
                        <div class="modal-body">
                            <div class="table-responsive shadow-sm rounded mt-4">
                                <asp:GridView
                                    runat="server"
                                    ID="gvProdcutos"
                                    CssClass="table table-striped table-hover align-middle"
                                    AutoGenerateColumns="false"
                                    OnSelectedIndexChanged="gvProdcutos_SelectedIndexChanged"
                                    DataKeyNames="IdProducto"
                                    OnRowCommand="gvProdcutos_RowCommand">

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
                        <div class="modal-footer">
                            <div class="container">
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
                                            OnTextChanged="txtCantidad_TextChanged" />
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
</asp:Content>
