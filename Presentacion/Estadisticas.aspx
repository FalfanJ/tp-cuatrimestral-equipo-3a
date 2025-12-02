<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Estadisticas.aspx.cs" Inherits="Presentacion.Estadisticas" %>

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
    
    <div class="container">
        <h2>Tablero de Estadísticas</h2>
        <hr />

        <div class="row">
            <div class="col-md-6">
                <div class="card mb-3">
                    <div class="card-header bg-primary text-white">Top 5 Clientes (Mayores Compras)</div>
                    <div class="card-body">
                        <asp:GridView ID="gvTopClientes" runat="server" CssClass="table table-striped" AutoGenerateColumns="false">
                            <Columns>
                                <asp:BoundField DataField="NombreLabel" HeaderText="Cliente" />
                                <asp:BoundField DataField="CantidadVentas" HeaderText="Compras" />
                                <asp:BoundField DataField="TotalAcumulado" HeaderText="Total Gastado ($)" DataFormatString="{0:C}" />
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
            </div>
            <div class="col-md-6">
                <div class="card mb-3">
                    <div class="card-header bg-success text-white">Top Vendedores</div>
                    <div class="card-body">
                        <asp:GridView ID="gvTopVendedores" runat="server" CssClass="table table-striped" AutoGenerateColumns="false">
                            <Columns>
                                <asp:BoundField DataField="NombreLabel" HeaderText="Vendedor" />
                                <asp:BoundField DataField="CantidadVentas" HeaderText="Ventas" />
                                <asp:BoundField DataField="TotalAcumulado" HeaderText="Total Facturado ($)" DataFormatString="{0:C}" />
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
            </div>
        </div>

        <div class="row">
            <div class="col-md-12">
                <div class="card mb-3">
                    <div class="card-header bg-info text-white">
                        Productos Más Vendidos
                    </div>
                    <div class="card-body">
                        <div class="form-group row">
                            <label class="col-sm-2 col-form-label">Filtrar por:</label>
                            <div class="col-sm-4">
                                <asp:DropDownList ID="ddlFiltroTiempo" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlFiltroTiempo_SelectedIndexChanged">
                                    <asp:ListItem Text="Última Semana" Value="Semana" />
                                    <asp:ListItem Text="Último Mes" Value="Mes" Selected="True" />
                                    <asp:ListItem Text="Todo el Historial" Value="Todo" />
                                </asp:DropDownList>
                            </div>
                        </div>
                        <br />
                        <asp:GridView ID="gvProductosVendidos" runat="server" CssClass="table table-bordered" AutoGenerateColumns="false">
                            <Columns>
                                <asp:BoundField DataField="NombreProducto" HeaderText="Producto" />
                                <asp:BoundField DataField="CantidadVentas" HeaderText="Unidades Vendidas" />
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
            </div>
        </div>

        <div class="row">
            <div class="col-md-6">
                <div class="card border-danger mb-3">
                    <div class="card-header bg-danger text-white">⚠️ Stock Crítico (Reponer)</div>
                    <div class="card-body">
                        <asp:GridView ID="gvStockCritico" runat="server" CssClass="table table-sm" AutoGenerateColumns="false">
                            <Columns>
                                <asp:BoundField DataField="NombreProducto" HeaderText="Producto" />
                                <asp:BoundField DataField="StockActual" HeaderText="Actual" ItemStyle-Font-Bold="true" ItemStyle-ForeColor="Red" />
                                <asp:BoundField DataField="StockMinimo" HeaderText="Mínimo" />
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
            </div>
            <div class="col-md-6">
                <div class="card border-warning mb-3">
                    <div class="card-header bg-warning text-dark">📦 Exceso de Stock (Baja Rotación)</div>
                    <div class="card-body">
                         <small class="text-muted">Productos con mucho stock sin ventas en 30 días.</small>
                        <asp:GridView ID="gvStockExceso" runat="server" CssClass="table table-sm" AutoGenerateColumns="false">
                            <Columns>
                                <asp:BoundField DataField="NombreProducto" HeaderText="Producto" />
                                <asp:BoundField DataField="StockActual" HeaderText="Actual" />
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
            </div>
        </div>

        <div class="row">
            <div class="col-md-12">
                 <div class="card mb-3">
                    <div class="card-header bg-dark text-white">Margen de Ganancia por Categoría</div>
                    <div class="card-body">
                        <asp:GridView ID="gvMargenCategoria" runat="server" CssClass="table table-hover" AutoGenerateColumns="false">
                            <Columns>
                                <asp:BoundField DataField="Categoria" HeaderText="Categoría" />
                                <asp:BoundField DataField="GananciaTotal" HeaderText="Ganancia Estimada ($)" DataFormatString="{0:C}" />
                            </Columns>
                        </asp:GridView>
                    </div>
                 </div>
            </div>
        </div>

    </div>
</asp:Content>