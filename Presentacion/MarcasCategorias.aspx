<%@ Page Title="Marcas y Categorías" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="MarcasCategorias.aspx.cs" Inherits="Presentacion.MarcasCategorias" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container mt-4">
        <h2 class="mb-4 text-center">Administración de Marcas y Categorías</h2>

        <div class="row">
            <!-- Sección de Marcas -->
            <div class="col-md-6">
                <div class="card shadow-sm p-3 mb-4 rounded">
                    <h4 class="mb-3 text-primary">Marcas</h4>

                    <asp:TextBox ID="txtNuevaMarca" runat="server" CssClass="form-control mb-2" placeholder="Ingrese nueva marca"></asp:TextBox>
                    <asp:Button ID="btnAgregarMarca" runat="server" CssClass="btn btn-success mb-3" Text="Agregar" OnClick="btnAgregarMarca_Click" />
                    <asp:Label ID="lblMensajeMarca" runat="server" CssClass="text-success d-block mb-2"></asp:Label>
                    <h4 id="lblTituloMarcas" runat="server" class="mb-3 text-primary">Lista de Marcas</h4>
                    <h4 id="lblSinMarcas" runat="server" class="mb-3 text-muted" visible="false">Sin registros</h4>
                    <div class="table-responsive shadow-sm rounded mt-3">
                        <asp:GridView ID="gvMarcas" runat="server"
                            CssClass="table table-striped table-hover align-middle"
                            AutoGenerateColumns="False"
                            DataKeyNames="IdMarca"
                            OnRowEditing="gvMarcas_RowEditing"
                            OnRowCancelingEdit="gvMarcas_RowCancelingEdit"
                            OnRowUpdating="gvMarcas_RowUpdating"
                            OnRowCommand="gvMarcas_RowCommand"
                            GridLines="None">

                            <Columns>
                                <asp:BoundField DataField="IdMarca" HeaderText="ID" ReadOnly="true" />

                                <asp:TemplateField HeaderText="Marca">
                                    <ItemTemplate>
                                        <%# Eval("Nombre") %>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtNombreEdit" runat="server" Text='<%# Bind("Nombre") %>' CssClass="form-control form-control-sm"></asp:TextBox>
                                    </EditItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Acciones">
                                    <ItemTemplate>
                                        <asp:Button runat="server" CommandName="Edit" Text="Editar" CssClass="btn btn-warning btn-sm me-2" />
                                        <asp:Button runat="server" CommandName="ConfirmarEliminacionMarca" Text="Eliminar" CssClass="btn btn-danger btn-sm"
                                            CommandArgument='<%# Eval("IdMarca") %>' />
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:Button runat="server" CommandName="Update" Text="Guardar" CssClass="btn btn-primary btn-sm me-2" />
                                        <asp:Button runat="server" CommandName="Cancel" Text="Cancelar" CssClass="btn btn-secondary btn-sm" />
                                    </EditItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
            </div>

            <!-- Sección de Categorías -->
            <div class="col-md-6">
                <div class="card shadow-sm p-3 mb-4 rounded">
                    <h4 class="mb-3 text-primary">Categorías</h4>

                    <asp:TextBox ID="txtNuevaCategoria" runat="server" CssClass="form-control mb-2" placeholder="Ingrese nueva categoría"></asp:TextBox>
                    <asp:Button ID="btnAgregarCategoria" runat="server" CssClass="btn btn-success mb-3" Text="Agregar" OnClick="btnAgregarCategoria_Click" />
                    <asp:Label ID="lblMensajeCategoria" runat="server" CssClass="text-success d-block mb-2"></asp:Label>
                    <h4 id="lblTituloCategorias" runat="server" class="mb-3 text-primary">Lista de Categorías</h4>
                    <h4 id="lblSinCategorias" runat="server" class="mb-3 text-muted" visible="false">Sin registros</h4>
                    <div class="table-responsive shadow-sm rounded mt-3">
                        <asp:GridView ID="gvCategorias" runat="server"
                            CssClass="table table-striped table-hover align-middle"
                            AutoGenerateColumns="False"
                            DataKeyNames="IdCategoria"
                            OnRowEditing="gvCategorias_RowEditing"
                            OnRowCancelingEdit="gvCategorias_RowCancelingEdit"
                            OnRowUpdating="gvCategorias_RowUpdating"
                            OnRowCommand="gvCategorias_RowCommand"
                            GridLines="None">

                            <Columns>
                                <asp:BoundField DataField="IdCategoria" HeaderText="ID" ReadOnly="true" />

                                <asp:TemplateField HeaderText="Categoría">
                                    <ItemTemplate>
                                        <%# Eval("Nombre") %>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtNombreEditCat" runat="server" Text='<%# Bind("Nombre") %>' CssClass="form-control form-control-sm"></asp:TextBox>
                                    </EditItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Acciones">
                                    <ItemTemplate>
                                        <asp:Button runat="server" CommandName="Edit" Text="Editar" CssClass="btn btn-warning btn-sm me-2" />
                                        <asp:Button runat="server" CommandName="ConfirmarEliminacionCategoria" Text="Eliminar" CssClass="btn btn-danger btn-sm"
                                            CommandArgument='<%# Eval("IdCategoria") %>' />
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:Button runat="server" CommandName="Update" Text="Guardar" CssClass="btn btn-primary btn-sm me-2" />
                                        <asp:Button runat="server" CommandName="Cancel" Text="Cancelar" CssClass="btn btn-secondary btn-sm" />
                                    </EditItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <!--  MODAL Bootstrap para confirmación -->
    <div class="modal fade" id="confirmDeleteModal" tabindex="-1" aria-labelledby="confirmDeleteLabel" aria-hidden="true">
        <div class="modal-dialog modal-dialog-centered">
            <div class="modal-content">
                <div class="modal-header bg-danger text-white">
                    <h5 class="modal-title" id="confirmDeleteLabel">Confirmar eliminación</h5>
                    <button type="button" class="btn-close btn-close-white" data-bs-dismiss="modal"></button>
                </div>
                <div class="modal-body">
                    ¿Está seguro de que desea eliminar este registro?
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cancelar</button>
                    <asp:Button ID="btnConfirmarEliminacion" runat="server" CssClass="btn btn-danger" Text="Eliminar" OnClick="btnConfirmarEliminacion_Click" />
                </div>
            </div>
        </div>
    </div>

    <script>
        function abrirModalEliminar(id, tipo) {
            // Guardamos el ID y tipo en campos ocultos
            document.getElementById('<%= hiddenIdEliminar.ClientID %>').value = id;
            document.getElementById('<%= hiddenTipoEliminar.ClientID %>').value = tipo;

            var modal = new bootstrap.Modal(document.getElementById('confirmDeleteModal'));
            modal.show();
        }
    </script>

    <asp:HiddenField ID="hiddenIdEliminar" runat="server" />
    <asp:HiddenField ID="hiddenTipoEliminar" runat="server" />
</asp:Content>
