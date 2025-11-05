<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ReporteFactura.aspx.cs" Inherits="Presentacion.WebForm4" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Reporte de Factura</h2>
    <p>Vista previa del comprobante de venta.</p>

    <asp:Panel ID="pnlFactura" runat="server" CssClass="border p-3">
        <asp:Label ID="lblNumeroFactura" runat="server" Text="Factura N°: 0001-00000001"></asp:Label>
        <hr />
        <asp:GridView ID="gvDetalleFactura" runat="server" CssClass="table table-sm table-striped">
        </asp:GridView>
    </asp:Panel>

    <asp:Button ID="btnImprimir" runat="server" CssClass="btn btn-primary mt-3" Text="Imprimir" />
</asp:Content>