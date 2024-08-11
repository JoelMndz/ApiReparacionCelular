using ApiRepracionCelular.Controllers.Interfaces;
using ApiRepracionCelular.Dto;
using ApiRepracionCelular.Servicios.Reparacion.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ApiRepracionCelular.Controllers
{
    [Authorize]
    public class ReparacionController : ApiControllerBase
    {
        private readonly IReparacionService reparacionService;

        public ReparacionController(IReparacionService reparacionService)
        {
            this.reparacionService = reparacionService;
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerReparaciones()
        {
            var reparaciones = await reparacionService.ObtenerReparaciones();
            return Ok(reparaciones);
        }

        [HttpPost]
        public async Task<IActionResult> AgregarReparacion(CrearReparacionDto dto)
        {
            var reparacion = await reparacionService.CrearReparacion(dto);
            return Ok(reparacion);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> FinalizarReparacion(int id)
        {
            var reparacion = await reparacionService.FinalizarReparacion(id);
            return Ok(reparacion);
        }
    }
}
