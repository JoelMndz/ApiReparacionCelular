using ApiRepracionCelular.Dto;

namespace ApiRepracionCelular.Servicios.Reparacion.Interfaces
{
    public interface IReparacionService
    {
        Task<IReadOnlyCollection<ReparacionDto>> ObtenerReparaciones();
        Task<ReparacionDto> CrearReparacion(CrearReparacionDto dto);
        Task<ReparacionDto> FinalizarReparacion(int id);
    }
}
