using Microsoft.Bot.Builder.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;

namespace suzanobot.Dialogs
{
    [Serializable]
    public class NotaFiscalDialog : IDialog
    {
        public async Task StartAsync(IDialogContext context)
        {
            string message = "Qual o status da sua nota fical?";

            PromptDialog.Choice(context, ResumeAfterAnswer, new[] { "Não Lançada", "Lançada", "Em Processamento", "Não Sei" }, message);
        }

        private async Task ResumeAfterAnswer(IDialogContext context, IAwaitable<string> result)
        {
            var message = context.MakeMessage();

            switch (await result)
            {
                case "Não Lançada":
                    message.Text = string.Format("## Opção de nota fical **Não Lançada** \n\n Acesse o link [Catalago Fiscal](https://suzanoprod.service-now.com/csc/csc_catalogo_fiscal.do)");
                    break;
                case "Lançada":
                    break;
                case "Em Processamento":
                    break;
                case "Não Sei":
                    break;
            }
            await context.PostAsync(message);

            context.Done(await result);
        }
    }
}