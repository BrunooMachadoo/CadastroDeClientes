using CadastroDeClientesBackEnd.Models;
using System.Text.RegularExpressions;

namespace CadastroDeClientesBackEnd.BusinessLayer
{
    public class CreateClienteBU
    {
        public RetornoBU Validade(ClienteCreateModel cliente)
        {
            var retornoBU = new RetornoBU();

            if (cliente == null)
            {
                retornoBU.Sucesso = false;
                retornoBU.Msg = "Informe os dados do cliente para realização do cadastro.";
                return retornoBU;
            }
            else if (string.IsNullOrEmpty(cliente.Email))
            {
                retornoBU.Sucesso = false;
                retornoBU.Msg = "O campo E-mail é obrigatório.";
                return retornoBU;
            }
            else if (string.IsNullOrEmpty(cliente.Nome))
            {
                retornoBU.Sucesso = false;
                retornoBU.Msg = "O campo Nome é obrigatório.";
                return retornoBU;
            }
            else if (!string.IsNullOrEmpty(cliente.Nome))
            {
                Match mt = Regex.Match(cliente.Nome, "[^a-zA-Zà-úÀ-Ú0-9 ]+");

                if (mt.Success)
                {
                    retornoBU.Sucesso = false;
                    retornoBU.Msg = $"O campo Nome não suporta os caracteres: {mt.Value}";
                    return retornoBU;
                }
            }

            retornoBU.Sucesso = true;
            return retornoBU;
        }
    }
}
