using System;
using Dynacoop2023.AlfaPeople.Model;
using Dynacoop2023.AlfaPeople.Repository;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Tooling.Connector;

namespace Dynacoop2023.AlfaPeople.Controller
{
    public class ContaController
    {
        private ContaRepository contaRepository { get; set; }
        
        public ContaController(IOrganizationService serviceClient)
        {
            this.contaRepository = new ContaRepository(serviceClient);
        }

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

        public Entity GetAccountById(Guid id, string[] columns)
        {
            return contaRepository.GetAccountById(id, columns);
        }

        public void IncrementOrDecrementTotalAmount(Entity opp, bool incrementa, decimal valorTotalOpp)
        {
            contaRepository.IncrementOrDecrementTotalAmount(opp, incrementa, valorTotalOpp);
        }
    }
}
