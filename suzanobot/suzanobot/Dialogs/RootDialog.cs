using Microsoft.Bot.Builder.Dialogs;
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
			UriBuilder uri = Utils.Utils.GetPlaceHoldImg(56, 640, 330, Options.nomeBot);

			reply.AddHeroCard(Options.tituloOpcaoInicial, Options.descricaoOpcaoInicial, Options.initialOptions, new[] { uri.ToString() });

			await context.PostAsync(reply);

			context.Wait(this.OnOptionSelected);
		}

		private async Task OnOptionSelected(IDialogContext context, IAwaitable<IMessageActivity> result)
		{
			var message = await result;
			var text = "";

			if (message.Text == Options.initialOptions[0])
			{
				var dialog = new NotaFiscalDialog();
				context.Call(dialog, NotaFiscalReplayMessage);
			}
			else if (message.Text == Options.initialOptions[1])
			{
				var dialog = new AtendimentoDialog();
				context.Call(dialog, AtendimentoReplayMessage);
			}
			else if (message.Text == Options.initialOptions[2])
			{
				var dialog = new OutrosDialog();
				context.Call(dialog, OutrosReplayMessage);
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