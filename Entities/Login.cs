using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Login
    {
        public Guid Login_ID { get; set; }
        public Guid Usuario_ID { get; set; }
        public string Correo { get; set; }
        public string Contrasena { get; set; }
        public DateTime FechaContrasena { get; set; }
        public DateTime FechaUltimoAcceso { get; set; }
        public bool Activo { get; set; }

    }
}
