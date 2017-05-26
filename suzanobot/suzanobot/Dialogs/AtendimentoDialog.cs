using Microsoft.Bot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using suzanobot.Utils;
using suzanobot.Services;

namespace suzanobot.Dialogs
{
	[Serializable]
	public class AtendimentoDialog : IDialog
	{
		public async Task StartAsync(IDialogContext context)
		{
			string descricao = Options.descricaoOpcaoAtendimento;
			PromptDialog.Choice(context, ResumeAfterAnswer, Options.optionsSolicitacaoDocumentos, descricao);
		}

		private async Task ResumeAfterAnswer(IDialogContext context, IAwaitable<string> result)
		{
			var escolha = await result;
			string text = string.Format("Você escolheu **{0}**.", escolha);
			var reply = context.MakeMessage();
			reply.Text = HttpUtility.HtmlDecode(text);
			await context.PostAsync(reply);
			context.Done(string.Empty);
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