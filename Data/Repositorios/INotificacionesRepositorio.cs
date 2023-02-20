using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositorios
{
    public interface INotificacionesRepositorio
    {
        Task<PlantillasCorreos> ObtienePlantillaCorreo(string tipo);
        Task<ConfiguracionCorreo> ObtieneDatosCorreo();
        Task<DatosNotificacion> ObtieneDatosNotificacion(Guid loginID);


    }
}
