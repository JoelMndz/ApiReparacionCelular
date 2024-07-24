using ApiRepracionCelular.Dto.Autenticacion;
using ApiRepracionCelular.Entidades;
using ApiRepracionCelular.Servicios.Autenticacion.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ApiRepracionCelular.Servicios.Autenticacion
{
    public class AutenticacionService:IAutenticacionService
    {
        private readonly ContextoDB contexto;

        public AutenticacionService(ContextoDB contexto)
        {
            this.contexto = contexto;
        }

        public async Task<LoginRespuestaDTO> IniciarSesion(LoginDatosDTO datos)
        {
            //TODO: Probar Conexion
            //var lista = await this.contexto.Usuario.ToListAsync();
            //Automaper
            return new LoginRespuestaDTO(1, "TEST", "TEST");
        }
    }
}
