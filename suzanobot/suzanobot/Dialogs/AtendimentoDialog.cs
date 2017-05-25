using Microsoft.Bot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using suzanobot.Utils;

namespace suzanobot.Dialogs
{
	[Serializable]
	public class AtendimentoDialog : IDialog
	{
		public async Task StartAsync(IDialogContext context)
		{
			string descricao = Options.descricaoOpcaoAtendimento;
			PromptDialog.Choice(context, ResumeAfterAnswer, Options.optionsAtendimento, descricao);
		}

		private async Task ResumeAfterAnswer(IDialogContext context, IAwaitable<string> result)
		{
			var message = await result;
			string textRetorno = "Você selecionou a opção " + message;

			//if (message == Options.optionsAtendimento[0])
			//{ }
			//else if (message == Options.optionsAtendimento[1])
			//{ }
			//else if (message == Options.optionsAtendimento[2])
			//{ }
			//else if (message == Options.optionsAtendimento[3])
			//{ }
			//else if (message == Options.optionsAtendimento[4])
			//{ }
			//else if (message == Options.optionsAtendimento[5])
			//{ }

			var reply = context.MakeMessage();
			reply.Text = HttpUtility.HtmlDecode(textRetorno);
			await context.PostAsync(reply);
			context.Done(string.Empty);
		}
	}
}