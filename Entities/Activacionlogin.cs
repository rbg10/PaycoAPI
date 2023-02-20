using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Activacionlogin
    {
        public Guid ID { get; set; }
        public Guid Login_ID { get; set; }
        public string Token { get; set; }
        public bool TokenEnviado { get; set; }
        public bool CuentaActivada { get; set; }
        public DateTime FechaActualizacion { get; set; }
    }
}
