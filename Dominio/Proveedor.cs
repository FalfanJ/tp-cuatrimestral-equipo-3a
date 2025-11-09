using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Proveedor : Persona
    {
        public Int64 IdProveedor { get; set; }
        public string RazonSocial { get; set; }
    }
}
