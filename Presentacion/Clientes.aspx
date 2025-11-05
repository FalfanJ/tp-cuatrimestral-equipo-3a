<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Clientes.aspx.cs" Inherits="Presentacion.WebForm1" %>



<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Gestión de Clientes</h2>
    <asp:Button ID="btnNuevoCliente" runat="server" CssClass="btn btn-success mb-3" Text="Nuevo Cliente" />

    <asp:GridView ID="gvClientes" runat="server" CssClass="table table-striped" AutoGenerateColumns="False">
        <Columns>
            <asp:BoundField DataField="Id" HeaderText="ID" />
            <asp:BoundField DataField="Nombre" HeaderText="Nombre" />
            <asp:BoundField DataField="Email" HeaderText="Email" />
            <asp:ButtonField Text="Editar" CommandName="Editar" />
        </Columns>
    </asp:GridView>
</asp:Content>