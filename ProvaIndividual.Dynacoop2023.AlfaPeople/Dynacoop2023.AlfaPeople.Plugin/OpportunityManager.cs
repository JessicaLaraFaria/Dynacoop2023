using System;
using Dynacoop2023.AlfaPeople.Controller;
using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk;

namespace Dynacoop2023.AlfaPeople.Plugin
{
    public class OpportunityManager : IPlugin
    {
        IOrganizationService service { get; set; }
        public void Execute(IServiceProvider serviceProvider)
        {
            IPluginExecutionContext context = (IPluginExecutionContext)serviceProvider.GetService(typeof(IPluginExecutionContext));
            IOrganizationServiceFactory serviceFactory = (IOrganizationServiceFactory)serviceProvider.GetService(typeof(IOrganizationServiceFactory));
            service = serviceFactory.CreateOrganizationService(context.UserId);
            ITracingService tracingService = (ITracingService)serviceProvider.GetService(typeof(ITracingService));

            Entity opportunity = new Entity();

            Create(context, opportunity);
            UpdateAccount(context, opportunity);

        }

        private void UpdateAccount(IPluginExecutionContext context, Entity opportunity)
        {
            EntityReference accountReference = opportunity.Contains("parentaccountid") ? (EntityReference)opportunity["parentaccountid"] : null;              

            if(accountReference != null)
            {
                decimal valorTotalOppPost = opportunity.GetAttributeValue<Money>("totalamount").Value;
                UpdateValorTotalOportunidade(accountReference, true, valorTotalOppPost);
            }


            if (context.MessageName.Equals("Update"))
            {
                Entity preImage = (Entity)context.PreEntityImages["PreImage"];                   
                Entity postImage = (Entity)context.PostEntityImages["PostImage"];                   

                if (
                    preImage != null &&
                    preImage.Contains("parentaccountid") &&
                    postImage != null && postImage.Contains("parentaccountid") &&
                    preImage.GetAttributeValue<EntityReference>("parentaccountid").Id == postImage.GetAttributeValue<EntityReference>("parentaccountid").Id
                    )
                {
                    return;
                }

                if (preImage.Contains("parentaccountid"))
                {
                    decimal valorTotalOpp = preImage.GetAttributeValue<Money>("totalamount").Value;
                    EntityReference preAccountReference = (EntityReference)preImage["parentaccountid"];
                    Entity opp = UpdateValorTotalOportunidade(preAccountReference, false, valorTotalOpp);
                }

                if (postImage.Contains("parentaccountid"))
                {
                    decimal valorTotalOppPost = postImage.GetAttributeValue<Money>("totalamount").Value;
                    EntityReference postAccountReference = (EntityReference)postImage["parentaccountid"];
                    UpdateValorTotalOportunidade(postAccountReference, true, valorTotalOppPost);
                }
                   
            }else if (context.MessageName.Equals("Delete"))
            {
                Entity preImage = (Entity)context.PreEntityImages["PreImage"];
                if (preImage.Contains("parentaccountid"))
                {
                    decimal valorTotalOpp = preImage.GetAttributeValue<Money>("totalamount").Value;
                    EntityReference preAccountReference = (EntityReference)preImage["parentaccountid"];
                    Entity opp = UpdateValorTotalOportunidade(preAccountReference, false, valorTotalOpp);
                }
            }            
        }

        private void Create(IPluginExecutionContext context, Entity opportunity)
        {
            if (context.MessageName.Equals("Create"))
            {
                opportunity = (Entity)context.InputParameters["Target"];
            }           
        }

        private Entity UpdateValorTotalOportunidade(EntityReference accountReference, bool incrementa, decimal valorTotalOpp)
        {
            ContaController contaController = new ContaController(service);
            Entity oppAccount = contaController.GetAccountById(accountReference.Id, new string[] { "pjf_valor_total_opp" });
            contaController.IncrementOrDecrementTotalAmount(oppAccount, incrementa, valorTotalOpp);
            return oppAccount;
        }
    }
}
