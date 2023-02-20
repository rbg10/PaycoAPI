using Business.IRepositorios;
using Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace GestorEscolar_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[ApiController]
    //[Route("[controller]")]
    public class CuentasController : ControllerBase
    {

        private readonly ICuentas _cuentas;

        public CuentasController( ICuentas cuentas)
        {

            _cuentas= cuentas;
        }

        [HttpPost("login")]
        public async Task<ActionResult<Respuesta>> Login([FromBody]Login data)
        {
            try
            {
                var resultado = await _cuentas.Login(data.Correo, data.Contrasena);

                if (resultado != null)
                {
                    return Ok(resultado);
                }
                else
                {
                    return new OkObjectResult(resultado);
                }
            }
            catch (Exception ex)
            {

                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }


        [HttpPost("CreaUsuario")]
        public async Task<ActionResult<Respuesta>> CreaUsuario([FromBody] Usuario item)
        {
            try
            {
                var resultado = await _cuentas.AltaUsuario(item);

                if (resultado.Estado)
                {
                    return Ok(resultado);
                }
                else
                {
                    return new OkObjectResult(resultado);
                }
            }
            catch (Exception ex)
            {

                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpPost("ActivaCuenta/{correo}")]
        public async Task<ActionResult<Respuesta>> ActivaCuenta(string correo)
        {
            try
            {
                var resultado = await _cuentas.TokenActivacionCuenta(correo);

                if (resultado!=null)
                {
                    return Ok(resultado);
                }
                else
                {
                    return new OkObjectResult(resultado);
                }
            }
            catch (Exception ex)
            {

                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpPost("ValidaToken/{token}")]
        public async Task<ActionResult<Respuesta>> ValidaToken(string token)
        {
            try
            {
                var resultado = await _cuentas.ValidaToken(token);

                if (resultado != null)
                {
                    return Ok(resultado);
                }
                else
                {
                    return new OkObjectResult(resultado);
                }
            }
            catch (Exception ex)
            {

                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpPost("AsignaContrasena/{loginID}/{contrasena}")]
        public async Task<ActionResult<Respuesta>> AsignaContrasena(Guid loginID, string contrasena)
        {
            try
            {
                var resultado = await _cuentas.AsignaContrasena(loginID, contrasena);

                if (resultado != null)
                {
                    return Ok(resultado);
                }
                else
                {
                    return new OkObjectResult(resultado);
                }
            }
            catch (Exception ex)
            {

                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }


    }
}
