using Business.IRepositorios;
using Data.Repositories;
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
    public class Cuentas : ICuentas
    {
        private readonly ICuentasRepositorio _cuentasRepositorio;
        private readonly INotificaciones _notificaciones;

        public Cuentas(ICuentasRepositorio cuentasRepositorio, INotificaciones notificaciones)
        {
            _cuentasRepositorio = cuentasRepositorio;
            _notificaciones = notificaciones;
        }

        public Task<Respuesta> ActualizaEstatusTokenEnviado(Guid loginID)
        {
            return _cuentasRepositorio.ActualizaEstatusTokenEnviado(loginID);
        }

        public Task<Respuesta> AltaUsuario(Usuario item)
        {
            return _cuentasRepositorio.AltaUsuario(item);
        }

        public Task<Respuesta> AsignaContrasena(Guid loginID, string contrasena)
        {
            string contrasenaEncriptada =EncriptaDatos.EncriptaCadena(contrasena);
            return _cuentasRepositorio.AsignaContrasena(loginID, contrasenaEncriptada);

        }

        public async Task<PerfilUsuario> Login(string correo, string contrasena)
        {
            PerfilUsuario _perfil = new PerfilUsuario();
            TokenSesion _token = new TokenSesion();
            TokenJWT _GeneraToken = new TokenJWT();

            string contrasenaEncriptada = EncriptaDatos.EncriptaCadena(contrasena);

            _perfil =await _cuentasRepositorio.Login(correo, contrasenaEncriptada);

            if(_perfil.Usuario_ID != Guid.Empty)
            {
                _token = _GeneraToken.GeneraTokenSesion(_perfil, _perfil.Roles);

                _perfil.Token = _token.Token;
                _perfil.FechaAlta = _token.Expiracion;
            }

            return _perfil;
        }

        public async Task<string> TokenActivacionCuenta(string correo)
        {
            try
            {
                Activacionlogin _dataToken  = new();
                string enviaCorreo = string.Empty;
                _dataToken = await _cuentasRepositorio.TokenActivacionCuenta(correo);
                enviaCorreo = await _notificaciones.EnviaNotificacion(TipoNotificacion.Activacion.ToString(), _dataToken.Login_ID, _dataToken.Token);
                if(enviaCorreo.Contains(""))
                    await ActualizaEstatusTokenEnviado(_dataToken.Login_ID);

                return enviaCorreo;
            }
            catch (Exception)
            {

                throw;
            }


        }

        public Task<Respuesta> ValidaToken(string token)
        {
            string[] valores = token.Split('_');
            DateTime fechaToken = Convert.ToDateTime(valores[2]);

            return _cuentasRepositorio.ValidaToken(fechaToken);
            
        }
    }
}
