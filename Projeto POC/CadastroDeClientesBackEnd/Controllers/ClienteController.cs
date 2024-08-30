using CadastroDeClientesBackEnd.Models;
using CadastroDeClientesBackEnd.ServiceLayer;
using Microsoft.AspNetCore.Mvc;

namespace CadastroDeClientesBackEnd.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ClienteController : Controller
    {
        private readonly IClienteInterface _clienteService;
        public ClienteController(IClienteInterface clienteService)
        {
            _clienteService = clienteService;
        }

        [HttpGet]
        public async Task<ActionResult<ServiceResponse<List<ClienteGetModel>>>> GetClientes()
        {
            return Ok(await _clienteService.GetClientes());
        }

        [HttpPost]
        public async Task<ActionResult<ServiceResponse<string>>> CreateCliente(ClienteCreateModel cliente)
        {
            return Ok(await _clienteService.CreateCliente(cliente));
        }
    }
}
