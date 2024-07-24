using ApiRepracionCelular.Dto.Autenticacion;

namespace ApiRepracionCelular.Servicios.Autenticacion.Interfaces
{
    public interface IAutenticacionService
    {
        public Task<LoginRespuestaDTO> IniciarSesion(LoginDatosDTO datos);
    }
}
