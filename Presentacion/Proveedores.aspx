<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="Proveedores.aspx.cs" Inherits="Presentacion.Proveedores" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container mt-4">
        <div class="d-flex justify-content-between align-items-center mb-3">
            <h2 class="mb-0">Gestión de Proveedores</h2>
            <button type="button" class="btn btn-success" data-bs-toggle="modal" data-bs-target="#modalNuevoProveedor">
                Nuevo Proveedor
            </button>
        </div>

        <div class="table-responsive shadow-sm rounded mt-5">
            <asp:GridView ID="gvProveedor" 
                runat="server" 
                CssClass="table table-striped table-hover align-middle"
                AutoGenerateColumns="False" 
                DataKeyNames="Id"
                GridLines="None">

                <Columns>
                    <asp:BoundField DataField="Id" HeaderText="ID" />
                    <asp:BoundField DataField="Nombre" HeaderText="Nombre" />
                    <asp:BoundField DataField="Email" HeaderText="Email" />
                    <asp:BoundField DataField="Telefono" HeaderText="Teléfono" />
                    <asp:BoundField DataField="Direccion" HeaderText="Dirección" />
                    <asp:BoundField DataField="CUIT" HeaderText="CUIT" />
                    <asp:TemplateField HeaderText="Acciones">
                        <ItemTemplate>
                            <div class="d-flex gap-2">
                                <!-- Botón Editar -->
                                <button type="button" 
                                        class="btn btn-warning btn-sm" 
                                        data-bs-toggle="modal" 
                                        data-bs-target="#modalEditarProveedor">
                                    ✏️ Editar
                                </button>

                                <!-- Botón Eliminar -->
                                <button type="button" 
                                        class="btn btn-danger btn-sm" 
                                        data-bs-toggle="modal" 
                                        data-bs-target="#modalEliminarProveedor">
                                    🗑️ Eliminar
                                </button>
                            </div>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
    </div>

    <!-- 🟢 Modal Nuevo Proveedor -->
    <div class="modal fade" id="modalNuevoProveedor" tabindex="-1" aria-labelledby="modalNuevoProveedorLabel" aria-hidden="true">
        <div class="modal-dialog modal-lg modal-dialog-centered">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="modalNuevoProveedorLabel">➕ Nuevo Proveedor</h5>
                    <button type="button" class="btn-close btn-close-white" data-bs-dismiss="modal" aria-label="Cerrar"></button>
                </div>
                <div class="modal-body">
                    <form id="formNuevoProveedor">
                        <div class="row g-3">
                            <div class="col-md-6">
                                <label for="txtNombre" class="form-label">Nombre</label>
                                <input type="text" class="form-control" id="txtNombre" placeholder="Ej: Proveedor S.A.">
                            </div>
                            <div class="col-md-6">
                                <label for="txtEmail" class="form-label">Email</label>
                                <input type="email" class="form-control" id="txtEmail" placeholder="proveedor@mail.com">
                            </div>
                            <div class="col-md-6">
                                <label for="txtTelefono" class="form-label">Teléfono</label>
                                <input type="text" class="form-control" id="txtTelefono" placeholder="Ej: +54 11 1234-5678">
                            </div>
                            <div class="col-md-6">
                                <label for="txtDireccion" class="form-label">Dirección</label>
                                <input type="text" class="form-control" id="txtDireccion" placeholder="Calle Falsa 123, Buenos Aires">
                            </div>
                            <div class="col-md-6">
                                <label for="txtCUIT" class="form-label">CUIT</label>
                                <input type="text" class="form-control" id="txtCUIT" placeholder="20-12345678-9">
                            </div>
                        </div>
                    </form>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cerrar</button>
                    <button type="button" class="btn btn-success">Guardar Proveedor</button>
                </div>
            </div>
        </div>
    </div>

    <!-- ✏️ Modal Editar Proveedor -->
    <div class="modal fade" id="modalEditarProveedor" tabindex="-1" aria-labelledby="modalEditarProveedorLabel" aria-hidden="true">
        <div class="modal-dialog modal-lg modal-dialog-centered">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="modalEditarProveedorLabel">✏️ Editar Proveedor</h5>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Cerrar"></button>
                </div>
                <div class="modal-body">
                    <form id="formEditarProveedor">
                        <div class="row g-3">
                            <div class="col-md-6">
                                <label for="txtEditNombre" class="form-label">Nombre</label>
                                <input type="text" class="form-control" id="txtEditNombre" value="Nautica Mar S.A.">
                            </div>
                            <div class="col-md-6">
                                <label for="txtEditEmail" class="form-label">Email</label>
                                <input type="email" class="form-control" id="txtEditEmail" value="contacto@nauticamar.com">
                            </div>
                            <div class="col-md-6">
                                <label for="txtEditTelefono" class="form-label">Teléfono</label>
                                <input type="text" class="form-control" id="txtEditTelefono" value="+54 11 5555-1234">
                            </div>
                            <div class="col-md-6">
                                <label for="txtEditDireccion" class="form-label">Dirección</label>
                                <input type="text" class="form-control" id="txtEditDireccion" value="Av. Libertador 1000">
                            </div>
                            <div class="col-md-6">
                                <label for="txtEditCUIT" class="form-label">CUIT</label>
                                <input type="text" class="form-control" id="txtEditCUIT" value="30-12345678-9">
                            </div>
                        </div>
                    </form>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cancelar</button>
                    <button type="button" class="btn btn-warning">Guardar Cambios</button>
                </div>
            </div>
        </div>
    </div>

    <!-- 🗑️ Modal Eliminar Proveedor -->
    <div class="modal fade" id="modalEliminarProveedor" tabindex="-1" aria-labelledby="modalEliminarProveedorLabel" aria-hidden="true">
        <div class="modal-dialog modal-dialog-centered">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="modalEliminarProveedorLabel">🗑️ Eliminar Proveedor</h5>
                    <button type="button" class="btn-close btn-close-white" data-bs-dismiss="modal" aria-label="Cerrar"></button>
                </div>
                <div class="modal-body text-center">
                    <p class="fs-5">¿Estás seguro de que deseas eliminar este proveedor?</p>
                    <p><strong>Nautica Mar S.A.</strong></p>
                    <p class="text-muted mb-0">Esta acción no se puede deshacer.</p>
                </div>
                <div class="modal-footer justify-content-center">
                    <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cancelar</button>
                    <button type="button" class="btn btn-danger">Eliminar</button>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
