using ApiRepracionCelular.Dto;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ApiRepracionCelular.Servicios.Usuario.Interfaces
{
    public interface IUsuarioService
    {
        Task<IReadOnlyCollection<UsuarioDto>> ObtenerUsuarios();
        Task<UsuarioDto> CrearUsuario(CrearUsuarioDto dto);
        Task<UsuarioDto> ActualizarUsuario(ActualizarUsuarioDto dto);
        Task<UsuarioDto> EliminarUsuario(int id);

    }
}
