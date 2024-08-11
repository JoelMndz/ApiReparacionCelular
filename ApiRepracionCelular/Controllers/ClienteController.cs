using ApiRepracionCelular.Controllers.Interfaces;
using ApiRepracionCelular.Dto;
using ApiRepracionCelular.Servicios.Cliente.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ApiRepracionCelular.Controllers
{
    [Authorize]
    public class ClienteController : ApiControllerBase
    {
        private readonly IClienteService clienteService;

        public ClienteController(IClienteService clienteService)
        {
            this.clienteService = clienteService;
        }

        [HttpGet]
        public async Task<ActionResult> ObtenerClientes()
        {
            var clientes = await clienteService.ObtenerClientes();
            return Ok(clientes);
        }

        [HttpPost]
        public async Task<ActionResult> CrearCliente(CrearClienteDto dto)
        {
            var cliente = await clienteService.CrearCliente(dto);
            return Ok(cliente);
        }

        [HttpPatch]
        public async Task<ActionResult> ActualizarCliente(ClienteDto dto)
        {
            var cliente = await clienteService.ActualizarCliente(dto);
            return Ok(cliente);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> EliminarCliente(int id)
        {
            var cliente = await clienteService.EliminarCliente(id);
            return Ok(cliente);
        }
    }
}
