<%@ Page Title="Gestión de Productos" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ProductoLista.aspx.cs" Inherits="Presentacion.ProductoLista" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <style>
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

        .header-red-bg {
            background-color: #D10000;
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

        .badge-stock-ok {
            background-color: #8BD100;
            font-size: 0.9em;
        }

        .badge-stock-bad {
            background-color: #D10000;
            font-size: 0.9em;
        }

        .form-label {
            font-weight: 600;
            color: #4D079C;
        }
    </style>

    <div class="container pb-5">

        <asp:UpdatePanel ID="UpdatePanelProductos" runat="server" UpdateMode="Conditional">
            <ContentTemplate>

                <asp:HiddenField ID="hfIdProductoEliminar" runat="server" />

                <div class="row mb-4 mt-4 align-items-center">
                    <div class="col-md-6">
                        <h2 class="fw-bold"><i class="fas fa-box-open me-2"></i>Gestión de Productos</h2>
                    </div>
                    <div class="col-md-6 text-md-end">
                        <asp:LinkButton ID="btnAbrirModalNuevo" runat="server" CssClass="btn btn-action-green btn-lg shadow" OnClick="btnAbrirModalNuevo_Click">
                                <i class="fas fa-plus-circle me-1"></i> Nuevo Producto
                        </asp:LinkButton>
                    </div>
                </div>

                <%-- SECCIÓN DE FILTROS --%>
                <div class="card card-custom mb-4">
                    <div class="card-header header-gradient-bg">
                        <h5 class="mb-0"><i class="fas fa-filter me-2"></i>Filtros de Búsqueda</h5>
                    </div>
                    <div class="card-body">
                        <div class="row g-3">
                            <div class="col-md-4">
                                <label class="form-label">Buscar por Nombre:</label>
                                <div class="input-group">
                                    <span class="input-group-text bg-light"><i class="fas fa-search text-muted"></i></span>
                                    <asp:TextBox ID="txtBuscarNombre" runat="server" CssClass="form-control" AutoPostBack="true" OnTextChanged="Filtro_SelectedIndexChanged" placeholder="Ej: Martillo..."></asp:TextBox>
                                </div>
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
                                <asp:Button ID="btnLimpiarFiltros" runat="server" Text="Limpiar Filtros" CssClass="btn btn-general-blue w-100" OnClick="btnLimpiarFiltros_Click" />
                            </div>
                        </div>
                    </div>
                </div>

                <%-- GRILLA DE PRODUCTOS --%>
                <div class="card card-custom shadow">
                    <div class="card-body p-0">
                        <div class="table-responsive">
                            <asp:GridView ID="gvProductos" runat="server" CssClass="table table-hover table-striped align-middle mb-0"
                                AutoGenerateColumns="False" GridLines="None" ShowHeaderWhenEmpty="true"
                                EmptyDataText="No se encontraron productos." AllowPaging="true" PageSize="10"
                                OnPageIndexChanging="gvProductos_PageIndexChanging" AllowSorting="true"
                                OnSorting="gvProductos_Sorting" OnRowCommand="gvProductos_RowCommand"
                                DataKeyNames="IdProducto">

                                <HeaderStyle BackColor="White" ForeColor="#1A0047" BorderStyle="None" Height="50px" Font-Bold="True" />
                                <PagerStyle CssClass="pagination-container p-3 my-2" HorizontalAlign="Center" BackColor="Transparent" />

                                <Columns>
                                    <asp:BoundField DataField="Nombre" HeaderText="Producto" SortExpression="Nombre" />
                                    <asp:TemplateField HeaderText="Marca" SortExpression="Marca.Nombre">
                                        <ItemTemplate><span class="fw-bold" style="color: #4D079C"><%# Eval("Marca.Nombre") %></span></ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Categoría" SortExpression="Categoria.Nombre">
                                        <ItemTemplate><span class="badge bg-light text-dark border"><%# Eval("Categoria.Nombre") %></span></ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="Precio" HeaderText="Precio" DataFormatString="{0:C}" ItemStyle-CssClass="text-end fw-bold" ItemStyle-ForeColor="#8BD100" SortExpression="Precio" />
                                    <asp:BoundField DataField="Stock" HeaderText="Stock" ItemStyle-CssClass="text-center" SortExpression="Stock" />

                                    <asp:TemplateField HeaderText="Disponibilidad" ItemStyle-CssClass="text-center">
                                        <ItemTemplate>
                                            <span class="badge rounded-pill <%# (short)Eval("Stock") > 0 ? "badge-stock-ok" : "badge-stock-bad" %>">
                                                <%# (short)Eval("Stock") > 0 ? "Con Stock" : "Sin Stock" %>
                                            </span>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Acciones" ItemStyle-CssClass="text-center">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="btnEditar" runat="server" CommandName="Editar"
                                                CommandArgument='<%# Eval("IdProducto") %>'
                                                CssClass="btn btn-sm btn-action-green me-1 shadow-sm" ToolTip="Editar">
                                                <i class="fas fa-pencil-alt"></i> Editar
                                            </asp:LinkButton>

                                            <button type="button" class="btn btn-sm btn-action-red shadow-sm"
                                                onclick="setProductoAEliminar('<%# Eval("IdProducto") %>', '<%# Eval("Nombre") %>')"
                                                data-bs-toggle="modal" data-bs-target="#modalEliminarProducto">
                                                <i class="fas fa-trash-alt"></i>Eliminar
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

    <%-- MODAL NUEVO / EDITAR --%>
    <div class="modal fade" id="modalNuevoProducto" tabindex="-1" aria-labelledby="modalNuevoProductoLabel" aria-hidden="true">
        <div class="modal-dialog modal-lg modal-dialog-centered">
            <div class="modal-content card-custom">
                <asp:UpdatePanel ID="UpdatePanelNuevoProducto" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:HiddenField ID="hfIdProducto" runat="server" />
                        <div class="modal-header header-gradient-bg">
                            <h5 class="modal-title fw-bold" id="modalNuevoProductoLabel"><%= TituloModal %></h5>
                            <button type="button" class="btn-close btn-close-white" data-bs-dismiss="modal"></button>
                        </div>
                        <div class="modal-body p-4">
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
                                    <div class="input-group">
                                        <span class="input-group-text">$</span>
                                        <asp:TextBox ID="txtPrecio" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <label class="form-label">Stock Actual</label>
                                    <asp:TextBox ID="txtStock" runat="server" CssClass="form-control" TextMode="Number"></asp:TextBox>
                                </div>
                                <div class="col-md-6">
                                    <label class="form-label">Stock Mínimo</label>
                                    <asp:TextBox ID="txtStockMinimo" runat="server" CssClass="form-control" TextMode="Number"></asp:TextBox>
                                </div>
                                <div class="col-md-6">
                                    <label class="form-label">% Ganancia</label>
                                    <asp:TextBox ID="txtGanancia" runat="server" CssClass="form-control" TextMode="Number"></asp:TextBox>
                                </div>
                                <div class="col-12">
                                    <label class="form-label">Descripción</label>
                                    <asp:TextBox ID="txtDescripcion" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="2"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="modal-footer bg-light">
                            <button type="button" class="btn btn-general-blue" data-bs-dismiss="modal">Cancelar</button>
                            <asp:Button ID="btnGuardarProducto" runat="server" Text="Guardar" CssClass="btn btn-action-green px-4" OnClick="btnGuardarProducto_Click" />
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>

    <%-- MODAL ELIMINAR --%>
    <div class="modal fade" id="modalEliminarProducto" tabindex="-1" aria-hidden="true">
        <div class="modal-dialog modal-dialog-centered">
            <div class="modal-content card-custom">
                <div class="modal-header header-red-bg">
                    <h5 class="modal-title fw-bold">🗑 Eliminar Producto</h5>
                    <button type="button" class="btn-close btn-close-white" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <div class="modal-body text-center py-4">
                    <i class="fas fa-exclamation-circle text-danger fa-3x mb-3"></i>
                    <p class="fs-5">¿Estás seguro de que deseas eliminar el producto?</p>
                    <p class="fw-bold text-danger fs-4" id="nombreProductoEliminar"></p>
                    <p class="text-muted small">Esta acción no se puede deshacer.</p>
                </div>
                <div class="modal-footer bg-light">
                    <button type="button" class="btn btn-general-blue" data-bs-dismiss="modal">Cancelar</button>
                    <asp:Button ID="btnEliminarProductoConfirmado" runat="server" Text="Confirmar Eliminación" CssClass="btn btn-action-red" OnClick="btnEliminarProductoConfirmado_Click" />
                </div>
            </div>
        </div>
    </div>

    <%-- TOAST NOTIFICACIONES --%>
    <div aria-live="polite" aria-atomic="true" style="position: fixed; top: 80px; right: 20px; z-index: 1060;">
        <div class="toast hide shadow-lg align-items-center border-0" role="alert" id="liveToast">
            <div class="d-flex">
                <div class="toast-body fw-bold" id="toastBody"></div>
                <button type="button" class="btn-close btn-close-white me-2 m-auto" data-bs-dismiss="toast"></button>
            </div>
        </div>
    </div>

    <script>
        function setProductoAEliminar(id, nombre) {
            document.getElementById('<%= hfIdProductoEliminar.ClientID %>').value = id;
            document.getElementById('nombreProductoEliminar').textContent = nombre;
        }

        function mostrarToast(mensaje, tipo) {
            var toastEl = document.getElementById('liveToast');
            var toastBody = document.getElementById('toastBody');

            toastBody.innerText = mensaje;

            if (tipo === 'danger') {
                toastEl.style.backgroundColor = "#D10000"; // Rojo
                toastEl.classList.add('text-white');
            } else {
                toastEl.style.backgroundColor = "#8BD100"; // Verde
                toastEl.classList.add('text-white');
            }

            var toast = new bootstrap.Toast(toastEl);
            toast.show();
        }
    </script>

</asp:Content>
