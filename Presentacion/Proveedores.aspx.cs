using System;
using System.Collections.Generic;

namespace Presentacion
{
    public partial class Proveedores : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarProveedores();
            }
        }

        private void CargarProveedores()
        {
            var proveedores = new List<dynamic>
            {
                new { Id = 1, Nombre = "Ferretería Industrial S.R.L.", Email = "ventas@ferreind.com", Telefono = "+54 11 4789-2233", Direccion = "Av. San Martín 4520, CABA", CUIT = "30-65781234-9" },
                new { Id = 2, Nombre = "Hierros y Tornillos del Sur", Email = "contacto@hytsur.com", Telefono = "+54 11 4665-7788", Direccion = "Calle Mitre 2345, Lomas de Zamora", CUIT = "33-78214569-1" },
                new { Id = 3, Nombre = "Distribuidora FerreMax", Email = "info@ferremax.com.ar", Telefono = "+54 11 4554-9933", Direccion = "Av. Rivadavia 15600, Morón", CUIT = "27-45981236-4" },
                new { Id = 4, Nombre = "Tornillería Delta", Email = "ventas@tornilleriadelta.com", Telefono = "+54 11 4899-5566", Direccion = "Camino General Belgrano 1023, Quilmes", CUIT = "30-99321456-7" },
                new { Id = 5, Nombre = "Materiales San José", Email = "msj@ferresanjose.com", Telefono = "+54 11 4766-4421", Direccion = "Ruta 8 Km 34, San Miguel", CUIT = "33-44561234-8" }
            };

            gvProveedor.DataSource = proveedores;
            gvProveedor.DataBind();
        }
    }
}
