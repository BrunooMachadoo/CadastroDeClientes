using CadastroDeClientesBackEnd.BusinessLayer;
using CadastroDeClientesBackEnd.DatabaseLayer;
using CadastroDeClientesBackEnd.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using static CadastroDeClientesBackEnd.DatabaseLayer.Entity;

namespace CadastroDeClientesBackEnd.ServiceLayer
{
    public class ClienteService : IClienteInterface
    {
        private readonly DBContext _context;

        public ClienteService(DBContext context)
        {
            _context = context;
        }

        public Task<ServiceResponse<ClienteGetModel>> GetCliente(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<ServiceResponse<string>> CreateCliente(ClienteCreateModel cliente)
        {
            var serviceResponse = new ServiceResponse<string>();

            var businessLayer = new CreateClienteBU();
            var validacaoBU = businessLayer.Validade(cliente);

            if (!validacaoBU.Sucesso)
            {
                serviceResponse.Status = "validação";
                serviceResponse.Msg = validacaoBU.Msg;
                return serviceResponse;
            }

            var clienteEntity = new Cliente();

            try
            {
                clienteEntity.Id = Guid.NewGuid();
                clienteEntity.Nome = cliente.Nome;
                clienteEntity.Email = cliente.Email;
                clienteEntity.Logotipo = cliente.Logotipo;

                _context.Clientes.Add(clienteEntity);
                await _context.SaveChangesAsync();
                serviceResponse.Dados = clienteEntity.Id.ToString();
                serviceResponse.Status = "sucesso";
                serviceResponse.Msg = "Cliente cadastrar com sucesso.";
            }
            catch (Exception ex)
            {
                if (ex.InnerException is SqlException)
                {
                    var sqlException = ex.InnerException as SqlException;

                    bool isEmailDuplicadoNumber = sqlException.Number == 2601;

                    bool isEmailDuplicadoText = sqlException.Message.Contains("IX_Clientes_Email'. The duplicate key");

                    if (isEmailDuplicadoNumber && isEmailDuplicadoText)
                    {
                        serviceResponse.Status = "inválido";
                        serviceResponse.Msg = $"Já existe cliente cadastrado com o e-mail: {clienteEntity.Email}";
                    }
                    else
                    {
                        serviceResponse.Status = "erro";
                        serviceResponse.Msg = "Não foi possível cadastrar o cliente.";
                    }
                }
                else
                {
                    serviceResponse.Status = "erro";
                    serviceResponse.Msg = "Não foi possível cadastrar o cliente.";
                }
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<List<ClienteGetModel>>> GetClientes()
        {
            var serviceResponse = new ServiceResponse<List<ClienteGetModel>>();

            try
            {
                DbSet<Cliente> clientes = _context.Clientes;

                if (clientes.Count() > 0)
                {
                    serviceResponse.Dados = new List<ClienteGetModel>();

                    foreach (var cliente in clientes)
                    {
                        var clienteModel = new ClienteGetModel();
                        clienteModel.Id = cliente.Id;
                        clienteModel.Nome = cliente.Nome;
                        clienteModel.Email = cliente.Email;

                        serviceResponse.Dados.Add(clienteModel);
                    }

                    serviceResponse.Status = "sucesso";
                    serviceResponse.Msg = "Clientes recuperados com sucesso.";
                }
                else
                {
                    serviceResponse.Status = "sucesso";
                    serviceResponse.Msg = "Não existem clientes cadastrados.";
                }
            }
            catch (Exception ex)
            {
                serviceResponse.Status = "erro";
                serviceResponse.Msg = "Não foi possível consultar os clientes.";
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<string>> DeleteCliente(string id)
        {
            var serviceResponse = new ServiceResponse<string>();

            try
            {
                _context.Clientes.Remove(new Cliente() { Id = Guid.Parse(id) });
                await _context.SaveChangesAsync();
                serviceResponse.Status = "sucesso";
                serviceResponse.Msg = "Cliente deletado com sucesso.";
            }
            catch (Exception)
            {
                serviceResponse.Status = "erro";
                serviceResponse.Msg = "Não foi possível deletar o cliente.";
            }

            return serviceResponse;
        }
    }
}
