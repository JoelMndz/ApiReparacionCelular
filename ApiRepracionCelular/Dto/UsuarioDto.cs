namespace ApiRepracionCelular.Dto
{
    public record CrearUsuarioDto(string Nombre, string Telefono, string Email, int IdRol);
    public record ActualizarUsuarioDto(int Id, string Nombre, string Telefono, string Email, int IdRol);
    public class UsuarioDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Telefono { get; set; } = null!;
        public string Email { get; set; } = string.Empty;
        public int IdRol { get; set; }
        public string Rol { get; set; } = string.Empty;
    };

}
