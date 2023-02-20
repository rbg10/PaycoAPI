using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Planteles 
    { 
        public Guid Plantel_ID { get; set; } 
        public string Nombre { get; set; }
        public string CentroEscolar { get; set; }
        public bool Activo { get; set; }

    }
}
