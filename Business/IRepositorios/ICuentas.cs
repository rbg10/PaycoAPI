using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.IRepositorios
{
    public interface ICuentas
    {
        Task<Respuesta> AltaUsuario(Usuario item);
        Task<string> TokenActivacionCuenta(string correo);
        Task<Respuesta> ValidaToken(string token);
        Task<Respuesta> ActualizaEstatusTokenEnviado(Guid loginID);
        Task<Respuesta> AsignaContrasena(Guid loginID, string contrasena);
        Task<PerfilUsuario> Login(string correo, string contrasena);
    }
}
