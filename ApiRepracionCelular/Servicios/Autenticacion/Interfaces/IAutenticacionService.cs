using ApiRepracionCelular.Dto;
using ApiRepracionCelular.Dto.Autenticacion;

namespace ApiRepracionCelular.Servicios.Autenticacion.Interfaces
{
    public interface IAutenticacionService
    {
        public Task<UsuarioDto> IniciarSesion(LoginDatosDTO datos);
    }
}
