namespace ApiRepracionCelular.Dto
{
    public record UsuarioDto(int Id, string Nombre, string Telefono, string Email, int IdRol, string Rol);
    public record CrearUsuarioDto(string Nombre, string Telefono, string Email, int IdRol);
    public record ActualizarUsuarioDto(int Id, string Nombre, string Telefono, string Email, int IdRol);
}
