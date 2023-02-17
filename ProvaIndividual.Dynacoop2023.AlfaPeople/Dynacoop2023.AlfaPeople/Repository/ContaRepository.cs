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
    public class ContaRepository
    {
        private CrmServiceClient serviceClient { get; set; }
        private string logicalname { get; set; }

        public ContaRepository(CrmServiceClient crmserviceClient)
        {
            this.serviceClient = crmserviceClient;
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

            Guid accountId = serviceClient.Create(conta);
            return accountId;
        }

        public EntityCollection BuscaCnpj(string cnpj)
        {
            QueryExpression queryAccount = new QueryExpression(this.logicalname);
            queryAccount.ColumnSet.AddColumns("name");
            queryAccount.Criteria.AddCondition("pjf_cnpj", ConditionOperator.Equal, cnpj);
            return this.serviceClient.RetrieveMultiple(queryAccount);
        }


    }
}
