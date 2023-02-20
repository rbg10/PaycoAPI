using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class DuracionCarrera
    {
        public Guid Carrera_ID { get; set; }
        public Guid  Periodo_ID  { get; set; }
        public int Duracion { get; set; }
    }
}
