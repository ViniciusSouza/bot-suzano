﻿using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using suzanobot.Extensions;
using System;
using System.Threading.Tasks;
using System.Web;
using suzanobot.Utils;

namespace suzanobot.Dialogs
{
    [Serializable]
    public class RootDialog : IDialog<object>
    {
        private ResumptionCookie resumptionCookie;
		
        public Task StartAsync(IDialogContext context)
        {
            context.Wait(MessageReceivedAsync);

            return Task.CompletedTask;
        }


        public virtual async Task MessageReceivedAsync(IDialogContext context, IAwaitable<IMessageActivity> result)
        {
            var message = await result;

            if (this.resumptionCookie == null)
            {
                this.resumptionCookie = new ResumptionCookie(message);
            }

            await this.WelcomeMessageAsync(context);
        }

        private async Task WelcomeMessageAsync(IDialogContext context)
        {
            var reply = context.MakeMessage();
            UriBuilder uri = Utils.Utils.GetPlaceHoldImg(56, 640, 330, "Suzano CSC bot");

            reply.AddHeroCard(
                "Olá vou te ajudar a encontrar documentos para os seguintes assuntos",
                "Se está com problema em algo diferente, clique em outros e digite a sua pergunta",
                Options.initialOptions,
                new[] { uri.ToString() }
                );

            await context.PostAsync(reply);

            context.Wait(this.OnOptionSelected);
        }

        private async Task OnOptionSelected(IDialogContext context, IAwaitable<IMessageActivity> result)
        {
            var message = await result;
            var text = "";

            //Nota Fiscal
            if (message.Text == Options.initialOptions[0])
            {
                var ntDialog = new NotaFiscalDialog();
                //text = "Você clicou em Nota fical";
                context.Call(ntDialog, NotaFiscalReplayMessage);
            }

            //Documento Fiscal
            if (message.Text == Options.initialOptions[1])
            {
				//text = "Você clicou em Documento fiscal";
				var atendimentoDialog = new AtendimentoDialog();
				context.Call(atendimentoDialog, AtendimentoReplayMessage);

			}

            //Outros
            if (message.Text == Options.initialOptions[2])
            {
                //text = "Você clicou em Documento fiscal";
                var atendimentoDialog = new OutrosDialog();
                context.Call(atendimentoDialog, OutrosReplayMessage);

            }

            var reply = context.MakeMessage();
            reply.Text = HttpUtility.HtmlDecode(text);
            await context.PostAsync(reply);
        }



		private async Task NotaFiscalReplayMessage(IDialogContext context, IAwaitable<object> result)
		{
			context.Done(string.Empty);
		}

		private async Task AtendimentoReplayMessage(IDialogContext context, IAwaitable<object> result)
		{
			context.Done(string.Empty);
		}
        private async Task OutrosReplayMessage(IDialogContext context, IAwaitable<object> result)
        {
            context.Done(string.Empty);
        }
    }
}