<%@ Page Title="Gestión de Productos" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ProductoLista.aspx.cs" Inherits="Presentacion.ProductoLista" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div class="container mt-4">

        <asp:UpdatePanel ID="UpdatePanelProductos" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                
                <asp:HiddenField ID="hfIdProductoEliminar" runat="server" />
                
                <div class="row mb-3">
                    <div class="col-md-6">
                        <h2><i class="fas fa-box-open"></i> Gestión de Productos</h2>
                    </div>
                    <div class="col-md-6 text-md-end">
                        
                        <asp:LinkButton ID="btnAbrirModalNuevo" runat="server" CssClass="btn btn-success" OnClick="btnAbrirModalNuevo_Click">
                             ✚ Nuevo Producto
                        </asp:LinkButton>
                    </div>
                </div>

              
                <div class="card shadow-sm mb-4">
                    <div class="card-header bg-light">
                        <h5 class="mb-0">Filtros de Búsqueda</h5>
                    </div>
                    <div class="card-body">
                        <div class="row g-3">
                            <div class="col-md-4">
                                <label class="form-label">Buscar por Nombre:</label>
                                <asp:TextBox ID="txtBuscarNombre" runat="server" CssClass="form-control" AutoPostBack="true" OnTextChanged="Filtro_SelectedIndexChanged"></asp:TextBox>
                            </div>
                            <div class="col-md-4">
                                <label class="form-label">Marca:</label>
                                <asp:DropDownList ID="ddlMarca" runat="server" CssClass="form-select" AutoPostBack="true" OnSelectedIndexChanged="Filtro_SelectedIndexChanged"></asp:DropDownList>
                            </div>
                            <div class="col-md-4">
                                <label class="form-label">Categoría:</label>
                                <asp:DropDownList ID="ddlCategoria" runat="server" CssClass="form-select" AutoPostBack="true" OnSelectedIndexChanged="Filtro_SelectedIndexChanged"></asp:DropDownList>
                            </div>
                            <div class="col-md-4">
                                <label class="form-label">Proveedor:</label>
                                <asp:DropDownList ID="ddlProveedor" runat="server" CssClass="form-select" AutoPostBack="true" OnSelectedIndexChanged="Filtro_SelectedIndexChanged"></asp:DropDownList>
                            </div>
                            <div class="col-md-4">
                                <label class="form-label">Disponibilidad:</label>
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

                <%-- GRILLA DE PRODUCTOS --%>
                <div class="card shadow-sm">
                    <div class="card-body">
                        <div class="table-responsive">
                            <asp:GridView ID="gvProductos" runat="server" CssClass="table table-hover align-middle"
                                AutoGenerateColumns="False" GridLines="Vertical" ShowHeaderWhenEmpty="true"
                                EmptyDataText="No se encontraron productos." AllowPaging="true" PageSize="10"
                                OnPageIndexChanging="gvProductos_PageIndexChanging" AllowSorting="true"
                                OnSorting="gvProductos_Sorting" OnRowCommand="gvProductos_RowCommand"
                                DataKeyNames="IdProducto">

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
                                            <%-- Botón Editar (Llama a RowCommand) --%>
                                            <asp:LinkButton ID="btnEditar" runat="server" CommandName="Editar" 
                                                CommandArgument='<%# Eval("IdProducto") %>' 
                                                CssClass="btn btn-sm btn-outline-primary me-2" ToolTip="Editar">
                                                <i class="fas fa-pencil-alt"></i>
                                            </asp:LinkButton>

                                            <%-- Botón Eliminar (Abre Modal JS) --%>
                                            <button type="button" class="btn btn-sm btn-outline-danger"
                                                onclick="setProductoAEliminar('<%# Eval("IdProducto") %>', '<%# Eval("Nombre") %>')"
                                                data-bs-toggle="modal" data-bs-target="#modalEliminarProducto">
                                                <i class="fas fa-trash-alt"></i>
                                            </button>
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

    <%--  MODAL NUEVO / EDITAR PRODUCTO --%>
    <div class="modal fade" id="modalNuevoProducto" tabindex="-1" aria-labelledby="modalNuevoProductoLabel" aria-hidden="true">
        <div class="modal-dialog modal-lg modal-dialog-centered">
            <div class="modal-content">
                <asp:UpdatePanel ID="UpdatePanelNuevoProducto" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        
                        <%-- Campo Oculto para ID en Edición --%>
                        <asp:HiddenField ID="hfIdProducto" runat="server" />

                        <div class="modal-header bg-success text-white">
                            <h5 class="modal-title" id="modalNuevoProductoLabel"><%= TituloModal %></h5>
                            <button type="button" class="btn-close btn-close-white" data-bs-dismiss="modal"></button>
                        </div>
                        <div class="modal-body">
                            <div class="row g-3">
                                <div class="col-md-6">
                                    <label class="form-label">Nombre</label>
                                    <asp:TextBox ID="txtNombreProducto" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="col-md-6">
                                    <label class="form-label">Número de Serie</label>
                                    <asp:TextBox ID="txtNumeroSerie" runat="server" CssClass="form-control"></asp:TextBox>
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
                                    <asp:TextBox ID="txtPrecio" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="col-md-6">
                                    <label class="form-label">Stock Actual</label>
                                    <asp:TextBox ID="txtStock" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="col-md-6">
                                    <label class="form-label">Stock Mínimo</label>
                                    <asp:TextBox ID="txtStockMinimo" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="col-md-6">
                                    <label class="form-label">% Ganancia</label>
                                    <asp:TextBox ID="txtGanancia" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="col-12">
                                    <label class="form-label">Descripción</label>
                                    <asp:TextBox ID="txtDescripcion" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="2"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="modal-footer">
                            <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cancelar</button>
                            <asp:Button ID="btnGuardarProducto" runat="server" Text="Guardar" CssClass="btn btn-success" OnClick="btnGuardarProducto_Click" />
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>

    <%-- MODAL CONFIRMAR ELIMINAR --%>
    <div class="modal fade" id="modalEliminarProducto" tabindex="-1" aria-hidden="true">
        <div class="modal-dialog modal-dialog-centered">
            <div class="modal-content">
                <div class="modal-header bg-danger text-white">
                    <h5 class="modal-title">🗑 Eliminar Producto</h5>
                    <button type="button" class="btn-close btn-close-white" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <div class="modal-body">
                    <p>¿Estás seguro de que deseas eliminar el producto <strong id="nombreProductoEliminar"></strong>?</p>
                    <p class="text-muted small">Esta acción no se puede deshacer.</p>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cancelar</button>
                    <asp:Button ID="btnEliminarProductoConfirmado" runat="server" Text="Eliminar" CssClass="btn btn-danger" OnClick="btnEliminarProductoConfirmado_Click" />
                </div>
            </div>
        </div>
    </div>

    <%-- TOAST NOTIFICACIONES --%>
    <div aria-live="polite" aria-atomic="true" style="position: fixed; top: 20px; right: 20px; z-index: 1060;">
        <div class="toast hide" role="alert" id="liveToast">
            <div class="toast-header">
                <strong class="me-auto" id="toastHeader">Sistema</strong>
                <button type="button" class="btn-close" data-bs-dismiss="toast"></button>
            </div>
            <div class="toast-body" id="toastBody"></div>
        </div>
    </div>

    <%-- JAVASCRIPT --%>
    <script>
        function setProductoAEliminar(id, nombre) {
            document.getElementById('<%= hfIdProductoEliminar.ClientID %>').value = id;
            document.getElementById('nombreProductoEliminar').textContent = nombre;
        }

        function mostrarToast(mensaje, tipo) {
            var toastEl = document.getElementById('liveToast');
            var toastBody = document.getElementById('toastBody');
            var toastHeader = document.getElementById('toastHeader');

            toastBody.innerText = mensaje;
            if (tipo === 'danger') {
                toastHeader.classList.add('text-danger');
                toastHeader.innerText = "Error";
            } else {
                toastHeader.classList.remove('text-danger');
                toastHeader.classList.add('text-success');
                toastHeader.innerText = "Éxito";
            }

            var toast = new bootstrap.Toast(toastEl);
            toast.show();
        }
    </script>

</asp:Content>
