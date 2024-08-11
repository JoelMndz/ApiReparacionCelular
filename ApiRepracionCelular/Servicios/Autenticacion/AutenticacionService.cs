using ApiRepracionCelular.Dto;
using ApiRepracionCelular.Dto.Autenticacion;
using ApiRepracionCelular.Entidades;
using ApiRepracionCelular.Servicios.Autenticacion.Interfaces;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Throw;
using UsuarioDominio = ApiRepracionCelular.Entidades.Usuario;

namespace ApiRepracionCelular.Servicios.Autenticacion
{
    public class AutenticacionService:IAutenticacionService
    {
        private readonly ContextoDB contexto;

        public AutenticacionService(ContextoDB contexto)
        {
            this.contexto = contexto;
        }

        private IMapper mapper => new MapperConfiguration(c =>
        {
            c.CreateMap<UsuarioDominio, UsuarioDto>()
                .ForMember(x => x.Rol, x => x.MapFrom(x => x.IdRolNavigation.Nombre));

        }).CreateMapper();

        public async Task<UsuarioDto> IniciarSesion(LoginDatosDTO datos)
        {
            var usuario = await contexto.Usuario
                .AsNoTracking()
                .Include(x => x.IdRolNavigation)
                .FirstOrDefaultAsync(x => x.Email.ToUpper() == datos.Email.ToUpper());
            usuario.ThrowIfNull(_=> new Exception("Creadenciales incorrectas"));
            usuario.Throw(_ => new Exception("Creadenciales incorrectas")).IfFalse(x => BCrypt.Net.BCrypt.Verify(datos.Password, x.Password));
            return mapper.Map<UsuarioDto>(usuario);
        }
    }
}
