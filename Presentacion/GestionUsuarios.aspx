<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="GestionUsuarios.aspx.cs" Inherits="Presentacion.GestionUsuarios" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Gestión de Usuarios</h2>
    <asp:Button ID="btnNuevoUsuario" runat="server" CssClass="btn btn-success mb-3" Text="Nuevo Usuario" />

    <asp:GridView ID="gvUsuarios" runat="server" CssClass="table table-striped" AutoGenerateColumns="False">
        <Columns>
            <asp:BoundField DataField="Id" HeaderText="ID" />
            <asp:BoundField DataField="Nombre" HeaderText="Nombre" />
            <asp:BoundField DataField="Email" HeaderText="Email" />
            <asp:ButtonField Text="Editar" CommandName="Editar" />
        </Columns>
    </asp:GridView>
</asp:Content>
