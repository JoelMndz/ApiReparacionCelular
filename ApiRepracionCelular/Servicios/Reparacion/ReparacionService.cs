using ApiRepracionCelular.Dto;
using ApiRepracionCelular.Entidades;
using ApiRepracionCelular.Servicios.Reparacion.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Throw;
using ReparacionDominio = ApiRepracionCelular.Entidades.Reparacion;

namespace ApiRepracionCelular.Servicios.Reparacion
{
    public class ReparacionService : IReparacionService
    {
        private readonly ContextoDB contexto;

        public ReparacionService(ContextoDB contexto)
        {
            this.contexto = contexto;
        }
        private IMapper mapper => new MapperConfiguration(c =>
        {
            c.CreateMap<ReparacionDominio, ReparacionDto>()
            .ForMember(x => x.Cliente, x => x.MapFrom(x => x.IdClienteNavigation.Nombre))
            .ForMember(x => x.Detalles, x=> x.MapFrom(x => x.ReparacionDetalle));

            c.CreateMap<ReparacionDetalle, ReparacionDetalleDto>();

        }).CreateMapper();
        public async Task<ReparacionDto> CrearReparacion(CrearReparacionDto dto)
        {
            var cliente = await contexto.Cliente.FindAsync(dto.IdCliente);
            cliente.ThrowIfNull(_ => new Exception("No existe el cliente"));
            var reparacion = new ReparacionDominio()
            {
                Estado = "PENDIENTE",
                FechaRegistro = DateTime.Now,
                FechaEntrega = dto.FechaEntrega,
                IdClienteNavigation = cliente,
                IdTecnicoNavigation = await contexto.Usuario.FindAsync(dto.IdTecnico)
            };
            foreach (var detalle in dto.Detalles)
            {
                contexto.ReparacionDetalle.Add(new ReparacionDetalle() { 
                    Descripcion = detalle.Descripcion,
                    Precio = detalle.Precio,
                    IdReparacionNavigation = reparacion
                });
            }
            await contexto.SaveChangesAsync();
            return mapper.Map<ReparacionDto>(reparacion);
        }

        public async Task<ReparacionDto> FinalizarReparacion(int id)
        {
            var reparacion = await this.contexto.Reparacion.FindAsync(id);
            reparacion.ThrowIfNull(_ => new Exception("La reparacion no existe"));
            reparacion.Estado = "TERMINADO";
            contexto.Reparacion.Update(reparacion);
            await contexto.SaveChangesAsync();
            return mapper.Map<ReparacionDto>(reparacion);
        }

        public async Task<IReadOnlyCollection<ReparacionDto>> ObtenerReparaciones()
        {
            return await contexto.Reparacion
                .AsNoTracking()
                .Include(x => x.IdClienteNavigation)
                .Include(x => x.ReparacionDetalle)
                .OrderByDescending(x => x.FechaRegistro)
                .ProjectTo<ReparacionDto>(mapper.ConfigurationProvider)
                .ToListAsync();
        }
    }
}
