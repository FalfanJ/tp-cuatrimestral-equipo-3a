<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SeleccionCliente.aspx.cs" Inherits="Presentacion.SeleccionCliente" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .card-custom {
            border: none;
            border-radius: 15px;
            box-shadow: 0 8px 20px rgba(0,0,0,0.25);
            background-color: #ffffff;
            overflow: hidden;
        }

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

        .header-yellow-bg {
            background-color: #E1A904;
            color: white;
            padding: 15px 20px;
        }
    </style>
    <script type="text/javascript">
        function openModal() {
            var myModalEl = document.getElementById('modalConfirmacion');
            var modal = bootstrap.Modal.getInstance(myModalEl); // Returns a Bootstrap modal instance
            if (!modal) {
                modal = new bootstrap.Modal(myModalEl);
            }
            modal.show();
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div class="container pb-5">
        <div class="row mb-4 mt-4 align-items-center">
            <div class="col-md-6">
                <h2 class="fw-bold"><i class="fas fa-users me-2"></i>Seleccion Cliente</h2>
            </div>
            <div class="col-md-6 text-md-end">
                <asp:Button Text="Cencelar Venta" runat="server" CssClass="btn btn-secondary btn-dark" ID="btnCancelarVenta" OnClick="btnCancelarVenta_Click" />
            </div>
        </div>

        <div class="card card-custom shadow">
            <div class="card-body p-0">
                <div class="table-responsive">
                    <asp:GridView
                        runat="server"
                        ID="gvCliente"
                        CssClass="table table-hover table-striped align-middle mb-0"
                        AutoGenerateColumns="false"
                        OnSelectedIndexChanged="gvCliente_SelectedIndexChanged"
                        DataKeyNames="IdCliente">

                        <HeaderStyle BackColor="White" ForeColor="#1A0047" BorderStyle="None" Height="50px" Font-Bold="True" />
                        <Columns>
                            <asp:CommandField ShowSelectButton="true" SelectText="Selecionar" HeaderText="Accion" />
                            <asp:BoundField DataField="Nombre" HeaderText="Nombre" />
                            <asp:BoundField DataField="Apellido" HeaderText="Apellido" />
                            <asp:BoundField DataField="Dni" HeaderText="DNI" />
                            <asp:BoundField DataField="Cuit" HeaderText="CUIT" />
                            <asp:BoundField DataField="Telefono" HeaderText="Telefono" />
                            <asp:BoundField DataField="Email" HeaderText="Email" />
                            <asp:BoundField DataField="Direccion" HeaderText="Direccion" />
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
        </div>
    </div>

    <%---- Modal Confirmacion ----%>
    <div class="modal fade" id="modalConfirmacion" tabindex="-1" aria-labelledby="modalConfirmacionLabel" aria-hidden="true">
        <div class="modal-dialog modal-dialog-centered">
            <div class="modal-content">
                <div class="modal-body text-center py-4">
                    <p class="fs-5">¿Estás seguro de su seleccion?</p>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">NO</button>
                    <asp:Button Text="SI" runat="server" ID="btnConfirmacion" OnClick="btnConfirmacion_Click" CssClass="btn btn-primary" />
                </div>
            </div>
        </div>
    </div>

    <%---- Modal Sin Clientes ----%>
    <div class="modal fade" id="modalClienteBD" tabindex="-1" data-bs-backdrop="static" data-bs-keyboard="false">
        <div class="modal-dialog modal-dialog-centered">
            <div class="modal-content card-custom">
                <div class="modal-header header-yellow-bg">
                    <h5 class="modal-title fw-bold">Sin Clientes</h5>
                </div>
                <div class="modal-body text-center py-4">
                    <p class="fs-5">Sin clientes en la base de datos</p>
                </div>
                <div class="modal-footer bg-light justify-content-center">
                    <asp:Button Text="Inicio" runat="server" CssClass="btn btn-primary" ID="btnValidacionBase" OnClick="btnValidacionBase_Click" />
                </div>
            </div>
        </div>
    </div>

    <%---- Modal Sin Productos ----%>
    <div class="modal fade" id="modalProductoBD" tabindex="-1" data-bs-backdrop="static" data-bs-keyboard="false">
        <div class="modal-dialog modal-dialog-centered">
            <div class="modal-content card-custom">
                <div class="modal-header header-yellow-bg">
                    <h5 class="modal-title fw-bold">Sin Productos</h5>
                </div>
                <div class="modal-body text-center py-4">
                    <p class="fs-5">Sin prodcutos en la base de datos</p>
                </div>
                <div class="modal-footer bg-light justify-content-center">
                    <asp:Button Text="Inicio" runat="server" CssClass="btn btn-primary" ID="btnValidacionBase2" OnClick="btnValidacionBase_Click" />
                </div>
            </div>
        </div>
    </div>
</asp:Content>
