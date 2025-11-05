<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="MarcasCategorias.aspx.cs" Inherits="Presentacion.WebForm5" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Administración de Marcas y Categorías</h2>

    <div class="row">
        <div class="col-md-6">
            <h4>Marcas</h4>
            <asp:TextBox ID="txtNuevaMarca" runat="server" CssClass="form-control mb-2" placeholder="Nueva Marca"></asp:TextBox>
            <asp:Button ID="btnAgregarMarca" runat="server" CssClass="btn btn-success mb-3" Text="Agregar" />
            <asp:GridView ID="gvMarcas" runat="server" CssClass="table table-bordered"></asp:GridView>
        </div>

        <div class="col-md-6">
            <h4>Categorías</h4>
            <asp:TextBox ID="txtNuevaCategoria" runat="server" CssClass="form-control mb-2" placeholder="Nueva Categoría"></asp:TextBox>
            <asp:Button ID="btnAgregarCategoria" runat="server" CssClass="btn btn-success mb-3" Text="Agregar" />
            <asp:GridView ID="gvCategorias" runat="server" CssClass="table table-bordered"></asp:GridView>
        </div>
    </div>
</asp:Content>