using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class ConfiguracionCorreo
    {
        public string Host { get; set; }
        public int Puerto{ get; set; }
        public string Correo { get; set; }
        public string Contrasena { get; set; }

    }
}
