<%@ Page Title="Compras" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Compras.aspx.cs" Inherits="Presentacion.Compras" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        body {
            background: linear-gradient(135deg, #2735F5 0%, #4D079C 100%) !important;
            background-attachment: fixed;
            min-height: 100vh;
        }

        h2 {
            color: white !important;
            text-shadow: 0 2px 4px rgba(0,0,0,0.3);
            font-weight: bold;
        }

        .card-custom {
            border: none;
            border-radius: 15px;
            box-shadow: 0 8px 20px rgba(0,0,0,0.25);
            background-color: #fff;
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
                transform: scale(1.05);
            }

        .form-label {
            font-weight: 600;
            color: #4D079C;
        }
    </style>

    <div class="container pb-5">
        <div class="d-flex justify-content-between align-items-center">
            <h2 class="fw-bold"><i class="fas fa-cart-plus me-2"></i>Compras</h2>
            <asp:Button ID="btnNuevaCompra" runat="server" CssClass="btn btn-action-green btn-lg shadow" Text="Registrar nueva compra" OnClick="btnNuevaCompra_Click" />
        </div>

        <asp:Label ID="lblMensaje" runat="server" CssClass="text-danger fw-bold" Visible="False"></asp:Label>

        <!-- Filtros -->
        <div class="card card-custom my-4">
            <div class="card-header header-gradient-bg">
                <h5 class="mb-0"><i class="fas fa-filter me-2"></i>Filtros de Búsqueda</h5>
            </div>
            <div class="card-body">
                <div class="row g-3">
                    <div class="col-md-4">
                        <label class="form-label">Proveedor:</label>
                        <asp:DropDownList ID="ddlProveedorFiltro" runat="server" CssClass="form-select" AutoPostBack="true" OnSelectedIndexChanged="Filtro_SelectedIndexChanged"></asp:DropDownList>
                    </div>
                    <div class="col-md-4">
                        <label class="form-label">Fecha Desde:</label>
                        <asp:TextBox ID="txtFechaDesde" runat="server" CssClass="form-control" TextMode="Date" AutoPostBack="true" OnTextChanged="Filtro_SelectedIndexChanged"></asp:TextBox>
                    </div>
                    <div class="col-md-4">
                        <label class="form-label">Fecha Hasta:</label>
                        <asp:TextBox ID="txtFechaHasta" runat="server" CssClass="form-control" TextMode="Date" AutoPostBack="true" OnTextChanged="Filtro_SelectedIndexChanged"></asp:TextBox>
                    </div>
                    <div class="col-md-4">
                        <label class="form-label">Usuario Responsable:</label>
                        <asp:DropDownList ID="ddlUsuarioFiltro" runat="server" CssClass="form-select" AutoPostBack="true" OnSelectedIndexChanged="Filtro_SelectedIndexChanged">
                        </asp:DropDownList>
                    </div>
                    <div class="col-md-4 d-flex align-items-end">
                        <asp:Button ID="btnLimpiarFiltros" runat="server" Text="Limpiar Filtros" CssClass="btn btn-general-blue w-100" OnClick="btnLimpiarFiltros_Click" />
                    </div>
                </div>
            </div>
        </div>

        <!-- Registros de coompras -->
        <div class="card card-custom mt-4">
            <div class="card-header header-gradient-bg">
                <h4 class="mb-0 fw-bold"><i class="fas fa-list me-2"></i>Compras Realizadas</h4>
            </div>
            <div class="card-body">
                <div class="table-responsive">
                    <asp:GridView ID="gvCompras" runat="server"
                        CssClass="table table-hover table-striped align-middle"
                        AutoGenerateColumns="False"
                        DataKeyNames="IdCompra"
                        EmptyDataText="No se han registrado compras."
                        GridLines="None">
                        <Columns>
                            <asp:BoundField DataField="IdCompra" HeaderText="ID" />
                            <asp:TemplateField HeaderText="Usuario Responsable">
                                <ItemTemplate>
                                    <%# Eval("Usuario.Email") %>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:BoundField DataField="Fecha" HeaderText="Fecha" DataFormatString="{0:dd/MM/yyyy}" />
                            <asp:BoundField DataField="ProveedorNombre" HeaderText="Proveedor" />
                            <asp:BoundField DataField="TotalProductos" HeaderText="Total Compra" DataFormatString="{0:C}" HtmlEncode="False" />
                            <asp:TemplateField HeaderText="Detalle">
                                <ItemTemplate>
                                    <%# FormatearDetalle(Eval("Detalle")) %>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
