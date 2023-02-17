using System;
using Dynacoop2023.AlfaPeople.Controller;
using Dynacoop2023.AlfaPeople.Model;
using Dynacoop2023.AlfaPeople.Repository;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Tooling.Connector;

namespace Dynacoop2023.AlfaPeople
{
    public class Program
    {
        static void Main(string[] args)
        {
            CrmServiceClient serviceClient = Singleton.GetService();

            ContaController contaController = new ContaController(serviceClient);
            ContatoController contatoController = new ContatoController(serviceClient);

            ContaModel contaModel = new ContaModel();
            ContatoModel contatoModel = new ContatoModel();




            //Cria Conta
            Console.WriteLine("__________Criação de Conta__________");

            Console.WriteLine("\nInforme o CNPJ da conta:");
            string CNPJ = Console.ReadLine();    
            
            var retornoCnpj = contaController.BuscaCnpj(CNPJ);
            if (retornoCnpj != null && retornoCnpj.Entities != null && retornoCnpj.Entities.Count > 0)
            {
                Console.WriteLine("Esse cnpj ja existe em uma conta.");
            }
            else
            {
                contaModel.Cnpj = CNPJ;

                Console.WriteLine("\nInforme o nome da Conta:");
                contaModel.Nome = Console.ReadLine();

                Console.WriteLine("\nInforme a preferencia para contato:\n1 - Telefone\n2 - Email");
                string opcao = Console.ReadLine();
                if (opcao == "1")
                {
                    contaModel.PreferenciaContato = 892440000;
                }
                else if (opcao == "2")
                {
                    contaModel.PreferenciaContato = 892440001;
                }

                Console.WriteLine("\nInforme o numero total de oportunidades:");
                contaModel.NumTotalOpp = int.Parse(Console.ReadLine());

                Console.WriteLine("\nInforme o valor total de oportunidades:");
                contaModel.ValorTotalOpp = decimal.Parse(Console.ReadLine());

                Console.WriteLine("\nInforme o segmento da conta:\n1 - Oportunidade\n2 - Cliente Potencial");
                string segmento = Console.ReadLine();
                if (segmento == "1")
                {
                    contaModel.IdSegmento = "200ccc65-3d32-4e31-857a-528fbf2ca72f";
                }
                else if (segmento == "2")
                {
                    contaModel.IdSegmento = "ff668578-4fb2-4153-bc40-9a95f6ddad54";
                }



                Console.WriteLine("\nDeseja cadastrar um contato para essa conta? S/N");
                string opcao2 = Console.ReadLine().ToUpper();
                if (opcao2 == "N")
                {
                    contaController.Create(contaModel);
                    Console.WriteLine("Conta criada com sucesso!");
                }
                else
                {
                    var idConta = contaController.Create(contaModel);
                    Console.Clear();
                    CriaContato(contatoController, contatoModel, idConta);
                }
                

            }
            Console.ReadKey();


        }

        private static void CriaContato(ContatoController contatoController, ContatoModel contatoModel, Guid idConta)
        {
            Console.WriteLine("__________Criação de Contato__________");

            Console.WriteLine("\nInforme o número do CPF:");
            string CPF = Console.ReadLine();            
            var retorno = contatoController.BuscaCpf(CPF);
            if (retorno != null && retorno.Entities != null && retorno.Entities.Count > 0) 
            {
                Console.WriteLine("Esse cpf ja existe para um contato.");
            }
            else
            {
                contatoModel.Cpf = CPF;

                Console.WriteLine("\nInforme o nome e um sobrenome do contato da conta:");
                string contatoPrimario = Console.ReadLine();
                string[] nomeCompleto = contatoPrimario.Split(' ');

                contatoModel.PrimeiroNome = nomeCompleto[0];
                contatoModel.Sobrenome = nomeCompleto[nomeCompleto.Length - 1];

                Console.WriteLine($"\nInforme o cargo/função de {nomeCompleto[0]}:");
                contatoModel.Cargo = Console.ReadLine();

                Console.WriteLine("\nInforme um telefone comercial:");
                contatoModel.Telefone = Console.ReadLine();

                contatoController.Create(contatoModel, idConta);
                Console.WriteLine("\nConta e contato criados com sucesso!");
            }
           

            Console.ReadKey();
        }
        

        
    }
}
