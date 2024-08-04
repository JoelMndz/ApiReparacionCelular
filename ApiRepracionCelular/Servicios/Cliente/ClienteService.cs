using ApiRepracionCelular.Dto;
using ClienteDominio = ApiRepracionCelular.Entidades.Cliente;
using AutoMapper;
using ApiRepracionCelular.Servicios.Cliente.Interfaces;
using ApiRepracionCelular.Entidades;
using Microsoft.EntityFrameworkCore;
using AutoMapper.QueryableExtensions;
using Throw;

namespace ApiRepracionCelular.Servicios.Cliente
{
    public class ClienteService:IClienteService
    {
        private readonly ContextoDB contexto;

        public ClienteService(ContextoDB contexto)
        {
            this.contexto = contexto;
        }

        private IMapper mapper => new MapperConfiguration(c =>
        {
            c.CreateMap<CrearClienteDto, ClienteDominio>();
            c.CreateMap<ClienteDominio, ClienteDto>();

        }).CreateMapper();

        public async Task<IReadOnlyCollection<ClienteDto>> ObtenerClientes()
        {
            var clientes = await contexto.Cliente
                .AsNoTracking()
                .ProjectTo<ClienteDto>(mapper.ConfigurationProvider)
                .ToListAsync();
            return clientes;
        }
        public async Task<ClienteDto> CrearCliente(CrearClienteDto dto)
        {
            var cliente = mapper.Map<ClienteDominio>(dto);
            contexto.Cliente.Add(cliente);
            await contexto.SaveChangesAsync();
            return mapper.Map<ClienteDto>(cliente);
        }
        public async Task<ClienteDto> ActualizarCliente(ClienteDto dto)
        {
            var cliente = await contexto.Cliente.FindAsync(dto.Id);
            cliente.ThrowIfNull(_=> new Exception("No existe el cliente"));
            cliente.Nombre = dto.Nombre;
            cliente.Telefono = dto.Telefono;
            cliente.Email = dto.Email;
            contexto.Cliente.Update(cliente);
            await contexto.SaveChangesAsync();
            return mapper.Map<ClienteDto>(cliente);
        }


        public async Task<ClienteDto> EliminarCliente(int id)
        {
            var cliente = await contexto.Cliente.FindAsync(id);
            cliente.ThrowIfNull(_ => new Exception("No existe el cliente"));
            contexto.Cliente.Remove(cliente);
            await contexto.SaveChangesAsync();
            return mapper.Map<ClienteDto>(cliente);
        }

    }
}
