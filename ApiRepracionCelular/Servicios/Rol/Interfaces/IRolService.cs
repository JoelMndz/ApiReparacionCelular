using ApiRepracionCelular.Dto;

namespace ApiRepracionCelular.Servicios.Rol.Interfaces
{
    public interface IRolService
    {
        Task<IReadOnlyCollection<RolDto>> ObtenerRoles();
        Task<RolDto> CrearRol(CrearRolDto dto);
        Task<RolDto> ActualizarRol(RolDto dto);
        Task<RolDto> EliminarRol(int id);
    }
}
