using Microsoft.Bot.Builder.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using suzanobot.Services;
using Microsoft.Bot.Connector;
using suzanobot.Utils;

namespace suzanobot.Dialogs
{
    [Serializable]
    public class CategoriaDialog : IDialog<string>
    {
        public string Frente { get; set; }
        public string Categoria { get; set; }
        public List<string> Categorias { get; set; }

        public CategoriaDialog (string frente)
        {
            Frente = frente;

            Categorias = Database.GetCategorias(Frente);
        }

        public async Task StartAsync(IDialogContext context)
        {
            string message = string.Format(Options.mensagemEscolhaCategoria, Frente);

            PromptDialog.Choice(context, ResumeAfterAnswer, Categorias, message);
        }

        private async Task ResumeAfterAnswer(IDialogContext context, IAwaitable<string> result)
        {
            Categoria = await result;
            var dialog = new ItemDialog(Frente, Categoria);

            context.Call(dialog, itemSelected);
        }

        private async Task itemSelected(IDialogContext context, IAwaitable<string> result)
        {
            var item = await result;
            context.Done(item);
        }
    }
}