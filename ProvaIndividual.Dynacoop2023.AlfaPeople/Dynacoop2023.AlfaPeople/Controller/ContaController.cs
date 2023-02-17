using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dynacoop2023.AlfaPeople.Model;
using Dynacoop2023.AlfaPeople.Repository;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Tooling.Connector;

namespace Dynacoop2023.AlfaPeople.Controller
{
    public class ContaController
    {
        private ContaRepository contaRepository { get; set; }

        public ContaController(CrmServiceClient serviceClient)
        {
            this.contaRepository = new ContaRepository(serviceClient);
        }

        public Guid Create(ContaModel contaModel)
        {
            return contaRepository.Create(contaModel);
        }

        public EntityCollection BuscaCnpj(string cnpj)
        {
            return contaRepository.BuscaCnpj(cnpj);
        }
    }
}
