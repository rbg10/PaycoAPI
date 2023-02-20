using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public interface ICuentasRepositorio
    {
        Task<Respuesta> AltaUsuario(Usuario item);
        Task<Activacionlogin> TokenActivacionCuenta(string correo);
        Task<Respuesta> ValidaToken(DateTime token);
        Task<Respuesta> ActualizaEstatusTokenEnviado(Guid loginID);
        Task<Respuesta> AsignaContrasena(Guid loginID, string contrasena);
        Task<PerfilUsuario> Login(string correo, string contrasena);

        Task<List<Roles>> ObtieneRolesUsuario(Guid usuarioID);
    }
}
