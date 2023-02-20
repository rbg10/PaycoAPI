using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.IRepositorios
{
    public interface INotificaciones
    {
        Task<PlantillasCorreos> ObtienePlantillaCorreo(string tipo);
        Task<ConfiguracionCorreo> ObtieneDatosCorreo();
        Task<DatosNotificacion> ObtieneDatosNotificacion(Guid loginID);

        Task<string> EnviaNotificacion (string tipo, Guid loginID, string token); 


        
    }
}
