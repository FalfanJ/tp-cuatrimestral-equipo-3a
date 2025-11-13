<%@ Page Title="Gestión de Productos" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Productos.aspx.cs" Inherits="Presentacion.ProductoLista" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div class="container mt-4">

        <%-- Panel de actualización para filtros y grilla --%>
        <asp:UpdatePanel ID="UpdatePanelProductos" runat="server" UpdateMode="Conditional">
            <ContentTemplate>

                <div class="row mb-3">
                    <div class="col-md-6">
                        <h2><i class="fas fa-box-open"></i> Gestión de Productos</h2>
                    </div>
                    <div class="col-md-6 text-md-end">
                        <%-- BOTÓN QUE ABRE EL MODAL --%>
                        <button type="button" class="btn btn-success" data-bs-toggle="modal" data-bs-target="#modalNuevoProducto">
                            ✚ Nuevo Producto
                        </button>
                    </div>
                </div>

                <%-- SECCIÓN DE FILTROS --%>
                <div class="card shadow-sm mb-4">
                    <div class="card-header bg-light">
                        <h5 class="mb-0">Filtros de Búsqueda</h5>
                    </div>
                    <div class="card-body">
                        <div class="row g-3">
                            <div class="col-md-4">
                                <label for="txtBuscarNombre" class="form-label">Buscar por Nombre:</label>
                                <asp:TextBox ID="txtBuscarNombre" runat="server" CssClass="form-control" AutoPostBack="true" OnTextChanged="Filtro_SelectedIndexChanged"></asp:TextBox>
                            </div>
                            <div class="col-md-4">
                                <label for="ddlMarca" class="form-label">Marca:</label>
                                <asp:DropDownList ID="ddlMarca" runat="server" CssClass="form-select" AutoPostBack="true" OnSelectedIndexChanged="Filtro_SelectedIndexChanged">
                                </asp:DropDownList>
                            </div>
                            <div class="col-md-4">
                                <label for="ddlCategoria" class="form-label">Categoría (Tipo):</label>
                                <asp:DropDownList ID="ddlCategoria" runat="server" CssClass="form-select" AutoPostBack="true" OnSelectedIndexChanged="Filtro_SelectedIndexChanged">
                                </asp:DropDownList>
                            </div>
                            <div class="col-md-4">
                                <label for="ddlProveedor" class="form-label">Proveedor:</label>
                                <asp:DropDownList ID="ddlProveedor" runat="server" CssClass="form-select" AutoPostBack="true" OnSelectedIndexChanged="Filtro_SelectedIndexChanged">
                                </asp:DropDownList>
                            </div>
                            <div class="col-md-4">
                                <label for="ddlStock" class="form-label">Disponibilidad:</label>
                                <asp:DropDownList ID="ddlStock" runat="server" CssClass="form-select" AutoPostBack="true" OnSelectedIndexChanged="Filtro_SelectedIndexChanged">
                                    <asp:ListItem Value="0">Todos</asp:ListItem>
                                    <asp:ListItem Value="1">Con Stock</asp:ListItem>
                                    <asp:ListItem Value="2">Sin Stock</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div class="col-md-4 d-flex align-items-end">
                                <asp:Button ID="btnLimpiarFiltros" runat="server" Text="Limpiar Filtros" CssClass="btn btn-outline-secondary" OnClick="btnLimpiarFiltros_Click" />
                            </div>
                        </div>
                    </div>
                </div>

                <%-- ÁREA DE LA GRILLA DE PRODUCTOS --%>
                <div class="card shadow-sm">
                    <div class="card-body">
                        <div class="table-responsive">
                            <asp:GridView ID="gvProductos"
                                runat="server"
                                CssClass="table table-hover align-middle"
                                AutoGenerateColumns="False"
                                GridLines="Vertical"
                                ShowHeaderWhenEmpty="true"
                                EmptyDataText="No se encontraron productos con los filtros aplicados."
                                AllowPaging="true"
                                PageSize="10"
                                OnPageIndexChanging="gvProductos_PageIndexChanging"
                                AllowSorting="true"
                                OnSorting="gvProductos_Sorting"
                                OnRowCommand="gvProductos_RowCommand">

                                <HeaderStyle CssClass="table-dark" />
                                <PagerStyle CssClass="pagination-container" />

                                <Columns>
                                    <asp:BoundField DataField="Nombre" HeaderText="Producto" SortExpression="Nombre" />
                                    <asp:TemplateField HeaderText="Marca" SortExpression="Marca.Nombre">
                                        <ItemTemplate><%# Eval("Marca.Nombre") %></ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Categoría" SortExpression="Categoria.Nombre">
                                        <ItemTemplate><%# Eval("Categoria.Nombre") %></ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="Precio" HeaderText="Precio" DataFormatString="{0:C}" ItemStyle-CssClass="text-end" SortExpression="Precio" />
                                    <asp:BoundField DataField="Stock" HeaderText="Stock" ItemStyle-CssClass="text-center" SortExpression="Stock" />
                                    <asp:TemplateField HeaderText="Disponibilidad" ItemStyle-CssClass="text-center">
                                        <ItemTemplate>
                                            <span class="badge bg-<%# (short)Eval("Stock") > 0 ? "success" : "danger" %>">
                                                <%# (short)Eval("Stock") > 0 ? "Con Stock" : "Sin Stock" %>
                                            </span>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Acciones" ItemStyle-CssClass="text-center">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="btnEditar" runat="server"
                                                CommandName="Editar"
                                                CommandArgument='<%# Eval("IdProducto") %>'
                                                CssClass="btn btn-sm btn-outline-primary"
                                                ToolTip="Editar Producto">
                                                <i class="fas fa-pencil-alt"></i> Editar
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                </div>

            </ContentTemplate>
        </asp:UpdatePanel>
    </div>

    <%-- 🟢 MODAL NUEVO PRODUCTO --%>
    <div class="modal fade" id="modalNuevoProducto" tabindex="-1" aria-labelledby="modalNuevoProductoLabel" aria-hidden="true">
        <div class="modal-dialog modal-lg modal-dialog-centered">
            <div class="modal-content">
                <asp:UpdatePanel ID="UpdatePanelNuevoProducto" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <div class="modal-header bg-success text-white">
                            <h5 class="modal-title" id="modalNuevoProductoLabel">➕ Nuevo Producto</h5>
                            <button type="button" class="btn-close btn-close-white" data-bs-dismiss="modal"></button>
                        </div>
                        <div class="modal-body">
                            <div class="row g-3">
                                <div class="col-md-6">
                                    <label class="form-label">Nombre</label>
                                    <asp:TextBox ID="txtNombreProducto" runat="server" CssClass="form-control" placeholder="Ej: Teclado Mecánico"></asp:TextBox>
                                </div>
                                <div class="col-md-6">
                                    <label class="form-label">Número de Serie</label>
                                    <asp:TextBox ID="txtNumeroSerie" runat="server" CssClass="form-control" placeholder="Ej: SN12345"></asp:TextBox>
                                </div>
                                <div class="col-md-6">
                                    <label class="form-label">Marca</label>
                                    <asp:DropDownList ID="ddlMarcaNuevo" runat="server" CssClass="form-select"></asp:DropDownList>
                                </div>
                                <div class="col-md-6">
                                    <label class="form-label">Categoría</label>
                                    <asp:DropDownList ID="ddlCategoriaNuevo" runat="server" CssClass="form-select"></asp:DropDownList>
                                </div>
                                <div class="col-md-6">
                                    <label class="form-label">Precio</label>
                                    <asp:TextBox ID="txtPrecio" runat="server" CssClass="form-control" placeholder="Ej: 25000"></asp:TextBox>
                                </div>
                                <div class="col-md-6">
                                    <label class="form-label">Stock Actual</label>
                                    <asp:TextBox ID="txtStock" runat="server" CssClass="form-control" placeholder="Ej: 15"></asp:TextBox>
                                </div>
                                <div class="col-md-6">
                                    <label class="form-label">Stock Mínimo</label>
                                    <asp:TextBox ID="txtStockMinimo" runat="server" CssClass="form-control" placeholder="Ej: 5"></asp:TextBox>
                                </div>
                                <div class="col-md-6">
                                    <label class="form-label">% Ganancia</label>
                                    <asp:TextBox ID="txtGanancia" runat="server" CssClass="form-control" placeholder="Ej: 25"></asp:TextBox>
                                </div>
                                <div class="col-12">
                                    <label class="form-label">Descripción</label>
                                    <asp:TextBox ID="txtDescripcion" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="2" placeholder="Ej: Producto de alta calidad"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="modal-footer">
                            <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cancelar</button>
                            <asp:Button ID="btnGuardarProducto" runat="server" Text="Guardar Producto" CssClass="btn btn-success" OnClick="btnGuardarProducto_Click" />
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>

</asp:Content>
