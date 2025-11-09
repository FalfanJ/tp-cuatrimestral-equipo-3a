using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Persona
    {
        public Int64 IdPersona { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public Int64 Dni { get; set; }
        public Int64 Cuit { get; set; }
        public bool TipoPersona { get; set; }
        public Int64 Telefono { get; set; }
        public string Email { get; set; }
        public string Direccion { get; set; }
    }
}
