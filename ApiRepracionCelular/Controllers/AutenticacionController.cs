using ApiRepracionCelular.Controllers.Interfaces;
using ApiRepracionCelular.Dto;
using ApiRepracionCelular.Dto.Autenticacion;
using ApiRepracionCelular.Servicios.Autenticacion.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ApiRepracionCelular.Controllers
{
    public class AutenticacionController : ApiControllerBase
    {
        private readonly IAutenticacionService autenticacionService;
        private readonly IConfiguration configuracion;

        public AutenticacionController(IAutenticacionService autenticacionService, IConfiguration configuracion)
        {
            this.autenticacionService = autenticacionService;
            this.configuracion = configuracion;
        }

        [HttpPost("login")]
        public async Task<ActionResult> IniciarSesion(LoginDatosDTO loginDatosDTO)
        {
            try
            {
                var usuario = await autenticacionService.IniciarSesion(loginDatosDTO);
                return Ok(new {usuario, jwt = GenerarToken(usuario)});
            }
            catch (Exception e)
            {
                return BadRequest(new { Mensaje = e.Message });
            }
        }

        private string GenerarToken(UsuarioDto usuario)
        {
            var claims = new[]
            {
                new Claim("id", usuario.Id.ToString()),
                new Claim(ClaimTypes.Role, usuario.Rol),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuracion["SECRETO"]!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expiration = DateTime.UtcNow.AddDays(7);

            JwtSecurityToken token = new JwtSecurityToken(
               issuer: null,
               audience: null,
               claims: claims,
               expires: expiration,
               signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
