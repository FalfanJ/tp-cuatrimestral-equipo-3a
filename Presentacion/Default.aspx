<%@ Page Title="Inicio" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Presentacion.Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        /* Fondo azul oscuro con degradado */
        body {
            background: linear-gradient(135deg, #0B1F44, #1E3A8A);
            min-height: 100vh;
            color: #fff;
            font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
        }

        .container {
            padding: 3rem 1rem;
        }

        /* Encabezado */
        h2 {
            font-size: 2.5rem;
            margin-bottom: 0.5rem;
        }

        p.fs-5 {
            color: rgba(255, 255, 255, 0.8);
            font-size: 1.2rem;
        }

        /* Cards */
        .card {
            background: rgba(255, 255, 255, 0.05);
            border-radius: 1rem;
            padding: 2rem;
            transition: transform 0.3s, box-shadow 0.3s;
            min-height: 300px;
            display: flex;
            flex-direction: column;
            justify-content: space-between;
        }

        .card:hover {
            transform: translateY(-10px);
            box-shadow: 0 15px 30px rgba(0,0,0,0.4);
        }

        .card i {
            font-size: 3rem;
            margin-bottom: 1rem;
        }

        .card-title {
            font-size: 1.5rem;
            margin-bottom: 0.5rem;
            color:white;
        }

        .card-text {
            color: rgba(255,255,255,0.8);
            flex-grow: 1;
        }

        a.btn {
            font-weight: bold;
            border-radius: 50px;
            transition: background 0.3s;
        }

        a.btn:hover {
            opacity: 0.9;
        }

        /* Centrar las cards */
        .row {
            justify-content: center;
        }

        /* Pie de página */
        .footer {
            text-align: center;
            margin-top: 3rem;
            color: rgba(255,255,255,0.6);
        }

        @media (max-width: 992px) {
            .card {
                min-height: 280px;
            }
        }

        @media (max-width: 768px) {
            .card {
                min-height: 250px;
            }
        }
    </style>

    <div class="container text-center">
        <!-- Encabezado -->
        <div class="mb-5">
            <h2 class="fw-bold">Ferretería Dos Clavos</h2>
            <p class="fs-5">Bienvenido al sistema de gestión</p>
        </div>

        <!-- Panel de acciones -->
        <div class="row g-4">

            <!-- Card Clientes -->
            <div class="col-md-4 col-sm-6 mb-4">
                <div class="card shadow-sm">
                    <div class="card-body text-center">
                        <i class="bi bi-people-fill text-primary"></i>
                        <h5 class="card-title fw-bold">Clientes</h5>
                        <p class="card-text">Administra la información de tus clientes, historial y contacto.</p>
                        <a href="Clientes.aspx" class="btn btn-primary w-100">Administrar Clientes</a>
                    </div>
                </div>
            </div>

            <!-- Card Productos -->
            <div class="col-md-4 col-sm-6 mb-4">
                <div class="card shadow-sm">
                    <div class="card-body text-center">
                        <i class="bi bi-box-seam text-success"></i>
                        <h5 class="card-title fw-bold">Productos</h5>
                        <p class="card-text">Gestiona los productos, stock, precios y categorías.</p>
                        <a href="Productos.aspx" class="btn btn-success w-100">Administrar Productos</a>
                    </div>
                </div>
            </div>

            <!-- Card Compras/Ventas -->
            <div class="col-md-4 col-sm-6 mb-4">
                <div class="card shadow-sm">
                    <div class="card-body text-center">
                        <i class="bi bi-cash-stack text-warning"></i>
                        <h5 class="card-title fw-bold">Compras / Ventas</h5>
                        <p class="card-text">Registra compras y ventas, controla tus ingresos y stock.</p>
                        <a href="ComprasVentas.aspx" class="btn btn-warning w-100">Ir a Compras / Ventas</a>
                    </div>
                </div>
            </div>

            <!-- Card Proveedores -->
            <div class="col-md-4 col-sm-6 mb-4">
                <div class="card shadow-sm">
                    <div class="card-body text-center">
                        <i class="bi bi-truck text-info"></i>
                        <h5 class="card-title fw-bold">Proveedores</h5>
                        <p class="card-text">Gestiona los proveedores, contactos y compras asociadas.</p>
                        <a href="Proveedores.aspx" class="btn btn-info w-100">Administrar Proveedores</a>
                    </div>
                </div>
            </div>

            <!-- Card Marcas y Categorías -->
            <div class="col-md-4 col-sm-6 mb-4">
                <div class="card shadow-sm">
                    <div class="card-body text-center">
                        <i class="bi bi-tags text-secondary"></i>
                        <h5 class="card-title fw-bold">Marcas y Categorías</h5>
                        <p class="card-text">Gestiona las marcas y categorías de los productos.</p>
                        <a href="MarcasCategorias.aspx" class="btn btn-secondary w-100">Administrar Marcas/Categorías</a>
                    </div>
                </div>
            </div>

            <!-- Card Reportes y Facturación -->
            <div class="col-md-4 col-sm-6 mb-4">
                <div class="card shadow-sm">
                    <div class="card-body text-center">
                        <i class="bi bi-file-earmark-text text-danger"></i>
                        <h5 class="card-title fw-bold">Reportes y Facturación</h5>
                        <p class="card-text">Genera reportes, controla facturación e ingresos.</p>
                        <a href="ReporteFactura" class="btn btn-danger w-100">Ir a Reportes / Facturación</a>
                    </div>
                </div>
            </div>

            <!-- Card Gestión de Usuarios -->
            <div class="col-md-4 col-sm-6 mb-4">
                <div class="card shadow-sm">
                    <div class="card-body text-center">
                        <i class="bi bi-person-badge text-dark"></i>
                        <h5 class="card-title fw-bold">Gestión de Usuarios</h5>
                        <p class="card-text">Administra los usuarios, roles y permisos del sistema.</p>
                        <a href="GestionUsuarios.aspx" class="btn btn-dark w-100">Administrar Usuarios</a>
                    </div>
                </div>
            </div>

        </div>

        <!-- Pie de página -->
        <div class="footer">
            <small>© 2025 Ferretería Dos Clavos</small>
        </div>
    </div>
</asp:Content>

