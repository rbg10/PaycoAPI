using Business.IRepositorios;
using Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace GestorEscolar_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CatalogosController : ControllerBase
    {
        private readonly ICatalogos _catalogos;

        public CatalogosController(ICatalogos catalogos)
        {
            _catalogos= catalogos;
        }


        [HttpGet("ObtieneRoles")]
        public async Task<ActionResult<List<Roles>>> Get()
        {
            try
            {
                var resultado= await _catalogos.ObtieneRoles();
                return Ok(resultado);
            }
            catch (Exception ex)
            {

                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);

            }
        }
    }
}
