using Business.IRepositorios;
using Data.Repositorios;
using Entities;
using Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilidades;

namespace Business
{
    public class Notificaciones : INotificaciones
    {
        private readonly INotificacionesRepositorio _notificacionesRepositorio;

        public Notificaciones(INotificacionesRepositorio notificacionesRepositorio)
        {
            _notificacionesRepositorio = notificacionesRepositorio;
        }

        public async Task<string> EnviaNotificacion(string tipo, Guid loginID, string token)
        {
            try
            {
                string respuesta= string.Empty;
                if (loginID == Guid.Empty) { throw new ArgumentException("No se pudo notificar al usuario"); }
                DatosNotificacion _datosNotificacion = new DatosNotificacion();
                ConfiguracionCorreo _configCorreo = new ConfiguracionCorreo();
                PlantillasCorreos _plantilla = new PlantillasCorreos();

                DatosCorreo _datosEnviarCorreo= new DatosCorreo();

                _plantilla = await ObtienePlantillaCorreo(tipo);

                _configCorreo = await ObtieneDatosCorreo();

                _datosNotificacion = await ObtieneDatosNotificacion(loginID);

                if (_datosNotificacion.Correo != null)
                    _datosEnviarCorreo.Para.Add(_datosNotificacion.Correo);
                if (_configCorreo.Correo != null)
                    _datosEnviarCorreo.De.Add( _configCorreo.Correo);

                _datosEnviarCorreo.NombreEmisor = _datosNotificacion.Sistema;
                _datosEnviarCorreo.NombreReceptor = _datosNotificacion.Nombre;
                _datosEnviarCorreo.TituloCorreo = "Activar Cuenta";
                _datosEnviarCorreo.CuerpoCorreo = ConstruyeCuerpoNotificacion(_datosNotificacion, _plantilla.Plantilla,token);


                respuesta = EnvioCorreo.EnviarCorreo(_datosEnviarCorreo, _configCorreo);

                return respuesta;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public Task<ConfiguracionCorreo> ObtieneDatosCorreo()
        {
            try
            {
                return _notificacionesRepositorio.ObtieneDatosCorreo();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public Task<DatosNotificacion> ObtieneDatosNotificacion(Guid loginID)
        {
            try
            {
                return _notificacionesRepositorio.ObtieneDatosNotificacion(loginID);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message); 
            }
        }

        public Task<PlantillasCorreos> ObtienePlantillaCorreo(string tipo)
        {
            try
            {
                return _notificacionesRepositorio.ObtienePlantillaCorreo(tipo);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    
        


        private string ConstruyeCuerpoNotificacion(DatosNotificacion _datos, string plantilla, string token)
        {
            try
            {
                string cuerpoCorreo = plantilla.Replace("*cliente", _datos.Nombre)
                    .Replace("Mercurio", _datos.Sistema)
                    .Replace("*APPURL", _datos.RutaSistema + "/" + token);


                return cuerpoCorreo;

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    
    
    
    
    
    }
}
