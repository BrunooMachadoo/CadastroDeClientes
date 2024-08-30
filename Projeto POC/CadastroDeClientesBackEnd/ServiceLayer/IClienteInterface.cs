using CadastroDeClientesBackEnd.Models;

namespace CadastroDeClientesBackEnd.ServiceLayer
{
    public interface IClienteInterface
    {
        Task<ServiceResponse<List<ClienteGetModel>>> GetClientes();
        Task<ServiceResponse<string>> CreateCliente(ClienteCreateModel cliente);
        Task<ServiceResponse<string>> DeleteCliente(string id);
        Task<ServiceResponse<ClienteGetModel>> GetCliente(Guid id);

    }
}
