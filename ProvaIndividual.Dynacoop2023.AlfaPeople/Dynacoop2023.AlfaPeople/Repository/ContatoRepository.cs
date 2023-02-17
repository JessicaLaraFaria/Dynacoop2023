using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dynacoop2023.AlfaPeople.Model;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using Microsoft.Xrm.Tooling.Connector;

namespace Dynacoop2023.AlfaPeople.Repository
{
    public class ContatoRepository
    {
        private CrmServiceClient serviceClient { get; set; }
        private string logicalname { get; set; }

        public ContatoRepository(CrmServiceClient crmserviceClient)
        {
            this.serviceClient = crmserviceClient;
            this.logicalname = "contact";
        }

        public Guid Create(ContatoModel contatoModel, Guid idConta)
        {
            Entity contato = new Entity(this.logicalname);

            contato["pjf_cpf"] = contatoModel.Cpf;
            contato["firstname"] = contatoModel.PrimeiroNome;
            contato["lastname"] = contatoModel.Sobrenome;
            contato["jobtitle"] = contatoModel.Cargo;
            contato["telephone1"] = contatoModel.Telefone;
            contato["parentcustomerid"] = new EntityReference("account", idConta);

            Guid contactId = this.serviceClient.Create(contato);
            return contactId;
        }

        public EntityCollection BuscaCpf(string cpf)
        {
            QueryExpression queryContact = new QueryExpression(this.logicalname);
            queryContact.ColumnSet.AddColumns("firstname", "telephone1");
            queryContact.Criteria.AddCondition("pjf_cpf", ConditionOperator.Equal, cpf);
            return this.serviceClient.RetrieveMultiple(queryContact);
        }

    }
}
