using CadastroDeClientesBackEnd.BusinessLayer;
using CadastroDeClientesBackEnd.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CadastroDeClientesTest.BusinessLayer
{
    [TestClass]
    public class CreateClienteBU_Test
    {
        [TestMethod]
        public void Dados_Null_Erro()
        {
            var createClienteBU = new CreateClienteBU();
            var retornoBU = createClienteBU.Validade(null);

            var retorno_esperado_Sucesso = false;
            var retorno_esperado_Msg = "Informe os dados do cliente para realização do cadastro.";

            Assert.IsTrue(
               retornoBU.Sucesso == retorno_esperado_Sucesso &&
               retornoBU.Msg == retorno_esperado_Msg);
        }

        [TestMethod]
        public void Email_Em_Null_Erro()
        {
            var createClienteBU = new CreateClienteBU();
            var clienteCreateModel = new ClienteCreateModel();
            var retornoBU = createClienteBU.Validade(clienteCreateModel);

            var retorno_esperado_Sucesso = false;
            var retorno_esperado_Msg = "O campo E-mail é obrigatório.";

            Assert.IsTrue(
               retornoBU.Sucesso == retorno_esperado_Sucesso &&
               retornoBU.Msg == retorno_esperado_Msg);
        }

        [TestMethod]
        public void Email_Nome_Null_Erro()
        {
            var createClienteBU = new CreateClienteBU();
            var clienteCreateModel = new ClienteCreateModel();
            clienteCreateModel.Email = "email@com.br";
            var retornoBU = createClienteBU.Validade(clienteCreateModel);

            var retorno_esperado_Sucesso = false;
            var retorno_esperado_Msg = "O campo Nome é obrigatório.";

            Assert.IsTrue(
               retornoBU.Sucesso == retorno_esperado_Sucesso &&
               retornoBU.Msg == retorno_esperado_Msg);
        }

        [TestMethod]
        public void Email_Nome_Invalido_Erro()
        {
            var createClienteBU = new CreateClienteBU();
            var clienteCreateModel = new ClienteCreateModel();
            clienteCreateModel.Email = "email@com.br";
            clienteCreateModel.Nome = "fulano de t@l";
            var retornoBU = createClienteBU.Validade(clienteCreateModel);

            var retorno_esperado_Sucesso = false;
            var retorno_esperado_Msg = "O campo Nome não suporta os caracteres: @";

            Assert.IsTrue(
               retornoBU.Sucesso == retorno_esperado_Sucesso &&
               retornoBU.Msg == retorno_esperado_Msg);
        }
    }
}
