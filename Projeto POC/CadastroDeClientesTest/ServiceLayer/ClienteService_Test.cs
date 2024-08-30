using CadastroDeClientesBackEnd.Controllers;
using CadastroDeClientesBackEnd.DatabaseLayer;
using CadastroDeClientesBackEnd.Models;
using CadastroDeClientesBackEnd.ServiceLayer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CadastroDeClientesTest.ServiceLayer
{
    [DoNotParallelize]
    [TestClass]
    public class ClienteService_Test
    {
        public void ClearDB() {

            var optionsBuilder = new DbContextOptionsBuilder<DBContext>();
            optionsBuilder.UseSqlServer(ServiceLayer_Global.Connection);
            var dbContext = new DBContext(optionsBuilder.Options);
            dbContext.Clientes.RemoveRange(dbContext.Clientes);
            dbContext.SaveChanges();
        }

        [TestMethod]
        public void CreateCliente_Sucesso()
        {
            ClearDB();
            var optionsBuilder = new DbContextOptionsBuilder<DBContext>();
            optionsBuilder.UseSqlServer(ServiceLayer_Global.Connection);
            var dbContext = new DBContext(optionsBuilder.Options);

            var clienteService = new ClienteService(dbContext);
            var controller = new ClienteController(clienteService);

            var clienteA = new ClienteCreateModel();
            clienteA.Nome = "Cliente A";
            clienteA.Email = "Email@com.br";

            var response = controller.CreateCliente(clienteA);

            var OkObjectResult = (OkObjectResult)response.Result.Result;
            var serviceResponse = (ServiceResponse<string>)OkObjectResult.Value;

            var retorno_esperado_Status = "sucesso";
            var retorno_esperado_Msg = "Cliente cadastrar com sucesso.";
            var retorno_esperado_Dados = 36;

            Assert.IsTrue(
                serviceResponse.Status == retorno_esperado_Status &&
                serviceResponse.Msg == retorno_esperado_Msg &&
                serviceResponse.Dados.Length == retorno_esperado_Dados);
        }

        [TestMethod]
        public void CreateCliente_EmailDuplicado_Error2()
        {
            var optionsBuilder = new DbContextOptionsBuilder<DBContext>();
            optionsBuilder.UseSqlServer(ServiceLayer_Global.Connection);
            var dbContext = new DBContext(optionsBuilder.Options);

            var clienteService = new ClienteService(dbContext);
            var controller = new ClienteController(clienteService);

            var clienteA = new ClienteCreateModel();
            clienteA.Nome = "Cliente A";
            clienteA.Email = "Email@com.br";

            var response = controller.CreateCliente(clienteA);

            var OkObjectResult = (OkObjectResult)response.Result.Result;
            var serviceResponse = (ServiceResponse<string>)OkObjectResult.Value;

            var retorno_esperado_Status = "inválido";
            var retorno_esperado_Msg = "Já existe cliente cadastrado com o e-mail: Email@com.br";
            string retorno_esperado_Dados = null;

            Assert.IsTrue(
                serviceResponse.Status == retorno_esperado_Status &&
                serviceResponse.Msg == retorno_esperado_Msg &&
                serviceResponse.Dados == retorno_esperado_Dados);
        }

        [TestMethod]
        public void GetClientes_Success3()
        {
            var optionsBuilder = new DbContextOptionsBuilder<DBContext>();
            optionsBuilder.UseSqlServer(ServiceLayer_Global.Connection);
            var dbContext = new DBContext(optionsBuilder.Options);

            var clienteService = new ClienteService(dbContext);
            var controller = new ClienteController(clienteService);

            var response = controller.GetClientes();

            var OkObjectResult = (OkObjectResult)response.Result.Result;
            var serviceResponse = (ServiceResponse<List<ClienteGetModel>>)OkObjectResult.Value;

            var retorno_esperado_Dados = 1;

            Assert.AreEqual(retorno_esperado_Dados, serviceResponse.Dados.Count);
        }
    }
}
