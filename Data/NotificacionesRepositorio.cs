using Dapper;
using Data.Repositorios;
using Entities;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Data
{
    public class NotificacionesRepositorio : INotificacionesRepositorio
    {
        private readonly string connection;

        public NotificacionesRepositorio(string Connection)
        {
            connection = Connection;
        }

        public IDbConnection _Connection
        {
            get { return new SqlConnection(connection); }
        }

        public async Task<ConfiguracionCorreo> ObtieneDatosCorreo()
        {
            ConfiguracionCorreo _datos = new ConfiguracionCorreo();
            try
            {
                using (IDbConnection conn = _Connection)
                {
                    conn.Open();
                    _datos = await conn.QueryFirstOrDefaultAsync<ConfiguracionCorreo>("SP_ObtieneCorreoConfiguracion", param: null, commandType: CommandType.StoredProcedure);
                    return _datos;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<DatosNotificacion> ObtieneDatosNotificacion(Guid loginID)
        {
            DatosNotificacion _datosNotif = new DatosNotificacion();
            try
            {
                using (IDbConnection conn = _Connection)
                {
                    conn.Open();
                    var parameter = new DynamicParameters();
                    parameter.Add("LoginID", loginID);
                    _datosNotif = await conn.QueryFirstOrDefaultAsync<DatosNotificacion>("SP_ObtieneDatosNotificacion", param: parameter, commandType: CommandType.StoredProcedure);
                    return _datosNotif;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<PlantillasCorreos> ObtienePlantillaCorreo(string tipo)
        {
            PlantillasCorreos _plantilla = new PlantillasCorreos();
            try
            {
                using (IDbConnection conn = _Connection)
                {
                    conn.Open();
                    var parameter = new DynamicParameters();
                    parameter.Add("tipo", tipo);

                    _plantilla = await conn.QueryFirstOrDefaultAsync<PlantillasCorreos>("SP_ObtienePlantilla", param: parameter, commandType: CommandType.StoredProcedure);

                    return _plantilla;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
