<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Productos.aspx.cs" Inherits="Presentacion.WebForm2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Gestión de Productos</h2>

    <asp:Button ID="btnNuevoProducto" runat="server" CssClass="btn btn-success mb-3" Text="Nuevo Producto" />

    <asp:GridView ID="gvProductos" runat="server" CssClass="table table-bordered" AutoGenerateColumns="False">
        <Columns>
            <asp:BoundField DataField="Id" HeaderText="ID" />
            <asp:BoundField DataField="Nombre" HeaderText="Producto" />
            <asp:BoundField DataField="Marca" HeaderText="Marca" />
            <asp:BoundField DataField="Stock" HeaderText="Stock" />
            <asp:ButtonField Text="Editar" CommandName="Editar" />
        </Columns>
    </asp:GridView>
</asp:Content>