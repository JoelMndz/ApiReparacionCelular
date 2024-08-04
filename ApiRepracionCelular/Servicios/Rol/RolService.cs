using ApiRepracionCelular.Dto;
using ApiRepracionCelular.Entidades;
using ApiRepracionCelular.Servicios.Rol.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Throw;
using RolDominio = ApiRepracionCelular.Entidades.Rol;

namespace ApiRepracionCelular.Servicios.Rol
{
    public class RolService:IRolService
    {
        private readonly ContextoDB contexto;

        public RolService(ContextoDB contexto)
        {
            this.contexto = contexto;
        }
        private IMapper mapper => new MapperConfiguration(c =>
        {
            c.CreateMap<CrearRolDto, RolDominio>();
            c.CreateMap<RolDominio, RolDto>();

        }).CreateMapper();


        public async Task<IReadOnlyCollection<RolDto>> ObtenerRoles()
        {
            var roles = await contexto.Rol
                .AsNoTracking()
                .ProjectTo<RolDto>(mapper.ConfigurationProvider)
                .ToListAsync();
            return roles;
        }

        public async Task<RolDto> CrearRol(CrearRolDto dto)
        {
            var rol = mapper.Map<RolDominio>(dto);
            contexto.Rol.Add(rol);
            await contexto.SaveChangesAsync();
            return mapper.Map<RolDto>(rol);
        }

        public async Task<RolDto> ActualizarRol(RolDto dto)
        {
            var rol = await contexto.Rol.FindAsync(dto.Id);
            rol.ThrowIfNull(_ => new Exception("El rol no existe"));
            rol.Nombre = dto.Nombre;
            contexto.Rol.Update(rol);
            await contexto.SaveChangesAsync();
            return mapper.Map<RolDto>(rol);
        }


        public async Task<RolDto> EliminarRol(int id)
        {
            var rol = await contexto.Rol.FindAsync(id);
            rol.ThrowIfNull(_ => new Exception("El rol no existe"));
            contexto.Rol.Remove(rol);
            await contexto.SaveChangesAsync();
            return mapper.Map<RolDto>(rol);
        }
    }
}
