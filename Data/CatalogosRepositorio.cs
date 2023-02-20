using Data.Repositorios;
using Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace Data
{
    public class CatalogosRepositorio : ICatalogosRepositorio
    {
        private readonly string connection;

        public CatalogosRepositorio(string Connection)
        {
            connection = Connection;
        }

        public IDbConnection _Connection
        {
            get { return new SqlConnection(connection); }
        }
        public async Task<List<Roles>> ObtieneRoles()
        {
            List<Roles> _roles = new ();

            try
            {

                using (IDbConnection conn = _Connection)
                {
                    conn.Open();


                    var resp = await conn.QueryAsync<Roles>("SP_ObtieneCatRoles", param: null, commandType: CommandType.StoredProcedure);

                    if (resp != null)
                    {
                        _roles = (List<Roles>)resp;
                    }

                }
                return _roles;  
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

       
    }
}
