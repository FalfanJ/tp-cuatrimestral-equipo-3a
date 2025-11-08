using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Usuario : Persona
    {
        public int IdUsuario { get; set; }
        public string TipoUsuario { get; set; }
        public string NombreUsuario { get; set; }
        public string Contraseña { get; set; }
    }
}
