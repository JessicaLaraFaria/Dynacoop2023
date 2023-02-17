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
            string url = "";
            string clientId = "";
            string clientSecret = "";

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
