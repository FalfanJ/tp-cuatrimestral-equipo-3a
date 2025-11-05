<%@ Page Title="Inicio" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Presentacion.Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Panel Principal</h2>
    <p>Bienvenido al sistema de gestión de MiNegocio.</p>

    <div class="row mt-3">
        <div class="col-md-4">
            <a href="Clientes.aspx" class="btn btn-primary w-100 mb-2">Administrar Clientes</a>
            <a href="Productos.aspx" class="btn btn-primary w-100 mb-2">Administrar Productos</a>
            <a href="ComprasVentas.aspx" class="btn btn-primary w-100 mb-2">Compras / Ventas</a>
        </div>
    </div>
</asp:Content>