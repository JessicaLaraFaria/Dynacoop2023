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
    public class ContatoController
    { 
        private ContatoRepository contatoRepository { get; set; }

        public ContatoController(CrmServiceClient serviceClient)
        {
            this.contatoRepository = new ContatoRepository(serviceClient);
        }

        public Guid Create(ContatoModel contatoModel, Guid idConta)
        {
            return contatoRepository.Create(contatoModel, idConta);
        }

        public EntityCollection BuscaCpf(string cpf)
        {
            return contatoRepository.BuscaCpf(cpf);
        }
    }
}
