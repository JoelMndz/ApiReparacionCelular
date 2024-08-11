using ApiRepracionCelular.Entidades;

namespace ApiRepracionCelular.Dto
{
    public record CrearReparacionDto(DateTime FechaEntrega, int IdCliente, int IdTecnico, CrearReparacionDatalleDto[] Detalles);
    public record CrearReparacionDatalleDto(string Descripcion, decimal Precio);
    public class ReparacionDto
    {
        public int Id { get; set; }
        public DateTime FechaRegistro { get; set; }
        public DateTime FechaEntrega { get; set; }
        public int IdCliente { get; set; }
        public int IdTecnico { get; set; }
        public string Cliente { get; set; } = string.Empty;
        public string Estado { get; set; } = string.Empty;
        public List<ReparacionDetalleDto> Detalles { get; set; } = new();
    }
    public partial class ReparacionDetalleDto
    {
        public int Id { get; set; }

        public string Descripcion { get; set; } = string.Empty;

        public decimal Precio { get; set; }
    }


}
