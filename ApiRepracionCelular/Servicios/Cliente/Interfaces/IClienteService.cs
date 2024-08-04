using ApiRepracionCelular.Dto;

namespace ApiRepracionCelular.Servicios.Cliente.Interfaces
{
    public interface IClienteService
    {
        Task<IReadOnlyCollection<ClienteDto>> ObtenerClientes();
        Task<ClienteDto> CrearCliente(CrearClienteDto cliente);
        Task<ClienteDto> ActualizarCliente(ClienteDto cliente);
        Task<ClienteDto> EliminarCliente(int id);
    }
}
