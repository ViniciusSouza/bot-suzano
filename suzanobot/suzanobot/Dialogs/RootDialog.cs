using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using suzanobot.Extensions;
using System;
using System.Threading.Tasks;
using System.Web;
using suzanobot.Utils;
using System.Collections.Generic;
using suzanobot.Model;
using suzanobot.Services;

namespace suzanobot.Dialogs
{
	[Serializable]
	public class RootDialog : IDialog<object>
	{
		private ResumptionCookie resumptionCookie;

        public string Frente { get; set; }
        public string Categoria { get; set; }

        public Task StartAsync(IDialogContext context)
		{
            Database.LoadJson();

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
			UriBuilder uri = Utils.Utils.GetPlaceHoldImg(56, 640, 330, Options.nomeBot);

            var frentes = Database.GetFrentes().ToArray();

            if (frentes != null && frentes.Length > 0)
            {
                reply.AddHeroCard(Options.tituloOpcaoInicial, Options.descricaoOpcaoInicial, frentes, new[] { uri.ToString() });
                await context.PostAsync(reply);
                context.Wait(this.onFrenteSelected);
            }
            else
            {
                var text = "Não existem frentes no arquivo Json, contate o administrador do bot";
                reply.Text = HttpUtility.HtmlDecode(text);
                await context.PostAsync(reply);
            }
		}

        private async Task onFrenteSelected(IDialogContext context, IAwaitable<IMessageActivity> result)
        {
            var message = await result;
            Frente = message.Text;
            var dialog = new CategoriaDialog(Frente);
            context.Call(dialog, CategoriaReplayMessage);
        }

        private async Task CategoriaReplayMessage(IDialogContext context, IAwaitable<string> result)
        {
            var resultado = await result;

            var text = "";
            if (resultado.Equals("Completed"))
            {
                text = "Obrigado por utilizar o Suzan";
            }
            else
            {
                text = "Sinto não poder te ajudar, ainda bem que conto com um time muito bom disponível através do número/contato";

            }

            var reply = context.MakeMessage();
            reply.Text = HttpUtility.HtmlDecode(text);
            await context.PostAsync(reply);
            context.Done("");

        }

     
	}
}