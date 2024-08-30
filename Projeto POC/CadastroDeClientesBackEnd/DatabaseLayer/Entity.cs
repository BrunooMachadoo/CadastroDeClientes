using Microsoft.Win32;
using System.ComponentModel.DataAnnotations;
using System.Net;

namespace CadastroDeClientesBackEnd.DatabaseLayer
{
    public class Entity
    {
        //O cadastro dos clientes deve conter apenas os seguintes campos: Nome, e-mail, Logotipo* e Logradouro.
        public class Cliente
        {
            public Guid Id { get; set; }
            public string Nome { get; set; }
            public string Email { get; set; }
            public string Logotipo { get; set; }
            public ICollection<ClienteEndereco> Enderecos { get; set; }

        }
               
        public class Endereco
        {
            public Guid Id { get; set; }
            public string Logradouro { get; set; }
            public ICollection<ClienteEndereco> Enderecos { get; set; }
        }

        public class ClienteEndereco
        {
            public Guid ClienteId { get; set; }
            public Cliente Cliente { get; set; }
            public Guid EnderecoId { get; set; }
            public Endereco Endereco { get; set; }
        }

    }
}
