using Microsoft.Bot.Builder.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using Microsoft.Bot.Connector;

namespace suzanobot.Dialogs
{
    [Serializable]
    public class NotaFiscalDialog : IDialog
    {
        private string[] optionsRecopi = new[]
            {
                "Sem Recopi",
                "Com Recopi"
            };

        private string[] optionsSuporteFornecedor = new[]
            {
                "Valor NF",
                "Tributação",
                "Alíquota",
                "Ajuste Cadastro",
            };

        private string[] optionsNotaTravadaSAP = new[]
            {
                "Aprovação de NF",
                "Reimpressão de NF",
                "Recusa de NF",
                "Status de NF",
            };

        private string[] optionsCancelamentoNF = new[]
            {
                "Emissão superior a 24 horas",
                "Emissão inferior a 24 horas",
            };

        private string[] optionsRecebimentoNF = new[]
            {
                "Informações necessárias para pagamento",
            };

        public async Task StartAsync(IDialogContext context)
        {
            string message = "Abaixo, você encontra as principais situações relacionadas à notas fiscais:";


            //PromptDialog.Choice(context, ResumeAfterAnswer, new[] { "Não Lançada", "Lançada", "Em Processamento", "Não Sei" }, message);
            PromptDialog.Choice(context, ResumeAfterAnswer, new[] { "Recopi", "Suporte ao Fornecedor", "Nota travada SAP", "Cancelamento", "Recebimento de NF", "Emissão de guias antecipadas", "Emissão de Carta de Correção (CC-e)", "Emissão NF", "Emissão NF Complementar" }, message);
        }

        private async Task ResumeAfterAnswer(IDialogContext context, IAwaitable<string> result)
        {
            var message = context.MakeMessage();
            //await context.PostAsync(message);

            switch (await result)
            {
                case "Recopi":
                    //message.Text = string.Format("## Opção de nota fical **Não Lançada** \n\n Acesse o link [Catalago Fiscal](https://suzanoprod.service-now.com/csc/csc_catalogo_fiscal.do)");
                    //message.AddHeroCard("Escolha o processo", optionsRecopi);
                    //await context.PostAsync(message);
                    //context.Wait(this.OnOptionSelectedRecopi);

                    PromptDialog.Choice(context, OnOptionSelectedRecopi, optionsRecopi, "Escolha o processo");

                    break;
                case "Suporte ao Fornecedor":
                    message.AddHeroCard("Escolha o processo", optionsSuporteFornecedor);
                    await context.PostAsync(message);
                    context.Wait(this.OnOptionSelectedSuporteFornecedor);
                    break;
                case "Nota travada SAP":
                    message.AddHeroCard("Escolha o processo", optionsNotaTravadaSAP);
                    await context.PostAsync(message);
                    context.Wait(this.OnOptionSelectedNotaTravadaSAP);
                    break;
                case "Cancelamento":
                    message.AddHeroCard("Escolha o processo", optionsCancelamentoNF);
                    await context.PostAsync(message);
                    context.Wait(this.OnOptionSelectedCancelamentoNF);
                    break;
                case "Recebimento de NF":
                    message.AddHeroCard("Escolha o processo", optionsRecebimentoNF);
                    await context.PostAsync(message);
                    context.Wait(this.OnOptionSelectedRecebimentoNF);
                    break;
                case "Emissão de guias antecipadas":
                    break;
                case "Emissão de Carta de Correção (CC-e)":
                    break;
                case "Emissão NF":
                    break;
                case "Emissão NF Complementar":
                    break;


                    //case "Não Lançada":
                    //    message.Text = string.Format("## Opção de nota fical **Não Lançada** \n\n Acesse o link [Catalago Fiscal](https://suzanoprod.service-now.com/csc/csc_catalogo_fiscal.do)");
                    //    break;
                    //case "Lançada":
                    //    break;
                    //case "Em Processamento":
                    //    break;
                    //case "Não Sei":
                    //    break;
            }
            //await context.PostAsync(message);

            //context.Done(await result);
        }

        private async Task OnOptionSelectedRecopi(IDialogContext context, IAwaitable<string> result)
        {
            var message = await result;
            var text = "";

            //Nota Fiscal
            if (message == "Sem Recopi")
            {
                //var ntDialog = new NotaFiscalDialog();
                //text = "Você clicou em Nota fical";
                text = "Você clicou em Sem Recop";
            }

            //Documento Fiscal
            if (message == "Com Recopi")
            {
                text = "Você clicou em Com Recop";
            }

            var reply = context.MakeMessage();
            reply.Text = HttpUtility.HtmlDecode(text);
            await context.PostAsync(reply);

            context.Done(await result);
        }
        private async Task OnOptionSelectedSuporteFornecedor(IDialogContext context, IAwaitable<IMessageActivity> result)
        {
            var message = await result;
            var text = "";

            //Nota Fiscal
            if (message.Text == "Valor NF")
            {
                text = "Você clicou em Valor NF";
            }

            if (message.Text == "Tributação")
            {
                text = "Você clicou em Tributação";
            }

            if (message.Text == "Alíquota")
            {
                text = "Você clicou em Alíquota";
            }

            if (message.Text == "Ajuste Cadastro")
            {
                text = "Você clicou em Ajuste Cadastro";
            }

            var reply = context.MakeMessage();
            reply.Text = HttpUtility.HtmlDecode(text);
            await context.PostAsync(reply);
        }
        private async Task OnOptionSelectedNotaTravadaSAP(IDialogContext context, IAwaitable<IMessageActivity> result)
        {
            var message = await result;
            var text = "";

            //Nota Fiscal
            if (message.Text == "Aprovação de NF")
            {
                text = "Você clicou em Aprovação de NF";
            }

            if (message.Text == "Reimpressão de NF")
            {
                text = "Você clicou em Reimpressão de NF";
            }

            if (message.Text == "Recusa de NF")
            {
                text = "Você clicou em Recusa de NF";
            }

            if (message.Text == "Status de NF")
            {
                text = "Você clicou em Status de NF";
            }

            var reply = context.MakeMessage();
            reply.Text = HttpUtility.HtmlDecode(text);
            await context.PostAsync(reply);
        }
        private async Task OnOptionSelectedCancelamentoNF(IDialogContext context, IAwaitable<IMessageActivity> result)
        {
            var message = await result;
            var text = "";

            //Nota Fiscal
            if (message.Text == "Emissão superior a 24 horas")
            {
                text = "Você clicou em Emissão superior a 24 horas";
            }

            if (message.Text == "Emissão inferior a 24 horas")
            {
                text = "Você clicou em Emissão inferior a 24 horas";
            }

            if (message.Text == "Recusa de NF")
            {
                text = "Você clicou em Recusa de NF";
            }

            if (message.Text == "Status de NF")
            {
                text = "Você clicou em Status de NF";
            }

            var reply = context.MakeMessage();
            reply.Text = HttpUtility.HtmlDecode(text);
            await context.PostAsync(reply);
        }
        private async Task OnOptionSelectedRecebimentoNF(IDialogContext context, IAwaitable<IMessageActivity> result)
        {
            var message = await result;
            var text = "";

            //Nota Fiscal
            if (message.Text == "Informações necessárias para pagamento")
            {
                text = "Informações necessárias para pagamento";
            }

            if (message.Text == "Emissão inferior a 24 horas")
            {
                text = "Você clicou em Emissão inferior a 24 horas";
            }

            if (message.Text == "Recusa de NF")
            {
                text = "Você clicou em Recusa de NF";
            }

            if (message.Text == "Status de NF")
            {
                text = "Você clicou em Status de NF";
            }

            var reply = context.MakeMessage();
            reply.Text = HttpUtility.HtmlDecode(text);
            await context.PostAsync(reply);
        }

    }
}