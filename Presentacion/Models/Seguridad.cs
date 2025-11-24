using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Presentacion.Models
{
    public static class Seguridad
    {
        public static bool EsVendedor(dynamic usuario)
        {
            return usuario != null && usuario.TipoUsuario == "Vendedor";
        }
    }
}