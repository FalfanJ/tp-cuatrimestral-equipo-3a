<%@ Page Title="Compras y Ventas" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ComprasVentas.aspx.cs" Inherits="Presentacion.WebForm3" %>

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
            transition: transform 0.3s ease;
        }
        
        .card-custom:hover {
            transform: translateY(-5px);
        }

        .header-gradient-bg {
            background: linear-gradient(to right, #2735F5, #4D079C);
            color: white;
            padding: 15px 20px;
        }

        .btn-action-green {
            background-color: #8BD100; border: none; color: white; font-weight: 600;
            transition: transform 0.2s;
        }
        .btn-action-green:hover { background-color: #75b300; color: white; transform: scale(1.05); }

        .btn-general-blue {
             background-color: #8FADFA; border: none; color: white; font-weight: 600;
             transition: transform 0.2s;
        }
        .btn-general-blue:hover { background-color: #6c94f7; color: white; transform: scale(1.05); }

    </style>

    <div class="container pb-5">
        
        <h2 class="text-center mb-5 mt-4 fw-bold"><i class="fas fa-exchange-alt me-2"></i>Movimientos Comerciales</h2>

        <div class="row justify-content-center">
            
            <div class="col-md-5 mb-4">
                <div class="card card-custom h-100">
                    <div class="card-header header-gradient-bg text-center">
                        <h4 class="mb-0 fw-bold"><i class="fas fa-shopping-bag me-2"></i>Compras</h4>
                    </div>
                    <div class="card-body text-center p-5">
                        <i class="fas fa-truck-loading fa-4x mb-4" style="color: #8FADFA;"></i>
                        <p class="card-text text-muted mb-4">Registre el ingreso de nueva mercadería y actualice el stock de sus proveedores.</p>
                        
                        <asp:Button ID="btnNuevaCompra" runat="server" 
                            CssClass="btn btn-general-blue btn-lg w-100 shadow py-3" 
                            Text="Registrar Compra" />
                    </div>
                </div>
            </div>

            <div class="col-md-5 mb-4">
                <div class="card card-custom h-100">
                    <div class="card-header header-gradient-bg text-center">
                        <h4 class="mb-0 fw-bold"><i class="fas fa-cash-register me-2"></i>Ventas</h4>
                    </div>
                    <div class="card-body text-center p-5">
                        <i class="fas fa-shopping-cart fa-4x mb-4" style="color: #8BD100;"></i>
                        <p class="card-text text-muted mb-4">Inicie una nueva venta, seleccione productos y genere el comprobante para el cliente.</p>
                        
                        <asp:Button ID="btnNuevaVenta" runat="server" 
                            CssClass="btn btn-action-green btn-lg w-100 shadow py-3" 
                            Text="Registrar Venta" 
                            OnClick="btnNuevaVenta_Click" />
                    </div>
                </div>
            </div>

        </div>
    </div>

</asp:Content>