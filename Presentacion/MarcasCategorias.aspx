<%@ Page Title="Marcas y Categorías" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="MarcasCategorias.aspx.cs" Inherits="Presentacion.MarcasCategorias" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <style>
        /* Fondo degradado */
        body {
            background: linear-gradient(135deg, #2735F5 0%, #4D079C 100%) !important;
            background-attachment: fixed;
            background-size: cover;
            min-height: 100vh;
        }

        /* Títulos */
        h2, .text-white-title {
            color: white !important;
            text-shadow: 0 2px 4px rgba(0,0,0,0.3);
        }

        /* Tarjetas */
        .card-custom {
            border: none;
            border-radius: 15px;
            box-shadow: 0 8px 20px rgba(0,0,0,0.25);
            background-color: #ffffff;
            overflow: hidden;
        }

        /* Encabezados */
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

        /* --- BOTONES --- */
        
        /* Verde (#8BD100) */
        .btn-action-green {
            background-color: #8BD100; border: none; color: white; font-weight: 600;
            transition: transform 0.2s;
        }
        .btn-action-green:hover { background-color: #75b300; color: white; transform: scale(1.05); }

        /* Rojo (#D10000) */
        .btn-action-red {
            background-color: #D10000; border: none; color: white; font-weight: 600;
            transition: transform 0.2s;
        }
        .btn-action-red:hover { background-color: #a30000; color: white; transform: scale(1.05); }

        /* Azul Claro (#8FADFA) */
        .btn-general-blue {
             background-color: #8FADFA; border: none; color: white; font-weight: 600;
             transition: transform 0.2s;
        }
        .btn-general-blue:hover { background-color: #6c94f7; color: white; transform: scale(1.05); }

        .form-label { font-weight: 600; color: #4D079C; }
    </style>

    <div class="container pb-5">
        <h2 class="mb-4 mt-4 text-center fw-bold text-white-title">Administración de Marcas y Categorías</h2>

        <div class="row">
            <div class="col-md-6 mb-4">
                <div class="card card-custom h-100">
                    <div class="card-header header-gradient-bg">
                        <h4 class="mb-0 fw-bold">Marcas</h4>
                    </div>
                    
                    <div class="card-body">
                        <div class="input-group mb-3">
                            <asp:TextBox ID="txtNuevaMarca" runat="server" CssClass="form-control" placeholder="Ingrese nueva marca"></asp:TextBox>
                            <asp:Button ID="btnAgregarMarca" runat="server" CssClass="btn btn-action-green" Text="Agregar" OnClick="btnAgregarMarca_Click" />
                        </div>
                        
                        <asp:Label ID="lblMensajeMarca" runat="server" CssClass="text-success d-block mb-2 fw-bold"></asp:Label>
                        
                        <h5 id="lblTituloMarcas" runat="server" class="mb-3 fw-bold" style="color: #4D079C;">Lista de Marcas</h5>
                        <h5 id="lblSinMarcas" runat="server" class="mb-3 text-muted" visible="false">Sin registros</h5>
                        
                        <div class="table-responsive">
                            <asp:GridView ID="gvMarcas" runat="server"
                                CssClass="table table-hover table-striped align-middle mb-0"
                                AutoGenerateColumns="False"
                                DataKeyNames="IdMarca"
                                OnRowEditing="gvMarcas_RowEditing"
                                OnRowCancelingEdit="gvMarcas_RowCancelingEdit"
                                OnRowUpdating="gvMarcas_RowUpdating"
                                OnRowCommand="gvMarcas_RowCommand"
                                GridLines="None">

                                <HeaderStyle BackColor="White" ForeColor="#1A0047" BorderStyle="None" Height="50px" Font-Bold="True" />

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
                                            <asp:Button runat="server" CommandName="Edit" Text="Editar" CssClass="btn btn-action-green btn-sm me-2" />
                                            <asp:Button runat="server" CommandName="ConfirmarEliminacionMarca" Text="Eliminar" CssClass="btn btn-action-red btn-sm"
                                                CommandArgument='<%# Eval("IdMarca") %>' />
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:Button runat="server" CommandName="Update" Text="Guardar" CssClass="btn btn-action-green btn-sm me-2" />
                                            <asp:Button runat="server" CommandName="Cancel" Text="Cancelar" CssClass="btn btn-general-blue btn-sm" />
                                        </EditItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                </div>
            </div>

            <div class="col-md-6 mb-4">
                <div class="card card-custom h-100">
                    <div class="card-header header-gradient-bg">
                        <h4 class="mb-0 fw-bold">Categorías</h4>
                    </div>

                    <div class="card-body">
                        <div class="input-group mb-3">
                            <asp:TextBox ID="txtNuevaCategoria" runat="server" CssClass="form-control" placeholder="Ingrese nueva categoría"></asp:TextBox>
                            <asp:Button ID="btnAgregarCategoria" runat="server" CssClass="btn btn-action-green" Text="Agregar" OnClick="btnAgregarCategoria_Click" />
                        </div>

                        <asp:Label ID="lblMensajeCategoria" runat="server" CssClass="text-success d-block mb-2 fw-bold"></asp:Label>
                        
                        <h5 id="lblTituloCategorias" runat="server" class="mb-3 fw-bold" style="color: #4D079C;">Lista de Categorías</h5>
                        <h5 id="lblSinCategorias" runat="server" class="mb-3 text-muted" visible="false">Sin registros</h5>
                        
                        <div class="table-responsive">
                            <asp:GridView ID="gvCategorias" runat="server"
                                CssClass="table table-hover table-striped align-middle mb-0"
                                AutoGenerateColumns="False"
                                DataKeyNames="IdCategoria"
                                OnRowEditing="gvCategorias_RowEditing"
                                OnRowCancelingEdit="gvCategorias_RowCancelingEdit"
                                OnRowUpdating="gvCategorias_RowUpdating"
                                OnRowCommand="gvCategorias_RowCommand"
                                GridLines="None">

                                <HeaderStyle BackColor="White" ForeColor="#1A0047" BorderStyle="None" Height="50px" Font-Bold="True" />

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
                                            <asp:Button runat="server" CommandName="Edit" Text="Editar" CssClass="btn btn-action-green btn-sm me-2" />
                                            <asp:Button runat="server" CommandName="ConfirmarEliminacionCategoria" Text="Eliminar" CssClass="btn btn-action-red btn-sm"
                                                CommandArgument='<%# Eval("IdCategoria") %>' />
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:Button runat="server" CommandName="Update" Text="Guardar" CssClass="btn btn-action-green btn-sm me-2" />
                                            <asp:Button runat="server" CommandName="Cancel" Text="Cancelar" CssClass="btn btn-general-blue btn-sm" />
                                        </EditItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <div class="modal fade" id="confirmDeleteModal" tabindex="-1" aria-labelledby="confirmDeleteLabel" aria-hidden="true">
        <div class="modal-dialog modal-dialog-centered">
            <div class="modal-content card-custom">
                <div class="modal-header header-red-bg">
                    <h5 class="modal-title fw-bold" id="confirmDeleteLabel">Confirmar eliminación</h5>
                    <button type="button" class="btn-close btn-close-white" data-bs-dismiss="modal"></button>
                </div>
                <div class="modal-body text-center py-4">
                    <i class="fas fa-exclamation-triangle text-danger fa-3x mb-3"></i>
                    <p class="fs-5">¿Está seguro de que desea eliminar este registro?</p>
                </div>
                <div class="modal-footer bg-light">
                    <button type="button" class="btn btn-general-blue" data-bs-dismiss="modal">Cancelar</button>
                    <asp:Button ID="btnConfirmarEliminacion" runat="server" CssClass="btn btn-action-red" Text="Eliminar" OnClick="btnConfirmarEliminacion_Click" />
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