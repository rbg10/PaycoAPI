using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public  class DatosCorreo
    {
        public List<string> De { get; set; } = new List<string>();
        public string NombreEmisor { get; set; }
        public List<string> Para { get; set; } = new List<string>();
        public string NombreReceptor { get; set; }
        public List<string> CopiaA { get; set; } = new List<string>();
        public string TituloCorreo { get; set; }
        public string CuerpoCorreo { get; set; }


    }
}
