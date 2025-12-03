<%@ Page Title="Nueva Compra" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="NuevaCompra.aspx.cs" Inherits="Presentacion.NuevaCompra" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

        <style>
        body {
            background: linear-gradient(135deg, #2735F5 0%, #4D079C 100%) !important;
            background-attachment: fixed;
            background-size: cover;
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
            background-color: #8BD100; border: none; color: white; font-weight: 600;
            transition: transform 0.2s;
        }
        .btn-action-green:hover { background-color: #75b300; color: white; transform: scale(1.05); }

        .btn-action-red {
            background-color: #D10000; border: none; color: white; font-weight: 600;
            transition: transform 0.2s;
        }
        .btn-action-red:hover { background-color: #a30000; color: white; transform: scale(1.05); }

        .btn-general-blue {
             background-color: #8FADFA; border: none; color: white; font-weight: 600;
             transition: transform 0.2s;
        }
        .btn-general-blue:hover { background-color: #6c94f7; color: white; transform: scale(1.05); }

        .form-label { font-weight: 600; color: #4D079C; }
    </style>

    <div class="container pb-5">
        <h2 class="text-center mb-5 mt-4 fw-bold"><i class="fas fa-cart-plus me-2"></i>Registrar Nueva Compra</h2>

        <asp:Label ID="lblMensaje" runat="server" Visible="False"></asp:Label>
        <div class="bg-white border rounded p-2">
        <div class="row mb-3">
            <div class="col-md-6">
                <label class="form-label fw-bold">Proveedor:</label>
                <asp:DropDownList ID="ddlProveedores" runat="server" CssClass="form-select"></asp:DropDownList>
            </div>
        </div>

        <div class="card shadow-sm border-0">
            <div class="card-body p-0">
                <div class="table-responsive">
                    <asp:GridView ID="gvProductosFaltantes" runat="server"
                        CssClass="table table-hover table-striped align-middle mb-0"
                        AutoGenerateColumns="False"
                        DataKeyNames="IdProducto"
                        EmptyDataText="No hay productos con bajo stock."
                        GridLines="None">
                        <Columns>
                            <asp:TemplateField ItemStyle-Width="50px" ItemStyle-CssClass="text-center">
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkSeleccionar" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="Nombre" HeaderText="Producto" />
                            <asp:BoundField DataField="Marca.Nombre" HeaderText="Marca" />
                            <asp:BoundField DataField="Stock" HeaderText="Stock Actual" />
                            <asp:BoundField DataField="StockMinimo" HeaderText="Stock Mín." />
                            <asp:TemplateField HeaderText="Cant. a Comprar">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtCantidadCompra" runat="server" CssClass="form-control text-center" TextMode="Number" min="1" placeholder="0"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
        </div>

        <div class="text-center mt-4">
            <asp:Button ID="btnConfirmarCompra" runat="server" Text="Confirmar Compra" CssClass="btn btn-success px-4 py-2" OnClick="btnConfirmarCompra_Click" />
            <a href="Compras.aspx" class="btn btn-secondary px-4 py-2 ms-2">Volver a Compras</a>
        </div>
        </div>
    </div>

    <!-- TOAST NOTIFICACIONES -->
<div aria-live="polite" aria-atomic="true" style="position: fixed; top: 80px; right: 20px; z-index: 1060;">
    <div class="toast hide shadow-lg align-items-center border-0" role="alert" id="liveToast">
        <div class="d-flex">
            <div class="toast-body fw-bold" id="toastBody"></div>
            <button type="button" class="btn-close btn-close-white me-2 m-auto" data-bs-dismiss="toast"></button>
        </div>
    </div>
</div>

<script>
    function mostrarToast(mensaje, tipo) {
        var toastEl = document.getElementById('liveToast');
        var toastBody = document.getElementById('toastBody');

        toastBody.innerText = mensaje;

        if (tipo === 'danger') {
            toastEl.style.backgroundColor = "#D10000";
            toastEl.classList.add('text-white');
        } else if (tipo === 'warning') {
            toastEl.style.backgroundColor = "#ffc107";
            toastEl.classList.remove('text-white');
        } else {
            toastEl.style.backgroundColor = "#8BD100";
            toastEl.classList.add('text-white');
        }

        var toast = new bootstrap.Toast(toastEl);
        toast.show();
    }
</script>

</asp:Content>



