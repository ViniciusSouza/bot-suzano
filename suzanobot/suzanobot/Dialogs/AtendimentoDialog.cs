using Microsoft.Bot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;

namespace suzanobot.Dialogs
{
	[Serializable]
	public class AtendimentoDialog : IDialog
	{
		private string[] optionsSolicitacaoDocumento = new[]
			{
				"Certidão Negativa de Débito",
				"Certidão Pendencias Financ. Suzano",
				"Alvarás",
				"Comprovante de recolhimento",
				"Recibo de entrega de obrigação",
				"Notas Fiscais emitidas pela Suzano",
			};

		private string[] optionsComprovanteRecolhimentoImpostos = new[]
			{
				"Fornecedor",
				"Suzano",
			};

		
		public async Task StartAsync(IDialogContext context)
		{
			string message = "Qual o procedimento desejado?";
			//PromptDialog.Choice(context, ResumeAfterAnswer, new[] { "Não Lançada", "Lançada", "Em Processamento", "Não Sei" }, message);
			PromptDialog.Choice(context, ResumeAfterAnswer, new[] { "Solicitação de documentos", "Comprovante de Recolhimento de Impostos" },message);
		}

		private async Task ResumeAfterAnswer(IDialogContext context, IAwaitable<string> result)
		{
			var message = context.MakeMessage();
			switch (await result)
			{
				case "Solicitação de documentos":
					message.AddHeroCard("Escolha o processo", optionsSolicitacaoDocumento);
					await context.PostAsync(message);
					context.Wait(this.OnOptionSelectedSolicitacaoDocumento);
					break;
				case "Comprovante de Recolhimento de Impostos":
					message.AddHeroCard("Escolha o processo", optionsComprovanteRecolhimentoImpostos);
					await context.PostAsync(message);
					context.Wait(this.OnOptionSelectedComprovanteRecolhimentoImpostos);
					break;
			}
			
		}

		private async Task OnOptionSelectedComprovanteRecolhimentoImpostos(IDialogContext context, IAwaitable<IMessageActivity> result)
		{
			var message = await result;
			var text = "";

			if (message.Text == "Fornecedor")
			{
				text = "Você clicou em Fornecedor";
			}

			if (message.Text == "Suzano")
			{
				text = "Você clicou em Suzano";
			}

			var reply = context.MakeMessage();
			reply.Text = HttpUtility.HtmlDecode(text);
			await context.PostAsync(reply);
		}


			private async Task OnOptionSelectedSolicitacaoDocumento(IDialogContext context, IAwaitable<IMessageActivity> result)
		{
			var message = await result;
			var text = "";

			if (message.Text == "Certidão Negativa de Débito")
			{
				text = "Você clicou em Certidão Negativa de Débito";
			}

			if (message.Text == "Certidão Pendencias Financ. Suzano")
			{
				text = "Você clicou em Certidão Pendencias Financ. Suzano";
			}

			if (message.Text == "Alvarás")
			{
				text = "Você clicou em Alvarás";
			}

			if (message.Text == "Comprovante de recolhimento")
			{
				text = "Você clicou em Comprovante de recolhimento";
			}

			if (message.Text == "Recibo de entrega de obrigação")
			{
				text = "Você clicou em Recibo de entrega de obrigação";
			}

			if (message.Text == "Notas Fiscais emitidas pela Suzano")
			{
				text = "Você clicou em Notas Fiscais emitidas pela Suzano";
			}
			

			var reply = context.MakeMessage();
			reply.Text = HttpUtility.HtmlDecode(text);
			await context.PostAsync(reply);
		}
	}
}