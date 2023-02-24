using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dynacoop2023.AlfaPeople.Model;
using Microsoft.Rest;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using Microsoft.Xrm.Tooling.Connector;

namespace Dynacoop2023.AlfaPeople.Repository
{
    public class ContaRepository
    {
        private CrmServiceClient crmserviceClient { get; set; }
        private string logicalname { get; set; }
        private IOrganizationService serviceClient { get; set; }

        public ContaRepository(IOrganizationService serviceClient)
        {
            this.serviceClient = serviceClient;
            this.logicalname = "account";
        }

        public ContaRepository(CrmServiceClient crmserviceClient)
        {
            this.crmserviceClient = crmserviceClient;
            this.logicalname = "account";
        }
        public Guid Create(ContaModel contaModel)
        {
            Entity conta = new Entity(this.logicalname);
            conta["pjf_cnpj"] = contaModel.Cnpj;
            conta["name"] = contaModel.Nome;
            conta["msdyn_segmentid"] = new EntityReference("msdyn_segment",Guid.Parse(contaModel.IdSegmento));
            conta["pjf_preferencia_contato"] = new OptionSetValue(contaModel.PreferenciaContato);
            conta["pjf_num_total_opp"] = contaModel.NumTotalOpp;
            conta["pjf_valor_total_opp"] = new Money(contaModel.ValorTotalOpp);            

            Guid accountId = crmserviceClient.Create(conta);
            return accountId;
        }

        public EntityCollection BuscaCnpj(string cnpj)
        {
            QueryExpression queryAccount = new QueryExpression(this.logicalname);
            queryAccount.ColumnSet.AddColumns("name");
            queryAccount.Criteria.AddCondition("pjf_cnpj", ConditionOperator.Equal, cnpj);
            return this.crmserviceClient.RetrieveMultiple(queryAccount);
        }

        public Entity GetAccountById(Guid id, string[] columns)
        {
            return serviceClient.Retrieve(this.logicalname, id, new ColumnSet(columns));
        }

        public void IncrementOrDecrementTotalAmount(Entity oppAccount, bool? incrementa, decimal valorTotalOpp)
        {
            decimal valorOpp = oppAccount.Contains("pjf_valor_total_opp") ? (decimal)oppAccount.GetAttributeValue<Money>("pjf_valor_total_opp").Value : 0;
            

            if (Convert.ToBoolean(incrementa))
            {
                valorOpp += valorTotalOpp;
            }
            else
            {
                valorOpp -= valorTotalOpp;
            }

            oppAccount["pjf_valor_total_opp"] = new Money(valorOpp);
            serviceClient.Update(oppAccount);
        }


    }
}
