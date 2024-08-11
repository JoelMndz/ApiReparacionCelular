using ApiRepracionCelular.Dto;
using ApiRepracionCelular.Entidades;
using ApiRepracionCelular.Servicios.Usuario.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Throw;
using UsuarioDominio = ApiRepracionCelular.Entidades.Usuario;

namespace ApiRepracionCelular.Servicios.Usuario
{
    public class UsuarioService : IUsuarioService
    {
        private readonly ContextoDB contexto;

        public UsuarioService(ContextoDB contexto)
        {
            this.contexto = contexto;
        }
        private IMapper mapper => new MapperConfiguration(c =>
        {
            c.CreateMap<CrearUsuarioDto, UsuarioDominio>();
            c.CreateMap<UsuarioDominio, UsuarioDto>()
                .ForMember(x => x.Rol, x => x.MapFrom(x => x.IdRolNavigation.Nombre));

        }).CreateMapper();
        public async Task<IReadOnlyCollection<UsuarioDto>> ObtenerUsuarios()
        {
            var usuarios = await contexto.Usuario
                .AsNoTracking()
                .Include(x => x.IdRolNavigation)
                .ProjectTo<UsuarioDto>(mapper.ConfigurationProvider)
                .ToListAsync();
            return usuarios;
        }
        public async Task<UsuarioDto> CrearUsuario(CrearUsuarioDto dto)
        {
            var usuario = mapper.Map<UsuarioDominio>(dto);
            usuario.IdRolNavigation = await contexto.Rol.FindAsync(usuario.IdRol);
            usuario.IdRolNavigation.ThrowIfNull(_=> new Exception("El rol no existe"));
            usuario.Password = BCrypt.Net.BCrypt.HashPassword("12345678");
            contexto.Usuario.Add(usuario);
            await contexto.SaveChangesAsync();
            return mapper.Map<UsuarioDto>(usuario);
        }
        public async Task<UsuarioDto> ActualizarUsuario(ActualizarUsuarioDto dto)
        {
            var usuario = await contexto.Usuario.FindAsync(dto.Id);
            usuario.ThrowIfNull(_=> new Exception("No existe el usuario"));
            usuario.IdRolNavigation = await contexto.Rol.FindAsync(dto.IdRol);
            usuario.IdRolNavigation.ThrowIfNull(_ => new Exception("El rol no existe"));
            usuario.Nombre = dto.Nombre;
            usuario.Telefono = dto.Telefono;
            usuario.Email = dto.Email;
            contexto.Usuario.Update(usuario);
            await contexto.SaveChangesAsync();
            return mapper.Map<UsuarioDto>(usuario);
        }

        public async Task<UsuarioDto> EliminarUsuario(int id)
        {
            var usuario = await contexto.Usuario.FindAsync(id);
            usuario.ThrowIfNull(_ => new Exception("No existe el usuario"));
            contexto.Usuario.Remove(usuario);
            await contexto.SaveChangesAsync();
            return mapper.Map<UsuarioDto>(usuario);
        }

    }
}
