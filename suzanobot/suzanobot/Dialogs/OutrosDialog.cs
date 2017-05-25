using Microsoft.Bot.Builder.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using Microsoft.Bot.Connector;

namespace suzanobot.Dialogs
{
    [Serializable]
    public class OutrosDialog : IDialog
    {
		

		public async Task StartAsync(IDialogContext context)
        {
            string message = "Outros";
			
			PromptDialog.Choice(context, ResumeAfterAnswer, new[] { "Outros" }, message);
		}

		private async Task ResumeAfterAnswer(IDialogContext context, IAwaitable<string> result)
        {
            var message = context.MakeMessage();


            context.Done(await result);
        }

	}
}