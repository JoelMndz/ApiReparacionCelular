using ApiRepracionCelular.Controllers.Interfaces;
using ApiRepracionCelular.Dto.Autenticacion;
using ApiRepracionCelular.Servicios.Autenticacion.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ApiRepracionCelular.Controllers
{
    public class AutenticacionController : ApiControllerBase
    {
        private readonly IAutenticacionService autenticacionService;

        public AutenticacionController(IAutenticacionService autenticacionService)
        {
            this.autenticacionService = autenticacionService;
        }
        [HttpPost]
        public async Task<ActionResult> IniciarSesion(LoginDatosDTO loginDatosDTO)
        {
            try
            {
                var respuesta = await autenticacionService.IniciarSesion(loginDatosDTO);
                return Ok(respuesta);
            }
            catch (Exception e)
            {
                return BadRequest(new { Mensaje = e.Message });
            }
        }
    }
}
