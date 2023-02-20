using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class ClientesContacto
    {
        public Guid ClienteContacto_ID { get; set; }
        public Guid Cliente_ID { get; set; }
        public string Direccion { get; set; }
        public string Contacto { get; set; }
        public string Telefono1 { get; set; }
        public string Telefono2 { get; set; }
        public string Correo { get; set; }

    }
}
