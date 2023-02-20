using Dapper;
using Data.Repositories;
using Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilidades;

namespace Data
{
    public class CuentasRespositorio : ICuentasRepositorio
    {
        private readonly string connection;

        public CuentasRespositorio (string Connection)
        {
            connection= Connection;
        }

        public IDbConnection _Connection 
        { 
            get { return new SqlConnection (connection); } 
        }

        public async Task<Respuesta> ActualizaEstatusTokenEnviado(Guid loginID)
        {
            Respuesta _respuesta = new Respuesta();
            try
            {
                using (IDbConnection conn = _Connection)
                {
                    conn.Open();
                    var parameter = new DynamicParameters();
                    parameter.Add("loginID", loginID);

                    var res = await conn.ExecuteScalarAsync("SP_ActualizaEstatusTokenEnviado", param: parameter, commandType: CommandType.StoredProcedure);
                    if (res != null)
                    {
                        _respuesta.Id = 0;
                        _respuesta.Estado = true;
                        _respuesta.Mensaje = "Actualizado correctamente";
                    }
                    return _respuesta;
                }
            }
            catch (Exception e)
            {
                _respuesta.Id = 0;
                _respuesta.Estado = false;
                _respuesta.Mensaje = e.Message;
                return _respuesta;
            }
        }

        public async Task<Respuesta> AltaUsuario(Usuario item)
        {
            Respuesta _respuesta = new Respuesta();

            try
            {
                TransformaData data= new TransformaData();
                DataTable dtRoles = data.ConvierteRolesTable(item.Roles);
                DataTable dtClientes = data.ConvierteClientesTable(item.Clientes);

                using (IDbConnection conn = _Connection)
                {
                    conn.Open();
                    var parameter = new DynamicParameters();
                    parameter.Add("nombre", item.Nombres);
                    parameter.Add("paterno", item.Paterno);
                    parameter.Add("materno", item.Materno);
                    parameter.Add("usuario", item.UsuarioInserta);
                    parameter.Add("Correo", item.Correo);
                    parameter.Add("roles", dtRoles, DbType.Object);
                    parameter.Add("clientes", dtClientes, DbType.Object);

                    var resp = await conn.ExecuteScalarAsync("SP_CreaUsuarioLogin", param: parameter, commandType: CommandType.StoredProcedure);

                    if(resp != null)
                    {
                        _respuesta.Estado = true;
                        _respuesta.Mensaje=resp.ToString();
                    }
                    
                }
            }
            catch (Exception e)
            {
                _respuesta.Estado = false;
                _respuesta.Mensaje = e.Message;
            }
            return _respuesta;
        }

        public async Task<Respuesta> AsignaContrasena(Guid loginID, string contrasena)
        {
            Respuesta _respuesta = new Respuesta();
            try
            {
                using (IDbConnection conn = _Connection)
                {
                    conn.Open();
                    var parameter = new DynamicParameters();
                    parameter.Add("loginID", loginID);
                    parameter.Add("contrasena", contrasena);

                    var res = await conn.ExecuteAsync("SP_AsignaContrasena", param: parameter, commandType: CommandType.StoredProcedure);

                    _respuesta.Id = 0;
                    _respuesta.Estado = true;
                    _respuesta.Mensaje = "Actualizado correctamente";

                    return _respuesta;
                }
            }
            catch (Exception e)
            {
                _respuesta.Id = 0;
                _respuesta.Estado = false;
                _respuesta.Mensaje = e.Message;
                return _respuesta;
            }
        }

        public async Task<PerfilUsuario> Login(string correo, string contrasena)
        {
            PerfilUsuario _perfil = new PerfilUsuario();
            try
            {
                using (IDbConnection conn = _Connection)
                {
                    conn.Open();
                    var parameter = new DynamicParameters();
                    parameter.Add("correo", correo);
                    parameter.Add("contrasena", contrasena);

                    _perfil = await conn.QueryFirstOrDefaultAsync<PerfilUsuario>("SP_Login", param: parameter, commandType: CommandType.StoredProcedure);

                    if(_perfil.Usuario_ID != Guid.Empty)
                    {
                        _perfil.Roles= await ObtieneRolesUsuario(_perfil.Usuario_ID);
                    }

                    return _perfil;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<List<Roles>> ObtieneRolesUsuario(Guid usuarioID)
        {
            List<Roles> _roles = new List<Roles>();
            try
            {
                using (IDbConnection conn = _Connection)
                {
                    conn.Open();
                    var parameter = new DynamicParameters();
                    parameter.Add("usuarioID", usuarioID);

                    var a = await conn.QueryAsync<Roles>("SP_ObtieneRolesDeUsuario", param: parameter, commandType: CommandType.StoredProcedure);
                    if(a != null)
                    {
                        _roles = (List<Roles>)a;
                    }
                    return _roles;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public async Task<Activacionlogin> TokenActivacionCuenta(string correo)
        {
            Activacionlogin _token = new Activacionlogin();
            try
            {
                using (IDbConnection conn = _Connection)
                {
                    conn.Open();
                    var parameter = new DynamicParameters();
                    parameter.Add("correo", correo);
            
                    _token  = await conn.QueryFirstOrDefaultAsync<Activacionlogin>("SP_TokenActivacionCuenta", param: parameter, commandType: CommandType.StoredProcedure);

                    return _token;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }            
        }

        public async Task<Respuesta> ValidaToken(DateTime fechaToken)
        {
            Respuesta _respuesta= new Respuesta();
            try
            {
                using (IDbConnection conn = _Connection)
                {
                    conn.Open();
                    var parameter = new DynamicParameters();
                    parameter.Add("fechaToken", fechaToken);

                    string _resp = await conn.ExecuteScalarAsync<string>("SP_ValidaToken", param: parameter, commandType: CommandType.StoredProcedure);
                   
                    _respuesta.Estado= true;
                    _respuesta.Mensaje = _resp;

                    return _respuesta;
                }
            }
            catch (Exception e)
            {
                _respuesta.Estado = false;
                _respuesta.Mensaje = e.Message;
                return _respuesta;
            }
        }
    }
}
