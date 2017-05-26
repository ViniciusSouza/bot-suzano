using Microsoft.Bot.Builder.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using suzanobot.Services;
using Microsoft.Bot.Connector;
using suzanobot.Extensions;
using suzanobot.Utils;

namespace suzanobot.Dialogs
{
	[Serializable]
	public class ItemDialog : IDialog<string>
	{
		public string Categoria { get; set; }
		public string Frente { get; set; }
		public Dictionary<string, string> Itens { get; set; }

		public ItemDialog(string frente, string categoria)
		{
			Categoria = categoria;
			Frente = frente;

			Itens = Database.GetItens(Frente, Categoria);
		}

		public async Task StartAsync(IDialogContext context)
		{
			string message = string.Format(Options.mensagemEscolhaItem, Categoria);

			//PromptDialog.Choice(context, ResumeAfterAnswer, Itens, message);
			var reply = context.MakeMessage();
			reply.Type = ActivityTypes.Message;
			reply.TextFormat = TextFormatTypes.Markdown;
			reply.Text = message;

			var actions = new List<CardAction>();

			foreach (var item in Itens)
			{
				actions.Add(new CardAction() { Title = item.Value, Value = string.Format("{0} - {1}", item.Key, item.Value) });
			}

			reply.SuggestedActions = new SuggestedActions()
			{
				Actions = actions
			};
			await context.PostAsync(reply);
			context.Wait(ItemSelected);

		}

		private async Task ItemSelected(IDialogContext context, IAwaitable<IMessageActivity> result)
		{
			var message = await result;

			var id = "-1";
			try
			{
				id = message.Text.Split('-')[0].Trim();
			}
			catch { }

			var documento = Database.getDocumentoByID(id);

			if (documento != null)
			{
				var reply = context.MakeMessage();
				reply.Type = ActivityTypes.Message;
				reply.TextFormat = "markdown";

				//var herocard = HeroCardExtensions.AddHeroCard(documento.Frente, documento.Categoria, documento.Requisitos, documento.Observacao, documento.Breadcrumb);
				//reply.Attachments.Add(herocard.ToAttachment());

				string text = "";
				text = text + string.Format("## {0} ## <br />", documento.Frente);
				text = text + string.Format("### {0} ### <br />", documento.Categoria);
				text = text + string.Format("**Requisitos:** {0} <br />", documento.Requisitos.Replace(";", "<br />"));
				text = text + string.Format("**Observação:** {0} <br />", documento.Observacao);
				text = text + string.Format("**Caminho no CSC:** {0} <br />", documento.Breadcrumb);

				reply.Text = text;

				await context.PostAsync(reply);
				context.Done("Completed");
			}
			else
			{
				context.Done("Failed");
			}
		}


	}
}