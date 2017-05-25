using Microsoft.Bot.Builder.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using Microsoft.Bot.Connector;
using suzanobot.Utils;
using suzanobot.Services;
using suzanobot.Model;

namespace suzanobot.Dialogs
{
    [Serializable]
    public class NotaFiscalDialog : IDialog
    {

        private string[] optionsNotaFiscal = new[]
           {
                "Emissão de guias antecipadas",
                "Com Recopi"
            };

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

            PromptDialog.Choice(context, ResumeAfterAnswer, Options.optionsNotaFiscal, message);
        }

        private async Task ResumeAfterAnswer(IDialogContext context, IAwaitable<string> result)
        {
            var message = context.MakeMessage();
            var choice = await result;
            string text = string.Format("Você escolheu {0}, escolha o subitem abaixo:", choice);

            //"Solicitação de pagamento e duvidas"
            if (choice.Equals(Options.optionsNotaFiscal[0]))
            {
                PromptDialog.Choice(context, ResumeItemAnswer, Options.optionsSolicitacaoPagamentoNF, text);
            }


            //""Nota travada SAP",
            if (choice.Equals(Options.optionsNotaFiscal[1]))
            {
                PromptDialog.Choice(context, ResumeItemAnswer, Options.optionsTravadaSAPNF, text);
            }

            //"Cancelamento de nota fiscal"
            if (choice.Equals(Options.optionsNotaFiscal[2]))
            {
                PromptDialog.Choice(context, ResumeItemAnswer, Options.optionsCancelamentoNF, text);
            }
        }

        private async Task ResumeItemAnswer(IDialogContext context, IAwaitable<string> result)
        {
            AzureSearchClient searchClient = new AzureSearchClient();

            //Busca o documento no Azure Search
            var itemSeaarchDialog = new ItemSearchDialog(searchClient);
            context.Call(itemSeaarchDialog, OutrosReplayMessage);
            context.Done(await result);

        }

        private Task OutrosReplayMessage(IDialogContext context, IAwaitable<object> result)
        {
            throw new NotImplementedException();
        }
    }
}