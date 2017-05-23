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

            PromptDialog.Choice(context, ResumeAfterAnswer, new[] { "Não Lançada", "Lançada", "Em Proocessamento", "Não Sei" }, message);
        }

        private async Task ResumeAfterAnswer(IDialogContext context, IAwaitable<string> result)
        {
            context.Done(await result);
        }
    }
}