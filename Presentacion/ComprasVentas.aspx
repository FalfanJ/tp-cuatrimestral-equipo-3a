<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ComprasVentas.aspx.cs" Inherits="Presentacion.WebForm3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Registrar Compra / Venta</h2>

    <div class="row">
        <div class="col-md-6">
            <asp:Button ID="btnNuevaCompra" runat="server" CssClass="btn btn-primary w-100 mb-3" Text="Registrar Compra" />
        </div>
        <div class="col-md-6">
            <asp:Button ID="btnNuevaVenta" runat="server" CssClass="btn btn-success w-100 mb-3" Text="Registrar Venta" OnClick="btnNuevaVenta_Click" />
        </div>
    </div>
</asp:Content>