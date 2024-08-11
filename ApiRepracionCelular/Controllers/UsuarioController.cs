using ApiRepracionCelular.Controllers.Interfaces;
using ApiRepracionCelular.Dto;
using ApiRepracionCelular.Servicios.Usuario.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ApiRepracionCelular.Controllers
{
    [Authorize(Roles = "Administrador")]
    public class UsuarioController : ApiControllerBase
    {
        private readonly IUsuarioService usuarioService;

        public UsuarioController(IUsuarioService usuarioService)
        {
            this.usuarioService = usuarioService;
        }

        [HttpGet]
        public async Task<ActionResult> ObtenerUsuarios()
        {
            var usuarios = await usuarioService.ObtenerUsuarios();
            return Ok(usuarios);
        }

        [HttpPost]
        public async Task<ActionResult> CrearUsuario(CrearUsuarioDto dto)
        {
            var usuario = await usuarioService.CrearUsuario(dto);
            return Ok(usuario);
        }

        [HttpPatch]
        public async Task<ActionResult> ActualizarUsuario(ActualizarUsuarioDto dto)
        {
            var usuario = await usuarioService.ActualizarUsuario(dto);
            return Ok(usuario);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> EliminarUsuario(int id)
        {
            var usuario = await usuarioService.EliminarUsuario(id);
            return Ok(usuario);
        }
    }
}
