using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class PerfilUsuario : TokenSesion
    {
        public Guid Usuario_ID { get; set; }
        public string Nombres { get; set; }
        public string Paterno { get; set; }
        public string Materno { get; set; }
        public string Correo { get; set; }
        public DateTime FechaAlta { get; set; }
        public List<Roles> Roles { get; set; } = new List<Roles>();
        public List<Cliente> Clientes { get; set; } = new List<Cliente>();
    }
}
