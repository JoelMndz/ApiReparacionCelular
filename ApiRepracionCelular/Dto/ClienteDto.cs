namespace ApiRepracionCelular.Dto
{
    public record ClienteDto(int Id, string Nombre, string Email, string Telefono);
    public record CrearClienteDto(string Nombre, string Email, string Telefono);
}
