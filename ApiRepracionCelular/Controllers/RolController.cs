using ApiRepracionCelular.Controllers.Interfaces;
using ApiRepracionCelular.Dto;
using ApiRepracionCelular.Servicios.Cliente;
using ApiRepracionCelular.Servicios.Rol.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ApiRepracionCelular.Controllers
{
    public class RolController: ApiControllerBase
    {
        private readonly IRolService rolService;

        public RolController(IRolService rolService)
        {
            this.rolService = rolService;
        }

        [HttpGet]
        public async Task<ActionResult> ObtenerRoles()
        {
            var data = await rolService.ObtenerRoles();
            return Ok(data);
        }

        [HttpPost]
        public async Task<ActionResult> CrearRol(CrearRolDto dto)
        {
            var data = await rolService.CrearRol(dto);
            return Ok(data);
        }

        [HttpPatch]
        public async Task<ActionResult> ActualizarRol(RolDto dto)
        {
            var data = await rolService.ActualizarRol(dto);
            return Ok(data);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> EliminarRol(int id)
        {
            var data = await rolService.EliminarRol(id);
            return Ok(data);
        }
    }
}
