<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Presentacion.Login" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml" lang="es">
<head runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <title>Iniciar sesión | Comercio</title>


    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/css/bootstrap.min.css" rel="stylesheet" />
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/js/bootstrap.bundle.min.js"></script>


    <link href="https://cdn.jsdelivr.net/npm/bootstrap-icons@1.11.1/font/bootstrap-icons.css" rel="stylesheet" />

    <style>
        body {
            background: linear-gradient(135deg, #007bff, #6610f2);
            height: 100vh;
            display: flex;
            justify-content: center;
            align-items: center;
            font-family: 'Segoe UI', sans-serif;
        }

        .login-card {
            background: white;
            border-radius: 15px;
            box-shadow: 0 8px 30px rgba(0, 0, 0, 0.2);
            width: 100%;
            max-width: 400px;
            min-width:500px;
            padding: 2em;
            animation: fadeIn 0.6s ease-in-out;
        }

        .login-card h3 {
            text-align: center;
            margin-bottom: 1.5rem;
            color: #343a40;
            font-weight: 700;
        }

        .form-control {
            border-radius: 10px;
        }

        .btn-login {
            background: linear-gradient(90deg, #007bff, #6610f2);
            border: none;
            color: white;
            border-radius: 10px;
            transition: all 0.3s ease;
        }

        .btn-login:hover {
            transform: scale(1.03);
            background: linear-gradient(90deg, #0056b3, #520dc2);
            color:white;
        }

        @keyframes fadeIn {
            from {  transform: translateY(-20px); }
            to { transform: translateY(0); }
        }

        .text-muted {
            font-size: 0.9rem;
            display: block;
            text-align: center;
            margin-top: 1rem;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="login-card">
            <h3><i class="bi bi-lock-fill me-2"></i>Iniciar Sesión</h3>

            <div class="mb-3">
                <label for="txtEmail" class="form-label">Correo electrónico</label>
                <div class="input-group">
                    <span class="input-group-text"><i class="bi bi-envelope-fill"></i></span>
                    <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" placeholder="perfiladmin@comercio.com"></asp:TextBox>
                </div>
            </div>

            <div class="mb-3">
                <label for="txtPassword" class="form-label">Contraseña</label>
                <div class="input-group">
                    <span class="input-group-text"><i class="bi bi-key-fill"></i></span>
                    <asp:TextBox ID="txtPassword" runat="server" CssClass="form-control" TextMode="Password" placeholder="********"></asp:TextBox>
                </div>
            </div>

            <asp:Button ID="btnLogin" runat="server" Text="Ingresar" CssClass="btn btn-login w-100 mt-3" OnClick="btnLogin_Click" />

            <asp:Label ID="lblMensaje" runat="server" CssClass="text-danger mt-3 d-block text-center"></asp:Label>

            <span class="text-muted">© 2025 Comercio</span>
        </div>
    </form>
</body>
</html>
