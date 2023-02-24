using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xrm.Tooling.Connector;

namespace Dynacoop2023.AlfaPeople
{
    public class Singleton
    {
        public static CrmServiceClient GetService()
        {
            string url = "org9f220468.crm2.dynamics.com/";
            string clientId = "d0793697-2bcf-4540-ae71-193c41fc822c";
            string clientSecret = "5a88Q~i36xKjc7-v.FdZ4Eh-3JqQgs~rKlJ9Vdvj";

            CrmServiceClient serviceCliente = new CrmServiceClient($"AuthType=ClientSecret;" +
                $"                                                                  Url=https://{url};" +
                $"                                                                  AppId={clientId};" +
                $"                                                                  ClientSecret={clientSecret};");

            if (!serviceCliente.CurrentAccessToken.Equals(null))
            {
                Console.WriteLine("Conexão realizada com sucesso");
            }
            else
            {
                Console.WriteLine("Erro na conexão");
            }
            return serviceCliente;
        }
    }
}
